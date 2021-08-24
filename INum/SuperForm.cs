using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Form = System.Windows.Forms.Form;
using Newtonsoft.Json;

namespace INum
{
    public partial class SuperForm : Form
    {
        public ButtonExternalEvent buttonExternalEvent;
        public ExternalEvent externalEvent;
        public SuperForm()
        {
            InitializeComponent();
            MyData myData = new MyData();
            string input = Main.ReadFromFile(Main.filename);
            myData = JsonConvert.DeserializeObject<MyData>(input);
            tb_prefix.Text = myData.Prefix;
            tb_suffix.Text = myData.Suffix;
            tb_eqp.Text = myData.Eqp;
            tb_startnum.Text = myData.StartNum;
            buttonExternalEvent = new ButtonExternalEvent();
            externalEvent = ExternalEvent.Create(buttonExternalEvent);
        }

        private void button_Click(object sender, EventArgs e)
        {
            externalEvent.Raise();
            Main.prefix = tb_prefix.Text;
            Main.eqp = tb_eqp.Text;
            Main.suffix = tb_suffix.Text;
            Main.startnum = tb_startnum.Text;

            MyData myData = new MyData();
            myData.Prefix = tb_prefix.Text;
            myData.Eqp = tb_eqp.Text;
            myData.Suffix = tb_suffix.Text;
            myData.StartNum = tb_startnum.Text;
            string output = JsonConvert.SerializeObject(myData);
            Main.WriteToFile(Main.filename, output);
            //Close();
        }

    }

    public class ButtonExternalEvent : IExternalEventHandler
    {

        public void Execute(UIApplication app)
        {
            UIDocument uidoc = app.ActiveUIDocument;
            Document doc = app.ActiveUIDocument.Document;

            Reference selectedElement = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, new AnnotationFilter(), "Select...");
            ElementId selectedElementId = selectedElement.ElementId;
            using (Transaction tr = new Transaction(doc, "MyTransaction"))
            {
                tr.Start();
                    
                ParameterSet ps = doc.GetElement(selectedElementId).Parameters;
                foreach (Parameter p in ps)
                {
                    if (p.Definition.Name.ToLower() == "мск_маркировка")
                    {
                        int.TryParse(Main.startnum, out int start);
                        p.Set(Main.prefix + Main.eqp + Main.suffix + start.ToString());
                        start += 1;
                        Main.startnum = start.ToString();
                        
                    }
                }
                tr.Commit();
            }

            
            return;
        }

        public string GetName()
        {
            return "External Event";
        }
    }

    public class AnnotationFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            if (elem.Category.Id.IntegerValue == (int) BuiltInCategory.OST_CommunicationDevices)
            {
                return true;
            }
            if (elem.Category.Id.IntegerValue == (int)BuiltInCategory.OST_FireAlarmDevices)
            {
                return true;
            }
            if (elem.Category.Id.IntegerValue == (int)BuiltInCategory.OST_MechanicalEquipment)
            {
                return true;
            }
            if (elem.Category.Id.IntegerValue == (int)BuiltInCategory.OST_ElectricalEquipment)
            {
                return true;
            }

            return false;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            throw new NotImplementedException();
        }
    }


}
