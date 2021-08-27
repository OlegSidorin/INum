using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;
using Autodesk.Revit.Attributes;

namespace INum
{
    [Transaction(TransactionMode.Manual), Regeneration(RegenerationOption.Manual)]
    public class Main : IExternalApplication
    {
        public MyExternalEventHandler myExternalEventHandler;
        public ExternalEvent myExternalEvent;

        public static ElementId adElementId = null;

        public static string TabName { get; set; } = "Надстройки";
        public static string PanelTechName { get; set; } = "На плане";

        public Result OnStartup(UIControlledApplication application)
        {
            var techPanel = application.CreateRibbonPanel(PanelTechName);
            string path = Assembly.GetExecutingAssembly().Location;
            DataForForm.FileName = Path.GetDirectoryName(path) + "\\inumdata.txt";
            var MBtnData = new PushButtonData("MBtnData", "МСК_\nмаркировка", path, "INum.MarkingCommand")
            {
                ToolTipImage = PngImageSource("INum.res.num.png"), 
                ToolTip = "Маркирует экземпляры семейств, записывает в параметр МСК_Маркировка"
            };
            var TechBtn = techPanel.AddItem(MBtnData) as PushButton;
            TechBtn.LargeImage = PngImageSource("INum.res.num-32.png"); 

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        private System.Windows.Media.ImageSource PngImageSource(string embeddedPath)
        {
            Stream stream = this.GetType().Assembly.GetManifestResourceStream(embeddedPath);
            var decoder = new System.Windows.Media.Imaging.PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);

            return decoder.Frames[0];
        }
    }
}
