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
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Form = System.Windows.Forms.Form;

namespace INum
{
    public partial class SuperForm : Form
    {
        public ButtonExternalEvent buttonExternalEvent;
        public ExternalEvent externalEvent;
        public SuperForm()
        {
            InitializeComponent();
            buttonExternalEvent = new ButtonExternalEvent();
            externalEvent = ExternalEvent.Create(buttonExternalEvent);
        }

        private void button_Click(object sender, EventArgs e)
        {
            externalEvent.Raise();
            ButtonExternalEvent.prefix = tb_prefix.Text;
            ButtonExternalEvent.eqp = tb_eqp.Text;
            ButtonExternalEvent.suffix = tb_suffix.Text;
            ButtonExternalEvent.startnum = tb_startnum.Text;
            Close();
        }
    }

    public class ButtonExternalEvent : IExternalEventHandler
    {
        public static string prefix = "";
        public static string eqp = "";
        public static string suffix = "";
        public static string startnum = "";
        public void Execute(UIApplication app)
        {
            UIDocument uidoc = app.ActiveUIDocument;
            Document doc = app.ActiveUIDocument.Document;
            
            Reference selectedElement = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, new AnnotationFilter(), "Select...");
            ElementId selectedElementId = selectedElement.ElementId;
            using (Transaction tr = new Transaction(doc, "wer"))
            {
                tr.Start();
                ParameterSet ps = doc.GetElement(selectedElementId).Parameters;
                foreach(Parameter p in ps)
                {
                    if (p.Definition.Name.ToLower() == "мск_маркировка")
                    {
                        p.Set(prefix + eqp + suffix + startnum);
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
               
            return false;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            throw new NotImplementedException();
        }
    }
}
