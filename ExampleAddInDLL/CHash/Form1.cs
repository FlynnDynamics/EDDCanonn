using EDDiscovery.DLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CHash
{
    public partial class Form1 : Form
    {
        EDDDLLManager mgr = new EDDDLLManager();

        public Form1()
        {
            InitializeComponent();
        }

        public void RequestHistory(long index, bool isjid, out EDDDLLIF.JournalEntry f)
        {
            EDDDLLIF.JournalEntry nje = new EDDDLLIF.JournalEntry() { ver = 99, indexno = 19 };

            nje.utctime = "99/98/97";
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

            f = nje;
        }
        //public EDDDLLIF.Fred RequestHistory(long index, bool isjid)
        //{
        //    return new EDDDLLIF.Fred();//new EDDDLLIF.JournalEntry();

        //public void RequestHistory(long index, bool isjid, out EDDDLLIF.Fred f)
        //{
        //    System.Diagnostics.Debug.WriteLine("Request history " + index + " " + isjid);
        //    f = new EDDDLLIF.Fred() { a = 2022, b = "kwkw" } ;
        //    f.currentmissions = new string[] { "a", "b" };
        //}
        ////{


        //    return nje;
        //}

        public EDDDLLIF.EDDCallBacks callbacks = new EDDDLLIF.EDDCallBacks();

        private void button1_Click(object sender, EventArgs e)
        {
            if ( mgr.Count == 0 )
            {
                callbacks.ver = 1;
                callbacks.historycallback = RequestHistory;
                string r = mgr.Load(@"..\..\..\x64\debug", "1.2.3.4",@"c:\code", callbacks, "All");
                richTextBox1.Text += "DLL Loaded: " + r + Environment.NewLine;
            }
            else
                richTextBox1.Text += "Already loaded" + Environment.NewLine;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mgr.UnLoad();
            richTextBox1.Text += "DLL UnLoad" + Environment.NewLine;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EDDDLLIF.JournalEntry nje = new EDDDLLIF.JournalEntry() { ver = 99, indexno = 19 };

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

            mgr.NewJournalEntry(nje);

            richTextBox1.Text += "NJE" + Environment.NewLine;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mgr.ActionCommand("Win64DLL", "Command", new string[] { "one", "two", "three" });
            richTextBox1.Text += "Action" + Environment.NewLine;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            EDDDLLIF.JournalEntry nje = new EDDDLLIF.JournalEntry() { ver = 99, indexno = 19 };

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


            mgr.Refresh("Jameson", nje);
            richTextBox1.Text += "Refresh" + Environment.NewLine;

        }
    }
}
