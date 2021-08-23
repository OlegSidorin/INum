using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace INum
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class Main : IExternalApplication
    {
        public static string TabName { get; set; } = "Надстройки";
        public static string PanelTechName { get; set; } = "NUMINC";

        public Result OnStartup(UIControlledApplication application)
        {
            var techPanel = application.CreateRibbonPanel(PanelTechName);
            string path = Assembly.GetExecutingAssembly().Location;

            var MBtnData = new PushButtonData("MBtnData", "Авто\nномер", path, "INum.NumCommand")
            {
                ToolTipImage = PngImageSource("INum.res.num.png"), //new BitmapImage(new Uri(Path.GetDirectoryName(path) + "\\res\\num.png", UriKind.Absolute)),
                ToolTip = "Маркирует экземпляры семейств"
            };
            var TechBtn = techPanel.AddItem(MBtnData) as PushButton;
            TechBtn.LargeImage = PngImageSource("INum.res.num-32.png"); //new BitmapImage(new Uri(Path.GetDirectoryName(path) + "\\res\\num-32.png", UriKind.Absolute));


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
