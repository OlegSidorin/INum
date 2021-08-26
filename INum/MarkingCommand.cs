using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using System.IO;
using System.Reflection;
using ComponentManager = Autodesk.Windows.ComponentManager;
using Keys = System.Windows.Forms.Keys;

namespace INum
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    class MarkingCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;
            var app = commandData.Application.Application;
            string path = Assembly.GetExecutingAssembly().Location;
            string FOPFile = Path.GetDirectoryName(path) + "\\КПСП_ФОП.txt";
            app.SharedParametersFilename = FOPFile;
            CategorySet catSet = commandData.Application.Application.Create.NewCategorySet();
            Category cat;
            cat = doc.Settings.Categories.get_Item(BuiltInCategory.OST_MechanicalEquipment);
            catSet.Insert(cat);
            cat = doc.Settings.Categories.get_Item(BuiltInCategory.OST_ElectricalEquipment);
            catSet.Insert(cat);
            cat = doc.Settings.Categories.get_Item(BuiltInCategory.OST_SpecialityEquipment);
            catSet.Insert(cat);
            cat = doc.Settings.Categories.get_Item(BuiltInCategory.OST_CommunicationDevices);
            catSet.Insert(cat);
            cat = doc.Settings.Categories.get_Item(BuiltInCategory.OST_FireAlarmDevices);
            catSet.Insert(cat);
            cat = doc.Settings.Categories.get_Item(BuiltInCategory.OST_TelephoneDevices);
            catSet.Insert(cat);
            cat = doc.Settings.Categories.get_Item(BuiltInCategory.OST_ElectricalFixtures);
            catSet.Insert(cat);
            cat = doc.Settings.Categories.get_Item(BuiltInCategory.OST_DataDevices);
            catSet.Insert(cat);
            cat = doc.Settings.Categories.get_Item(BuiltInCategory.OST_LightingDevices);
            catSet.Insert(cat);
            cat = doc.Settings.Categories.get_Item(BuiltInCategory.OST_NurseCallDevices);
            catSet.Insert(cat);
            cat = doc.Settings.Categories.get_Item(BuiltInCategory.OST_SecurityDevices);
            catSet.Insert(cat);
            cat = doc.Settings.Categories.get_Item(BuiltInCategory.OST_GenericModel);
            catSet.Insert(cat);
            try
            {
                using (Transaction t = new Transaction(doc))
                {
                    t.Start("Add Shared Parameters");
                    DefinitionFile sharedParameterFile = app.OpenSharedParameterFile();
                    DefinitionGroup sharedParameterGroup = sharedParameterFile.Groups.get_Item("01 Обязательные ОБЩИЕ");
                    Definition sharedParameterDefinition = sharedParameterGroup.Definitions.get_Item("МСК_Маркировка");
                    ExternalDefinition externalDefinition = sharedParameterGroup.Definitions.get_Item("МСК_Маркировка") as ExternalDefinition;
                    Guid guid = externalDefinition.GUID;
                    InstanceBinding newIB = app.Create.NewInstanceBinding(catSet);
                    doc.ParameterBindings.Insert(externalDefinition, newIB, BuiltInParameterGroup.PG_TEXT);
                    //SharedParameterElement sp = SharedParameterElement.Lookup(doc, guid);
                    // InternalDefinition def = sp.GetDefinition();
                    // def.SetAllowVaryBetweenGroups(doc, true);
                    t.Commit();
                }

            }
            catch (Exception e)
            {
                TaskDialog.Show("Warning", e.ToString());
            }

            AppForm form = new AppForm();
            form.Show();

            return Result.Succeeded;
        }
    }
}
