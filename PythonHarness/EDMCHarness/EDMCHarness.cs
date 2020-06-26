using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace PyHarness
{
    public class EDMCHarnessEDDClass      // EDDClass marks this as type to instance.  Names of members follow EDDInterfaces names
    {
        private string storedout;
        private string currentout;
        private string uiout;

        public EDMCHarnessEDDClass()
        {
            System.Diagnostics.Debug.WriteLine("Made DLL instance of PyHarness");
        }

        EDDDLLInterfaces.EDDDLLIF.EDDCallBacks callbacks;
        Process pyharness;

        public string EDDInitialise(string vstr, string dllfolderp, EDDDLLInterfaces.EDDDLLIF.EDDCallBacks cb)
        {
            string[] vopts = vstr.Split(';');
            int jv = vopts.ContainsIn("JOURNALVERSION=2");
            if (jv == -1 || vopts[jv].Substring(15).InvariantParseInt(0) < 2)       // check journal version exists and is at 2 mininum
                return "!PY Harness requires a more recent host program";

            string EDMCAppFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EDD-EDMC");

            if (!Directory.Exists(EDMCAppFolder))
                return "!PY Harness EDD-EDMC folder not found";

            string py = BaseUtils.PythonLaunch.PythonLauncher();

            if (py == null)
                return "!PY Harness Can't find a python launcher";

            string startscript = "eddedmc.py";

            string pyscript = Path.Combine(EDMCAppFolder, startscript);
            if (!File.Exists(pyscript))
                return "!PY Harness Can't find the script";

            System.Diagnostics.Debug.WriteLine("Init func " + vstr + " " + dllfolderp);

            callbacks = cb;
            storedout = Path.Combine(EDMCAppFolder, "stored.edd");
            currentout = Path.Combine(EDMCAppFolder, "current.edd");
            uiout = Path.Combine(EDMCAppFolder, "ui.edd");

            BaseUtilsHelpers.DeleteFileNoError(storedout);
            BaseUtilsHelpers.DeleteFileNoError(currentout);
            BaseUtilsHelpers.DeleteFileNoError(uiout);

            pyharness = new Process();
            pyharness.StartInfo.FileName = py;
            pyharness.StartInfo.Arguments = pyscript;
            pyharness.StartInfo.WorkingDirectory = EDMCAppFolder;
            bool started = pyharness.Start();

            if (!started)
            {
                pyharness.Dispose();
                pyharness = null;
                return "!PY Harness could not start script";
            }

            System.Diagnostics.Debug.WriteLine("EDMC Harness started");
            return "1.0.0.0;PLAYLASTFILELOAD";
        }

        public void EDDTerminate()
        {
            System.Diagnostics.Debug.WriteLine("Unload EDMC Harness");
            BaseUtilsHelpers.DeleteFileNoError(storedout);
            BaseUtilsHelpers.DeleteFileNoError(currentout);
            BaseUtilsHelpers.DeleteFileNoError(uiout);

            if ( pyharness != null )
            {
                if (!pyharness.HasExited)
                {
                    pyharness.CloseMainWindow();
                    pyharness.WaitForExit(10000);
                }

                pyharness.Dispose();
                pyharness = null;
            }
            System.Diagnostics.Debug.WriteLine("Unloaded EDMC Harness");
        }

        public void EDDRefresh(string cmd, EDDDLLInterfaces.EDDDLLIF.JournalEntry lastje)
        {
            System.Diagnostics.Debug.WriteLine("EDMC Refresh");

            string f = string.Format("{{ \"timestamp\":\"{0}\", \"event\":\"RefreshOver\"}}", DateTime.UtcNow.Truncate(TimeSpan.TicksPerSecond).ToStringZulu());
            var s = File.AppendText(storedout);
            s.WriteLine(f);
            s.Close();
        }

        public void EDDNewJournalEntry(EDDDLLInterfaces.EDDDLLIF.JournalEntry je)
        {
            System.Diagnostics.Debug.WriteLine("EDMC New Journal Entry " + je.utctime);
            string filetoadd = je.stored ? storedout : currentout;
            var s = File.AppendText(filetoadd);
            s.WriteLine(je.json);
            s.Close();
        }

        public void EDDNewUIEvent(string json)
        {
            System.Diagnostics.Debug.WriteLine("EDMC New UI Event " + json);
            var s = File.AppendText(uiout);
            s.WriteLine(json);
            s.Close();
        }

    }
}
