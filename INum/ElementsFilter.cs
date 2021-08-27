using System;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

namespace INum
{
    public class ElementsFilter : ISelectionFilter
    {
        public bool AllowElement(Element element)
        {
            var elementId = element.Category.Id.IntegerValue;
            if (elementId == (int)BuiltInCategory.OST_MechanicalEquipment) return true;
            if (elementId == (int)BuiltInCategory.OST_ElectricalEquipment) return true;
            if (elementId == (int)BuiltInCategory.OST_SpecialityEquipment) return true;
            if (elementId == (int)BuiltInCategory.OST_CommunicationDevices) return true;
            if (elementId == (int)BuiltInCategory.OST_FireAlarmDevices) return true;
            if (elementId == (int)BuiltInCategory.OST_TelephoneDevices) return true;
            if (elementId == (int)BuiltInCategory.OST_ElectricalFixtures) return true;
            if (elementId == (int)BuiltInCategory.OST_DataDevices) return true;
            if (elementId == (int)BuiltInCategory.OST_LightingDevices) return true;
            if (elementId == (int)BuiltInCategory.OST_NurseCallDevices) return true;
            if (elementId == (int)BuiltInCategory.OST_SecurityDevices) return true;
            if (elementId == (int)BuiltInCategory.OST_SecurityDevices) return true;
            if (elementId == (int)BuiltInCategory.OST_GenericModel) return true;

            return false;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            throw new NotImplementedException();
        }
    }

}
