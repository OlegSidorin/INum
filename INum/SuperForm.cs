using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using System.IO;
using System.Reflection;
using Form = System.Windows.Forms.Form;
using IWin32Window = System.Windows.Forms.IWin32Window;
using ComponentManager = Autodesk.Windows.ComponentManager;
using Keys = System.Windows.Forms.Keys;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Autodesk.Revit.UI.Selection;

namespace INum
{
    public partial class SuperForm : Form
    {
        public ButtonCExternalEvent buttonCExternalEvent;
        public ExternalEvent externalCEvent;

        public SuperForm()
        {
            InitializeComponent();
            MyData myData = new MyData();
            string input = Main.ReadFromFile(Main.filename);
            myData = JsonConvert.DeserializeObject<MyData>(input);
            tb_prefix.Text = myData.Prefix;
            tb_suffix.Text = myData.Suffix;
            tb_eqp.Text = myData.Eqp;
            nm.Value = myData.StartNum;
            buttonCExternalEvent = new ButtonCExternalEvent();
            externalCEvent = ExternalEvent.Create(buttonCExternalEvent);
        }


        private void buttonС_Click(object sender, EventArgs e)
        {
            Main.prefix = tb_prefix.Text;
            Main.eqp = tb_eqp.Text;
            Main.suffix = tb_suffix.Text;
            Main.startnum = nm.Value;

            MyData myData = new MyData();
            myData.Prefix = tb_prefix.Text;
            myData.Eqp = tb_eqp.Text;
            myData.Suffix = tb_suffix.Text;
            myData.StartNum = nm.Value;
            string output = JsonConvert.SerializeObject(myData);
            Main.WriteToFile(Main.filename, output);

            externalCEvent.Raise();
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
                        //int.TryParse(Main.startnum, out int start);
                        p.Set(Main.prefix + Main.eqp + Main.suffix + Main.startnum.ToString("0"));
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
    public class ButtonCExternalEvent : IExternalEventHandler
    {
        static bool _place_one_single_instance_then_abort = true;
        List<ElementId> _added_element_ids = new List<ElementId>();
        IWin32Window _revit_window;

        public void Execute(UIApplication app)
        {
            _revit_window = new JtWindowHandle(ComponentManager.ApplicationWindow);
            UIDocument uidoc = app.ActiveUIDocument;
            Document doc = app.ActiveUIDocument.Document;

            FilteredElementCollector a = new FilteredElementCollector(doc).OfClass(typeof(Family));

            Family family = a.FirstOrDefault<Element>(e => e.Name.Equals("КПСП_Марка_МСК_Маркировка")) as Family;

            if (null == family)
            {

                if (!File.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\КПСП_Марка_МСК_Маркировка.rfa"))
                {
                    TaskDialog.Show("!", "Нет семейства марки");
                }

                using (Transaction tx = new Transaction(doc, "Load Family"))
                {
                    tx.Start();
                    doc.LoadFamily(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\КПСП_Марка_МСК_Маркировка.rfa", out family);
                    tx.Commit();
                }
            }

            FamilySymbol symbol = null;
            ElementId eId = null;
            foreach (ElementId id in family.GetFamilySymbolIds())
            {
                eId = id;
                FamilySymbol s = doc.GetElement(id) as FamilySymbol;
                symbol = s;
                break;
            }

            _added_element_ids.Clear();

            app.Application.DocumentChanged += new EventHandler<DocumentChangedEventArgs>(OnDocumentChanged);
            try
            {
                uidoc.PromptForFamilyInstancePlacement(symbol);
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException ex)
            {
                Debug.Print(ex.Message);
            }

            app.Application.DocumentChanged -= new EventHandler<DocumentChangedEventArgs>(OnDocumentChanged);

            int n = _added_element_ids.Count;
            
            ElementId selectedElementId = null;

            var itlist = new FilteredElementCollector(doc).OfClass(typeof(IndependentTag)).Where(e => e.Name.Equals("КПСП_Марка_МСК_Маркировка")).Cast<IndependentTag>().ToList();

            foreach (IndependentTag it in itlist)
            {
                if (Main.adElementId.IntegerValue == it.Id.IntegerValue)
                {
                    selectedElementId = it.TaggedLocalElementId;
                }

            }

            using (Transaction tr = new Transaction(doc, "MyTransaction"))
            {
                tr.Start();

                ParameterSet ps = doc.GetElement(selectedElementId).Parameters;
                foreach (Parameter p in ps)
                {
                    if (p.Definition.Name.ToLower() == "мск_маркировка")
                    {
                        p.Set(Main.prefix + Main.eqp + Main.suffix + Main.startnum.ToString("0"));
                    }
                }
                tr.Commit();
            }

            return;
        }
        void OnDocumentChanged(object sender, DocumentChangedEventArgs e)
        {
            ICollection<ElementId> idsAdded = e.GetAddedElementIds();
            
            Main.adElementId = idsAdded.FirstOrDefault();

            int n = idsAdded.Count;

            Debug.Print("{0} id{1} added.", n, Util.PluralSuffix(n));

            // this does not work, because the handler will
            // be called each time a new instance is added,
            // overwriting the previous ones recorded:

            //_added_element_ids = e.GetAddedElementIds();

            _added_element_ids.AddRange(idsAdded);

            if (_place_one_single_instance_then_abort
              && 0 < n)
            {
                // Why do we send the WM_KEYDOWN message twice?
                // I tried sending it once only, and that does
                // not work. Maybe the proper thing to do would 
                // be something like the Press.OneKey method...
                // nope, that did not work.

                //Press.OneKey( _revit_window.Handle,
                //  (char) Keys.Escape );

                Press.PostMessage(_revit_window.Handle,
                  (uint)Press.KEYBOARD_MSG.WM_KEYDOWN,
                  (uint)Keys.Escape, 0);

                Press.PostMessage(_revit_window.Handle,
                  (uint)Press.KEYBOARD_MSG.WM_KEYDOWN,
                  (uint)Keys.Escape, 0);
            }
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
