using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BasicW3C.Lists;

namespace BasicW3C.Tools
{
    public static class W3CParser
    {
        private static bool IsComment(string line)
        {
            if (line.Substring(0,1) == "#") return true;
            return false;
        }

        private static string[] ReadFile(string path)
        {
            return File.ReadAllLines(path);
        }
        private static W3CLog Parse(string line)
        {
            W3CLog tmp = new W3CLog();
            // Format is bellow
            //date time s-ip cs-method cs-uri-stem cs-uri-query s-port cs-username c-ip cs(User-Agent) cs(Referer) sc-status sc-substatus sc-win32-status time-taken X-Forwarded-For
            string[] split = line.Split(' ');
            tmp.Date = split[0];
            tmp.Time = split[1];
            tmp.ServerIP = split[2];
            tmp.Method = split[3];
            tmp.URIPath = split[4];
            tmp.URIQuery = split[5];
            tmp.ServerPort = split[6];
            tmp.UserProfile = split[7];
            tmp.ClientIP = split[8];
            tmp.UserAgent = split[9];
            tmp.ClientReferer = split[10];
            tmp.HTTPResponseCode = split[11]; // sc-status
            tmp.HTTPSubStatus = split[12];
            tmp.Win32Status = split[13];
            tmp.LoadingTime = split[14];
            if(split.Length==16) tmp.X_Forwarded_For = split[15]; // length is +1 for extra option i guess?

            return tmp;
        }

        private static List<W3CLog> GetLogList(string[] lines)
        {
            List<W3CLog> parsed = new List<W3CLog>();
            for (int i = 0; i < lines.Length; i++)
            {
                if (!IsComment(lines[i]))
                {
                    parsed.Add(Parse(lines[i]));
                }
                else
                {
                    //Ignore, it is a comment bois
                }
            }

            return parsed;
        }
        /// <summary>
        /// Will read and return all log items from a file directly.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<W3CLog> ParseLog(string path)
        {
            return GetLogList(ReadFile(path));
        }
        /// <summary>
        /// Will return all log items from a memory string array
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static List<W3CLog> ParseContents(string[] lines)
        {
            return GetLogList(lines);
        }
    }
}
