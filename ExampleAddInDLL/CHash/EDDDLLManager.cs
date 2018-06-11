using EDDiscovery.DLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHash
{
    public class EDDDLLManager
    {
        public int Count { get { return dlls.Count; } }

        private List<EDDDLLCaller> dlls = new List<EDDDLLCaller>();

        public int Load(string directory, string ourversion)
        {
            if (!Directory.Exists(directory))
                return 0;

            FileInfo[] allFiles = Directory.EnumerateFiles(directory, "*.dll", SearchOption.TopDirectoryOnly).Select(f => new FileInfo(f)).OrderBy(p => p.LastWriteTime).ToArray();

            foreach (FileInfo f in allFiles)
            {
                EDDDLLCaller caller = new EDDDLLCaller();

                if (caller.Load(f.FullName, ourversion))        // if loaded, add to list
                {
                    dlls.Add(caller);
                }

            }

            return dlls.Count;
        }

        public void UnLoad()
        {
            foreach (EDDDLLCaller caller in dlls)
            {
                caller.UnLoad();
            }

            dlls.Clear();
        }

        public void Refresh(string cmdr, EDDDLLIF.JournalEntry je)
        {
            foreach (EDDDLLCaller caller in dlls)
            {
                caller.Refresh(cmdr, je);
            }
        }

        public void NewJournalEntry(EDDDLLIF.JournalEntry nje)
        {
            foreach (EDDDLLCaller caller in dlls)
            {
                caller.NewJournalEntry(nje);
            }
        }

        // NULL/False if no DLL found, or <string,true> if DLL found, string may be null if DLL does not implement action command
        public Tuple<string,bool> ActionCommand(string dllname, string cmd, string[] paras)     
        {
            foreach (EDDDLLCaller caller in dlls)
            {
                if (caller.Name.Equals(dllname, StringComparison.InvariantCultureIgnoreCase) || dllname.Equals("All",StringComparison.InvariantCultureIgnoreCase))
                {
                    return new Tuple<string,bool>(caller.ActionCommand(cmd, paras),true);
                }
            }

            return new Tuple<string, bool>(null, false);
        }
    }
}

