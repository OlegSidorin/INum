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
using Autodesk.Revit.DB.Events;
using Newtonsoft.Json;

namespace INum
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class Main : IExternalApplication
    {
        public static string filename = "";
        public MyExternalEventHandler myExternalEventHandler;
        public ExternalEvent myExternalEvent;
        public static string prefix = "";
        public static string eqp = "";
        public static string suffix = "";
        public static string startnum = "";

        public static string TabName { get; set; } = "Надстройки";
        public static string PanelTechName { get; set; } = "NUMINC";

        public Result OnStartup(UIControlledApplication application)
        {
            var techPanel = application.CreateRibbonPanel(PanelTechName);
            string path = Assembly.GetExecutingAssembly().Location;
            filename = Path.GetDirectoryName(path) + "\\inumdata.txt";
            var MBtnData = new PushButtonData("MBtnData", "Авто\nномер", path, "INum.NumCommand")
            {
                ToolTipImage = PngImageSource("INum.res.num.png"), //new BitmapImage(new Uri(Path.GetDirectoryName(path) + "\\res\\num.png", UriKind.Absolute)),
                ToolTip = "Маркирует экземпляры семейств"
            };
            var TechBtn = techPanel.AddItem(MBtnData) as PushButton;
            TechBtn.LargeImage = PngImageSource("INum.res.num-32.png"); //new BitmapImage(new Uri(Path.GetDirectoryName(path) + "\\res\\num-32.png", UriKind.Absolute));

            application.ControlledApplication.DocumentChanged += ControlledApplication_DocumentChanged;

            /*
            MyData myData = new MyData();
            myData.Prefix = "pre-";
            myData.Eqp = "Equip";
            myData.Suffix = "-suf";
            myData.StartNum = "1";
            string output = JsonConvert.SerializeObject(myData);
            WriteToFile("C:\\Users\\Sidorin_O\\Documents\\TEST\\inumdata.txt", output);
            */

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            myExternalEventHandler = new MyExternalEventHandler();
            myExternalEvent = ExternalEvent.Create(myExternalEventHandler);
            myExternalEvent.Raise();
            return Result.Succeeded;
        }


        private void ControlledApplication_DocumentChanged(object sender, DocumentChangedEventArgs e)
        {
            string txname = e.GetTransactionNames().FirstOrDefault();

            if (txname == "MyTransaction")
            {
                Document doc = e.GetDocument();

                TaskDialog.Show("1", "Опа..." + doc.Title);
            }
        }

        private System.Windows.Media.ImageSource PngImageSource(string embeddedPath)
        {
            Stream stream = this.GetType().Assembly.GetManifestResourceStream(embeddedPath);
            var decoder = new System.Windows.Media.Imaging.PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);

            return decoder.Frames[0];
        }
        public static void WriteToFile(string fileName, string txt)
        {

            if (!File.Exists(fileName))
            {
                try
                {
                    using (FileStream fs = File.Create(fileName))
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes("");
                        fs.Write(info, 0, info.Length);
                    }
                }
                catch (Exception e)
                {
                    //System.Windows.MessageBox.Show(e.ToString());
                }
            }
            using (StreamWriter writer = new StreamWriter(fileName, false))
            {
                writer.WriteLine(txt);
            }

        }

        public static string ReadFromFile(string fileName)
        {
            string output = "";
            using (StreamReader reader = new StreamReader(fileName, true))
            {
                output = reader.ReadLine();
            }
            return output;
        }

    }

    public class MyExternalEventHandler : IExternalEventHandler
    {
        public void Execute(UIApplication app)
        {
            TaskDialog.Show("1", "Ooooops");
            return;
        }

        public string GetName()
        {
            return "Handler";
        }
    }

    public class MyData
    {
        public string Prefix { get; set; } 
        public string Eqp { get; set; } 
        public string Suffix { get; set; } 
        public string StartNum { get; set; } 

    }
}
