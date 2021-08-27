using System.IO;
using System.Text;
using System;
using Newtonsoft.Json;
using System.Reflection;

namespace INum
{
    public class DataForForm
    {
        public static string FileName = 
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\inumdata.txt";


        public string Prefix { get; set; }
        public string MiddlePart { get; set; }
        public string Suffix { get; set; }
        public decimal StartNum { get; set; }


        public void WriteData()
        {
            if (!File.Exists(FileName))
            {
                try
                {
                    using (FileStream fs = File.Create(FileName))
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes("");
                        fs.Write(info, 0, info.Length);
                    }
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.ToString());
                }
            }
            var dataForForm = new DataForForm();
            dataForForm.Prefix = Prefix;
            dataForForm.MiddlePart = MiddlePart;
            dataForForm.Suffix = Suffix;
            dataForForm.StartNum = StartNum;

            string output = JsonConvert.SerializeObject(dataForForm);
            using (StreamWriter writer = new StreamWriter(FileName, false))
            {
                writer.WriteLine(output);
            }
        }
        public void GetData()
        {
            string textFromFile = "";
            
            if (!File.Exists(FileName))
            {
                try
                {
                    using (FileStream fs = File.Create(FileName))
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes("");
                        fs.Write(info, 0, info.Length);
                    }
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.ToString());
                }
            }
            using (StreamReader reader = new StreamReader(FileName, true))
            {
                textFromFile = reader.ReadLine();
            }
            var dataForForm = new DataForForm();
            dataForForm = JsonConvert.DeserializeObject<DataForForm>(textFromFile);
            Prefix = dataForForm.Prefix;
            MiddlePart = dataForForm.MiddlePart;
            Suffix = dataForForm.Suffix;
            StartNum = dataForForm.StartNum;
        }
    }
}