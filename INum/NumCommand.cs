using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;

namespace INum
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    class NumCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = commandData.Application.ActiveUIDocument.Document;
            //System.Windows.Forms.MessageBox.Show("Привет! " + doc.ActiveView.ToString());
            /*
            do
            {
                
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
            }
            
            while (Main.startnum != "5");
            */
            SuperForm form = new SuperForm();
            form.Show();

            return Result.Succeeded;
        }
    }
}
