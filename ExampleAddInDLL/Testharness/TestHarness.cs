using EliteDangerousCore.DLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CHash
{
    public partial class TestHarness : Form
    {
        EDDDLLManager mgr = new EDDDLLManager();
        string csharpappdata = @"..\..\..\appdata";

        public TestHarness()
        {
            InitializeComponent();

            EDDDLLAssemblyFinder.AssemblyFindPath = csharpappdata;
            AppDomain.CurrentDomain.AssemblyResolve += EDDDLLAssemblyFinder.AssemblyResolve;
        }

        public bool RequestHistory(long index, bool isjid, out EDDDLLInterfaces.EDDDLLIF.JournalEntry f)
        {
            EDDDLLInterfaces.EDDDLLIF.JournalEntry nje = new EDDDLLInterfaces.EDDDLLIF.JournalEntry() { ver = 2, indexno = 19 };

            nje.utctime = DateTime.UtcNow.ToString();
            nje.name = "EventSummary!";
            nje.info = "Info";
            nje.detailedinfo = "DI";
            nje.materials = new string[2] { "one", "two" };
            nje.commodities = new string[2] { "c-one", "c-two" };
            nje.currentmissions = new string[2] { "m-one", "m-two" };
            nje.systemname = "Sys Fred";
            nje.x = 100.1;
            nje.y = 200.1;
            nje.z = 300.1;
            nje.travelleddistance = 1234.5;
            nje.travelledseconds = 6789;
            nje.islanded = true;
            nje.isdocked = true;
            nje.whereami = "Body";
            nje.shiptype = "Anaconda";
            nje.gamemode = "Open";
            nje.group = "Fred";
            nje.credits = 123456789;
            nje.eventid = "FunEvent";
            nje.currentmissions = new string[] { "M1", "M2" };
            nje.totalrecords = 2001;
            nje.jid = 101;
            nje.json = "{\"timestamp\"=\"10-20\"}";
            nje.cmdrname = "Buddy";
            nje.cmdrfid = "F19292";
            nje.shipident = "Y-1929";
            nje.shipname = "Julia";
            nje.hullvalue = 200000;
            nje.modulesvalue = 20000;
            nje.rebuy = 5000;
            nje.stored = false;

            f = nje;

            richTextBox1.Text += "Request history " + index + " " + isjid + Environment.NewLine;
            return true;
        }

        public bool RunAction(string eventname, string paras)
        {
            richTextBox1.Text += "Run action " + eventname + " " + paras + Environment.NewLine;
            return true;
        }

        public string GetShipLoadout(string shipnameorcurrent)
        {
            return "Ship JSON";
        }

        public EDDDLLInterfaces.EDDDLLIF.EDDCallBacks callbacks = new EDDDLLInterfaces.EDDDLLIF.EDDCallBacks();

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (mgr.Count == 0)
            {
                callbacks.ver = 2;
                callbacks.RequestHistory = RequestHistory;
                callbacks.RunAction = RunAction;
                callbacks.GetShipLoadout = GetShipLoadout;

                string[] options = new string[] { EDDDLLInterfaces.EDDDLLIF.FLAG_HOSTNAME + "EDLITE",
                                              EDDDLLInterfaces.EDDDLLIF.FLAG_JOURNALVERSION + "2",
                                              EDDDLLInterfaces.EDDDLLIF.FLAG_CALLBACKVERSION + "2",
                                            };
                
                //var r = mgr.Load(@"..\..\..\win64dll\bin\debug", "1.2.3.4", new string[] { "HOSTNAME=TESTHARNESS","JOURNALVERSION=2" }, @"c:\code", callbacks, "All");
                var r = mgr.Load(@"..\..\..\x64\debug", "1.2.3.4", options , callbacks, "All");
                richTextBox1.Text += "DLL Loaded: " + r.Item1 + Environment.NewLine;
                richTextBox1.Text += "DLL Failed: " + r.Item2 + Environment.NewLine;
                richTextBox1.Text += "DLL Not Allowed: " + r.Item3 + Environment.NewLine;

                var r2 = mgr.Load(csharpappdata, "1.2.3.4", options, callbacks, "All");
                richTextBox1.Text += "CSDLL Loaded: " + r2.Item1 + Environment.NewLine;
                richTextBox1.Text += "CSDLL Failed: " + r2.Item2 + Environment.NewLine;
                richTextBox1.Text += "CSDLL Not Allowed: " + r2.Item3 + Environment.NewLine;
            }
            else
                richTextBox1.Text += "Already loaded" + Environment.NewLine;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            mgr.UnLoad();
            richTextBox1.Text += "DLL UnLoad" + Environment.NewLine;
        }

        private void buttonNJE_Click(object sender, EventArgs e)
        {
            EDDDLLInterfaces.EDDDLLIF.JournalEntry nje = new EDDDLLInterfaces.EDDDLLIF.JournalEntry() { ver = 2, indexno = 19 };

            nje.utctime = DateTime.UtcNow.ToString();
            nje.name = "EventSummary";
            nje.info = "Info";
            nje.detailedinfo = "DI";
            nje.materials = new string[2] { "one", "two" };
            nje.commodities = new string[2] { "c-one", "c-two" };
            nje.currentmissions = new string[2] { "m-one", "m-two" };
            nje.systemname = "Sys Fred";
            nje.x = 100.1;
            nje.y = 200.1;
            nje.z = 300.1;
            nje.travelleddistance = 1234.5;
            nje.travelledseconds = 6789;
            nje.islanded = true;
            nje.isdocked = true;
            nje.whereami = "Body";
            nje.shiptype = "Anaconda";
            nje.gamemode = "Open";
            nje.group = "Fred";
            nje.credits = 123456789;
            nje.eventid = "FunEvent";
            nje.totalrecords = 2001;
            nje.jid = 101;
            nje.json = "{\"timestamp\"=\"10-20\"}";
            nje.cmdrname = "Buddy";
            nje.cmdrfid = "F19292";
            nje.shipident = "Y-1929";
            nje.shipname = "Julia";
            nje.hullvalue = 200000;
            nje.modulesvalue = 20000;
            nje.rebuy = 5000;
            nje.stored = false;

            mgr.NewJournalEntry(nje, false);

            richTextBox1.Text += "NJE" + Environment.NewLine;
        }

        private void buttonAction_Click(object sender, EventArgs e)
        {
            mgr.ActionCommand("All", "Command", new string[] { "one", "two", "three" });
            richTextBox1.Text += "Action" + Environment.NewLine;
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            EDDDLLInterfaces.EDDDLLIF.JournalEntry nje = new EDDDLLInterfaces.EDDDLLIF.JournalEntry() { ver = 2, indexno = 19 };

            nje.utctime = "01/02/03";
            nje.name = "EventSummary";
            nje.info = "Info";
            nje.detailedinfo = "DI";
            nje.materials = new string[2] { "one", "two" };
            nje.commodities = new string[2] { "c-one", "c-two" };
            nje.currentmissions = new string[2] { "m-one", "m-two" };
            nje.systemname = "Sys Fred";
            nje.x = 100.1;
            nje.y = 200.1;
            nje.z = 300.1;
            nje.travelleddistance = 1234.5;
            nje.travelledseconds = 6789;
            nje.islanded = true;
            nje.isdocked = true;
            nje.whereami = "Body";
            nje.shiptype = "Anaconda";
            nje.gamemode = "Open";
            nje.group = "Fred";
            nje.credits = 123456789;
            nje.totalrecords = 2001;
            nje.jid = 101;
            nje.json = "{\"timestamp\"=\"10-20\"}";


            mgr.Refresh("Jameson", nje);
            richTextBox1.Text += "Refresh" + Environment.NewLine;

        }

        private void buttonConfig_Click(object sender, EventArgs e)
        {
            var caller = mgr.FindCaller("CSharpDLL");
            if ( caller != null)
            {
                string[] config = caller.GetConfig();
                if (config != null)
                {
                    foreach (var x in config)
                        richTextBox1.AppendText("C:" + x + Environment.NewLine);
                }
                else
                    richTextBox1.AppendText("No config" + Environment.NewLine);
            }
        }

        private void buttonAJE_Click(object sender, EventArgs e)
        {
            EDDDLLInterfaces.EDDDLLIF.JournalEntry nje = new EDDDLLInterfaces.EDDDLLIF.JournalEntry() { ver = 2, indexno = 19 };

            nje.utctime = DateTime.UtcNow.ToString();
            nje.name = "EventSummary";
            nje.info = "Info";
            nje.detailedinfo = "DI";
            nje.materials = new string[2] { "one", "two" };
            nje.commodities = new string[2] { "c-one", "c-two" };
            nje.currentmissions = new string[2] { "m-one", "m-two" };
            nje.systemname = "Sys Fred";
            nje.x = 100.1;
            nje.y = 200.1;
            nje.z = 300.1;
            nje.travelleddistance = 1234.5;
            nje.travelledseconds = 6789;
            nje.islanded = true;
            nje.isdocked = true;
            nje.whereami = "Body";
            nje.shiptype = "Anaconda";
            nje.gamemode = "Open";
            nje.group = "Fred";
            nje.credits = 123456789;
            nje.eventid = "FunEvent";
            nje.totalrecords = 2001;
            nje.jid = 101;
            nje.json = "{\"timestamp\"=\"10-20\"}";
            nje.cmdrname = "Buddy";
            nje.cmdrfid = "F19292";
            nje.shipident = "Y-1929";
            nje.shipname = "Julia";
            nje.hullvalue = 200000;
            nje.modulesvalue = 20000;
            nje.rebuy = 5000;
            nje.stored = false;

            mgr.ActionJournalEntry("All",nje);

            richTextBox1.Text += "Action JE" + Environment.NewLine;

        }

        private void buttonUIEvent_Click(object sender, EventArgs e)
        {
            mgr.NewUIEvent("Test json");
        }
    }
}
