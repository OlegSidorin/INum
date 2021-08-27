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

namespace INum
{
    public partial class AppForm : Form
    {
        public ButtonCExternalEvent buttonCExternalEvent;
        public ExternalEvent externalCEvent;


        public AppForm()
        {
            InitializeComponent();

            var dataForForm = new DataForForm();
            dataForForm.GetData();
            tb_prefix.Text = dataForForm.Prefix;
            tb_suffix.Text = dataForForm.Suffix;
            tb_middle_part.Text = dataForForm.MiddlePart;
            nm.Value = dataForForm.StartNum;

            buttonCExternalEvent = new ButtonCExternalEvent();
            externalCEvent = ExternalEvent.Create(buttonCExternalEvent);
        }


        private void buttonС_Click(object sender, EventArgs e)
        {
            AppForm appForm = AppForm.ActiveForm as AppForm;
            ButtonCExternalEvent._active_form = appForm;

            var dataForForm = new DataForForm();
            dataForForm.Prefix = tb_prefix.Text;
            dataForForm.MiddlePart = tb_middle_part.Text;
            dataForForm.Suffix = tb_suffix.Text;
            dataForForm.StartNum = nm.Value;
            dataForForm.WriteData();

            ButtonCExternalEvent._data_for_form = dataForForm;

            externalCEvent.Raise();
        }
    }

    public class ButtonExternalEvent : IExternalEventHandler
    {
        public static DataForForm _data_for_form;
        public void Execute(UIApplication app)
        {
            UIDocument uidoc = app.ActiveUIDocument;
            Document doc = app.ActiveUIDocument.Document;

            Reference selectedElement = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, new ElementsFilter(), "Select...");
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
                        p.Set(_data_for_form.Prefix + _data_for_form.MiddlePart + _data_for_form.Suffix + _data_for_form.StartNum.ToString("0"));
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
        public static AppForm _active_form;
        public static DataForForm _data_for_form;

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

            var eventHandler = new EventHandler<DocumentChangedEventArgs>(OnDocumentChanged);
            app.Application.DocumentChanged += eventHandler;
            try
            {
                uidoc.PromptForFamilyInstancePlacement(symbol);
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException ex)
            {
                Debug.Print(ex.Message);
            }

            app.Application.DocumentChanged -= eventHandler;

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
            //var eventHandler2 = new EventHandler<DocumentChangedEventArgs>(OnMyTransaction);
            //app.Application.DocumentChanged += eventHandler2;
            using (Transaction tr = new Transaction(doc, "MyTransaction"))
            {
                tr.Start();

                ParameterSet ps = doc.GetElement(selectedElementId).Parameters;
                foreach (Parameter p in ps)
                {
                    if (p.Definition.Name.ToLower() == "мск_маркировка")
                    {
                        p.Set(_data_for_form.Prefix + _data_for_form.MiddlePart + _data_for_form.Suffix + _data_for_form.StartNum.ToString("0"));
                    }
                }
                tr.Commit();
            }
            _active_form.nm.Value += 1;
            //app.Application.DocumentChanged -= eventHandler2;
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

            string txname = e.GetTransactionNames().FirstOrDefault();

            if (txname == "MyTransaction")
            {
                Document doc = e.GetDocument();

                AppForm appForm = AppForm.ActiveForm as AppForm;
                appForm.nm.Value += 1;

                TaskDialog.Show("1", "Опа..." + doc.Title);
            }

        }
        void OnMyTransaction(object sender, DocumentChangedEventArgs e)
        {
            AppForm appForm = AppForm.ActiveForm as AppForm;
            string isActive = appForm == null ? " no access " : "access " + appForm.ClientSize.ToString();
            TaskDialog.Show("1", "Опа..." + e.GetDocument().Title + " Tranaction: " +e.GetTransactionNames().FirstOrDefault() + isActive);
            
            //string txname = e.GetTransactionNames().FirstOrDefault();

            //if (txname == "MyTransaction")
            //{
            //    Document doc = e.GetDocument();

                //AppForm appForm = AppForm.ActiveForm as AppForm;
                //appForm.nm.Value += 1;

            //}

        }
        public string GetName()
        {
            return "External Event";
        }
    }


}
