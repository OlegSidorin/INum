using Autodesk.Revit.UI;

namespace INum
{
    public class MyExternalEventHandler : IExternalEventHandler
    {
        public void Execute(UIApplication app)
        {
            string str = "";
            
            TaskDialog.Show("1", "Ooooops" + str);
            return;
        }

        public string GetName()
        {
            return "Handler";
        }
    }
}
