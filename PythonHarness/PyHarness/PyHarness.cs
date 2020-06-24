using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PyHarness
{
    public class PyHarnessEDDClass      // EDDClass marks this as type to instance
    {
        private string storedout;
        private string currentout;

        public PyHarnessEDDClass()
        {
            System.Diagnostics.Debug.WriteLine("Made DLL instance of PyHarness");
        }

        EDDDLLInterfaces.EDDDLLIF.EDDCallBacks callbacks;

        public string EDDInitialise(string vstr, string dllfolderp, EDDDLLInterfaces.EDDDLLIF.EDDCallBacks cb)
        {
            string[] vopts = vstr.Split(';');
            int jv = vopts.ContainsIn("JOURNALVERSION=2");
            if (jv == -1 || vopts[jv].Substring(15).InvariantParseInt(0) < 2)       // check journal version exists and is at 2 mininum
                return "!PY Harness requires a more recent host program";

            System.Diagnostics.Debug.WriteLine("Init func " + vstr + " " + dllfolderp);
            callbacks = cb;
            storedout = Path.Combine(dllfolderp, "Stored.log");
            currentout = Path.Combine(dllfolderp, "Current.log");
            BaseUtils.DeleteFileNoError(storedout);
            BaseUtils.DeleteFileNoError(currentout);
            return "1.0.0.0;PLAYLASTFILELOAD";
        }

        public void EDDTerminate()
        {
            System.Diagnostics.Debug.WriteLine("Unload");
            BaseUtils.DeleteFileNoError(storedout);
            BaseUtils.DeleteFileNoError(currentout);
        }

        public void EDDNewJournalEntry(EDDDLLInterfaces.EDDDLLIF.JournalEntry je)
        {
            System.Diagnostics.Debug.WriteLine("PY New Journal Entry " + je.utctime);
            string filetoadd = je.stored ? storedout : currentout;
            var s = File.AppendText(filetoadd);
            s.WriteLine(je.json);
            s.Close();
        }

    }
}
