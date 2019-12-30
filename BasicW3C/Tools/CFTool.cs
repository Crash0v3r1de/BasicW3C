using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicW3C.Tools
{
    public static class CFTool
    {
        public static bool NeedsParsed(string ip)
        {
            if (ip.Contains(",+")) return true;
            return false;
        }

        public static string GetClientIP(string ip)
        {
            var split = ip.Replace("+", "").Split(',');
            return split[0];
        }
    }
}
