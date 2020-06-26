using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Dependency;

namespace CSharpDLL
{
    public class EDDClass
    {
        public EDDClass()
        {
            System.Diagnostics.Debug.WriteLine("Made DLL instance");
            var x = new Class1();
        }

        EDDDLLInterfaces.EDDDLLIF.EDDCallBacks callbacks;

        public string EDDInitialise(string vstr, string dllfolder, EDDDLLInterfaces.EDDDLLIF.EDDCallBacks cb)
        {
            System.Diagnostics.Debug.WriteLine("Init func " + vstr + " " + dllfolder);
            callbacks = cb;
            return "1.0.0.0";
        }

        public void EDDTerminate()
        {
            System.Diagnostics.Debug.WriteLine("Unload");
        }

        public void EDDRefresh(string cmdname, EDDDLLInterfaces.EDDDLLIF.JournalEntry lastje)
        {
            System.Diagnostics.Debug.WriteLine("Refresh");
        }

        public void EDDNewJournalEntry(EDDDLLInterfaces.EDDDLLIF.JournalEntry je)
        {
            System.Diagnostics.Debug.WriteLine("New Journal Entry " + je.utctime);
        }

        public string EDDActionCommand(string cmdname, string[] paras)
        {
            System.Diagnostics.Debug.WriteLine("EDD Action Command");
            return "";
        }

        public void EDDActionJournalEntry(EDDDLLInterfaces.EDDDLLIF.JournalEntry je)
        {
            System.Diagnostics.Debug.WriteLine("EDD Action journal entry");
        }

        public void EDDNewUIEvent(string json)
        {
            System.Diagnostics.Debug.WriteLine("EDD UI Event" + json);
        }

        public string[] EDDConfigParameters(string[] values)
        {
            return new string[] { "One", "One-Value", "string" };
        }


    }
}
