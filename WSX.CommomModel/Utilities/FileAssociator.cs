using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.Utilities
{
    /// <summary>
    /// Demo:
    /// var info = new FileTypeRegInfo()
    /// {
    ///     ExtendName = ".wsx",
    ///     Description = "Project File",
    ///     IcoPath = Application.StartupPath + @"\1.ico",
    ///     ExePath = @"E:\WSX\Project\WSXExpert\WSXExpert\bin\x86\Debug\WSXExpert.exe"
    /// };
    /// FileAssociator.RegisterFileType(info);
    /// </summary>
    public static class FileAssociator
    {
        public static void RegisterFileType(FileTypeRegInfo regInfo)
        {
            //if (FileTypeRegistered(regInfo.ExtendName))
            //{
            //    return;
            //}

            string relationName = regInfo.ExtendName.Substring(1, regInfo.ExtendName.Length - 1).ToUpper() + "_FileType";

            RegistryKey fileTypeKey = Registry.ClassesRoot.CreateSubKey(regInfo.ExtendName);
            fileTypeKey.SetValue("", relationName);
            fileTypeKey.Close();

            RegistryKey relationKey = Registry.ClassesRoot.CreateSubKey(relationName);
            relationKey.SetValue("", regInfo.Description);

            RegistryKey iconKey = relationKey.CreateSubKey("DefaultIcon");
            iconKey.SetValue("", regInfo.IcoPath);

            RegistryKey shellKey = relationKey.CreateSubKey("Shell");
            RegistryKey openKey = shellKey.CreateSubKey("Open");
            RegistryKey commandKey = openKey.CreateSubKey("Command");
            commandKey.SetValue("", regInfo.ExePath + " %1");

            relationKey.Close();
        }

        public static FileTypeRegInfo GetFileTypeRegInfo(string extendName)
        {
            if (!FileTypeRegistered(extendName))
            {
                return null;
            }

            FileTypeRegInfo regInfo = new FileTypeRegInfo(extendName);

            string relationName = extendName.Substring(1, extendName.Length - 1).ToUpper() + "_FileType";
            RegistryKey relationKey = Registry.ClassesRoot.OpenSubKey(relationName);
            regInfo.Description = relationKey.GetValue("").ToString();

            RegistryKey iconKey = relationKey.OpenSubKey("DefaultIcon");
            regInfo.IcoPath = iconKey.GetValue("").ToString();

            RegistryKey shellKey = relationKey.OpenSubKey("Shell");
            RegistryKey openKey = shellKey.OpenSubKey("Open");
            RegistryKey commandKey = openKey.OpenSubKey("Command");
            string temp = commandKey.GetValue("").ToString();
            regInfo.ExePath = temp.Substring(0, temp.Length - 3);

            return regInfo;
        }

        public static bool UpdateFileTypeRegInfo(FileTypeRegInfo regInfo)
        {
            if (!FileTypeRegistered(regInfo.ExtendName))
            {
                return false;
            }


            string extendName = regInfo.ExtendName;
            string relationName = extendName.Substring(1, extendName.Length - 1).ToUpper() + "_FileType";
            RegistryKey relationKey = Registry.ClassesRoot.OpenSubKey(relationName, true);
            relationKey.SetValue("", regInfo.Description);

            RegistryKey iconKey = relationKey.OpenSubKey("DefaultIcon", true);
            iconKey.SetValue("", regInfo.IcoPath);

            RegistryKey shellKey = relationKey.OpenSubKey("Shell");
            RegistryKey openKey = shellKey.OpenSubKey("Open");
            RegistryKey commandKey = openKey.OpenSubKey("Command", true);
            commandKey.SetValue("", regInfo.ExePath + " %1");

            relationKey.Close();

            return true;
        }

        public static bool FileTypeRegistered(string extendName)
        {
            RegistryKey softwareKey = Registry.ClassesRoot.OpenSubKey(extendName);
            if (softwareKey != null)
            {
                return true;
            }

            return false;
        }
    }
    public class FileTypeRegInfo
    {
        public string ExtendName { get; set; }  //例如：".wxd,.cxd"
        public string Description { get; set; } //"WXS项目文件"
        public string IcoPath { get; set; }
        public string ExePath { get; set; }

        public FileTypeRegInfo()
        {

        }

        public FileTypeRegInfo(string extName)
        {
            ExtendName = extName;
        }
    }
}
