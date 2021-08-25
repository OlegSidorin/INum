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
using IWin32Window = System.Windows.Forms.IWin32Window;
using ComponentManager = Autodesk.Windows.ComponentManager;
using Keys = System.Windows.Forms.Keys;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace INum
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    class NumCommand : IExternalCommand
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

            SuperForm form = new SuperForm();
            form.Show();

            return Result.Succeeded;
        }
    }
    public class JtWindowHandle : IWin32Window
    {
        IntPtr _hwnd;

        public JtWindowHandle(IntPtr h)
        {
            Debug.Assert(IntPtr.Zero != h,
              "expected non-null window handle");

            _hwnd = h;
        }

        public IntPtr Handle
        {
            get
            {
                return _hwnd;
            }
        }
    }
    class Util
    {
        /// <summary>
        /// Return an English plural suffix 's' or
        /// nothing for the given number of items.
        /// </summary>
        public static string PluralSuffix(int n)
        {
            return 1 == n ? "" : "s";
        }

        // JavaScript sample implementations:
        // capitalize:function(){return this.replace(/\b[a-z]/g,function(match){return match.toUpperCase();});}
        // capitalize: function() { return this.charAt(0).toUpperCase() + this.substring(1).toLowerCase(); }

        /// <summary>
        /// Ensure that each space delimited word in the 
        /// given string has an upper case first character.
        /// </summary>
        public static string Capitalize(string s)
        {
            return string.Join(" ", s.Split(null)
              .Select<string, string>(a
               => a.Substring(0, 1).ToUpper()
                 + a.Substring(1)));
        }

        public static string RealString(double a)
        {
            return a.ToString("0.##");
        }

        public static string PointString(XYZ p)
        {
            return string.Format("({0},{1},{2})",
              RealString(p.X), RealString(p.Y),
              RealString(p.Z));
        }

        /// <summary>
        /// Extract a true or false value from the given
        /// string, accepting yes/no, Y/N, true/false, T/F
        /// and 1/0. We are extremely tolerant, i.e., any
        /// value starting with one of the characters y, n,
        /// t or f is also accepted. Return false if no 
        /// valid Boolean value can be extracted.
        /// </summary>
        public static bool GetTrueOrFalse(
          string s,
          out bool val)
        {
            val = false;

            if (s.Equals(Boolean.TrueString,
              StringComparison.OrdinalIgnoreCase))
            {
                val = true;
                return true;
            }
            if (s.Equals(Boolean.FalseString,
              StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            if (s.Equals("1"))
            {
                val = true;
                return true;
            }
            if (s.Equals("0"))
            {
                return true;
            }
            s = s.ToLower();

            if ('t' == s[0] || 'y' == s[0])
            {
                val = true;
                return true;
            }
            if ('f' == s[0] || 'n' == s[0])
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Select a specified file in the given folder.
        /// </summary>
        /// <param name="folder">Initial folder.</param>
        /// <param name="filename">Selected filename on 
        /// success.</param>
        /// <returns>Return true if a file was successfully 
        /// selected.</returns>
        static bool FileSelect(
          string folder,
          string title,
          string filter,
          ref string filename)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = title;
            dlg.CheckFileExists = true;
            dlg.CheckPathExists = true;
            dlg.InitialDirectory = folder;
            dlg.FileName = filename;
            dlg.Filter = filter;
            bool rc = (DialogResult.OK == dlg.ShowDialog());
            filename = dlg.FileName;
            return rc;
        }

        /// <summary>
        /// Select a WaveFront OBJ file in the given folder.
        /// </summary>
        /// <param name="folder">Initial folder.</param>
        /// <param name="filename">Selected filename on 
        /// success.</param>
        /// <returns>Return true if a file was successfully 
        /// selected.</returns>
        static public bool FileSelectObj(
          string folder,
          ref string filename)
        {
            return FileSelect(folder,
              "Select WaveFront OBJ file or Cancel to Exit",
              "WaveFront OBJ Files (*.obj)|*.obj",
              ref filename);
        }

        /// <summary>
        /// Return the size in bytes of the given file.
        /// </summary>
        static public long GetFileSize(string filename)
        {
            long fileSize = 0L;

            using (FileStream file = File.Open(
              filename, FileMode.Open))
            {
                fileSize = file.Seek(0L, SeekOrigin.End);
                file.Close();
            }
            return fileSize;
        }
    }
    public class Press
    {
        [DllImport("USER32.DLL")]
        public static extern bool PostMessage(
          IntPtr hWnd, uint msg, uint wParam, uint lParam);

        [DllImport("user32.dll")]
        static extern uint MapVirtualKey(
          uint uCode, uint uMapType);

        enum WH_KEYBOARD_LPARAM : uint
        {
            KEYDOWN = 0x00000001,
            KEYUP = 0xC0000001
        }

        public enum KEYBOARD_MSG : uint
        {
            WM_KEYDOWN = 0x100,
            WM_KEYUP = 0x101
        }

        enum MVK_MAP_TYPE : uint
        {
            VKEY_TO_SCANCODE = 0,
            SCANCODE_TO_VKEY = 1,
            VKEY_TO_CHAR = 2,
            SCANCODE_TO_LR_VKEY = 3
        }

        /// <summary>
        /// Post one single keystroke.
        /// </summary>
        static public void OneKey(IntPtr handle, char letter)
        {
            uint scanCode = MapVirtualKey(letter,
              (uint)MVK_MAP_TYPE.VKEY_TO_SCANCODE);

            uint keyDownCode = (uint)
              WH_KEYBOARD_LPARAM.KEYDOWN
              | (scanCode << 16);

            uint keyUpCode = (uint)
              WH_KEYBOARD_LPARAM.KEYUP
              | (scanCode << 16);

            PostMessage(handle,
              (uint)KEYBOARD_MSG.WM_KEYDOWN,
              letter, keyDownCode);

            PostMessage(handle,
              (uint)KEYBOARD_MSG.WM_KEYUP,
              letter, keyUpCode);
        }

        /// <summary>
        /// Post a sequence of keystrokes.
        /// </summary>
        public static void Keys(
          IntPtr revitHandle,
          string command)
        {
            //IntPtr revitHandle = System.Diagnostics.Process // 2018
            //  .GetCurrentProcess().MainWindowHandle; // 2018

            foreach (char letter in command)
            {
                OneKey(revitHandle, letter);
            }
        }
    }
}
