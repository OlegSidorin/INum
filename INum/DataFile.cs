using System.IO;
using System.Text;
using System;

namespace INum
{
    public class DataFile
    {
        public static string FileName = "";
        public static string ReadFromFile(string fileName)
        {
            string textFromFile = "";
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
                    System.Windows.Forms.MessageBox.Show(e.ToString());
                }
            }
            using (StreamReader reader = new StreamReader(fileName, true))
            {
                textFromFile = reader.ReadLine();
            }
            return textFromFile;
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
                    System.Windows.Forms.MessageBox.Show(e.ToString());
                }
            }
            using (StreamWriter writer = new StreamWriter(fileName, false))
            {
                writer.WriteLine(txt);
            }

        }
    }
}