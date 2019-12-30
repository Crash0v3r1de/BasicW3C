using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicW3C.Lists
{
    public class W3CLog
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public string ServerIP { get; set; }
        public string Method { get; set; }
        public string URIPath { get; set; }
        public string URIQuery { get; set; }
        public string ServerPort { get; set; }
        public string UserProfile { get; set; }
        public string ClientIP { get; set; }
        public string UserAgent { get; set; }
        public string ClientReferer { get; set; }
        public string HTTPResponseCode { get; set; }
        public string HTTPSubStatus { get; set; }
        public string Win32Status { get; set; }
        public string LoadingTime { get; set; }
        public string X_Forwarded_For { get; set; }
    }
}
