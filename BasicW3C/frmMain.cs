using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BasicW3C.Lists;
using BasicW3C.Tools;

namespace BasicW3C
{
    public partial class frmMain : Form
    {
        private static string _991LogFolder = @"C:\inetpub\logs\LogFiles\W3SVC1\";
        private HashSet<string> clients = new HashSet<string>();
        public frmMain()
        {
            InitializeComponent();
        }

        private void loadLatestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sites = IOHandler.GetSiteFolders();
            List<ValidLog> files = new List<ValidLog>();
            foreach (var site in sites)
            {
                files.AddRange(IOHandler.GetLogs(site));
            }
            List<string> lines = new List<string>();
            foreach (var log in files)
            {
                lines.AddRange(IOHandler.LogContents(log));
            }
            var logs = W3CParser.ParseContents(lines.ToArray());


            foreach (var log in logs)
            {
                if (CFTool.NeedsParsed(log.X_Forwarded_For)) log.X_Forwarded_For = CFTool.GetClientIP(log.X_Forwarded_For);
                if (log.X_Forwarded_For!=null& log.X_Forwarded_For!="-")
                {
                    if (!InList(log.X_Forwarded_For)) AddClient(log.X_Forwarded_For);
                }
                else if(!InList(log.ClientIP))
                {
                    AddClient(log.ClientIP);
                }
            }
        }

        private bool InList(string item)
        {
            if(item.Contains("37.49.230.100")) Debug.WriteLine("");
            if (clients.Contains(item)) return true;
            return false;
        }

        private void AddClient(string name)
        {
            lstClients.Items.Add(name);
            clients.Add(name);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
