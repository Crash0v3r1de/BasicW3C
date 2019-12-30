using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BasicW3C.Lists;

namespace BasicW3C.Tools
{
    public static class IOHandler
    {
        private static string LogParentPath = @"C:\inetpub\logs\LogFiles";
        private static bool IsFTP(string name)
        {
            if (name.Contains("FTP")) return true;
            return false;
        }
        private static string[] UsableFolders(bool includeFTP)
        {
            List<string> tmp = new List<string>();
            var dirs = Directory.GetDirectories(LogParentPath);
            if (includeFTP)
            {
                foreach (var dir in dirs)
                {
                    tmp.Add(LogParentPath+"\\"+dir);
                }
            }
            else
            {
                foreach (var dir in dirs)
                {
                    if(!IsFTP(dir)) tmp.Add(dir);
                }
            }

            return tmp.ToArray();
        }
        private static string[] UsableFiles(string folder)
        {
            return Directory.GetFiles(folder);
        }
        private static string[] LogLines(string log)
        {
            return File.ReadAllLines(log);
        }
        private static string GetDate(string raw)
        {
            var tmp = Regex.Match(raw, "u_ex(.*?)\\.").Groups[1].Value;
            tmp = tmp.Replace("_x", ""); // remove additional useless data if present
            return tmp;
        }
        private static DateTime ExtractedDate(string name)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            var tmp = GetDate(name);
            string format = "yyMMdd";
            var date = DateTime.ParseExact(tmp, format,provider);
            return date;
        }
        private static ValidLog ParseLogInfo(string log)
        {
            ValidLog tmp = new ValidLog();
            tmp.FileName = new FileInfo(log).Name;
            tmp.FullPath = log;
            tmp.LogDay = ExtractedDate(log);
            return tmp;
        }
        private static List<ValidLog> GetAllLogInfo(string folder)
        {
            var logs = UsableFiles(folder);
            List<ValidLog> tmp = new List<ValidLog>();
            foreach (var file in logs)
            {
             tmp.Add(ParseLogInfo(file));   
            }

            return tmp;
        }

        public static string[] GetSiteFolders(bool includeFTP = false)
        {
            return UsableFolders(includeFTP);
        }

        public static List<ValidLog> GetLogs(string folder)
        {
            return GetAllLogInfo(folder);
        }

        public static string[] LogContents(ValidLog log)
        {
            return LogLines(log.FullPath);
        }
    }
}
