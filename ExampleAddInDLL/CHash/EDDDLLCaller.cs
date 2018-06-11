using EDDiscovery.DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CHash
{
    public class EDDDLLCaller
    {
        public string Version { get; private set; }
        public string Name { get; private set; }

        private IntPtr pDll = IntPtr.Zero;
        private IntPtr pNewJournalEntry = IntPtr.Zero;
        private IntPtr pActionCommand = IntPtr.Zero;

        public bool Load(string path, string ourversion)
        {
            if (pDll == IntPtr.Zero)
            {
                pDll = NativeMethods.LoadLibrary(path);

                if (pDll != IntPtr.Zero)
                {
                    IntPtr peddinit = NativeMethods.GetProcAddress(pDll, "EDDInitialise");

                    if (peddinit != IntPtr.Zero)        // must have this to be an EDD DLL
                    {
                        Name = System.IO.Path.GetFileNameWithoutExtension(path);

                        EDDDLLIF.EDDInitialise edinit = (EDDDLLIF.EDDInitialise)Marshal.GetDelegateForFunctionPointer(
                                                                                            peddinit,
                                                                                            typeof(EDDDLLIF.EDDInitialise));
                        Version = edinit(ourversion);

                        pNewJournalEntry = NativeMethods.GetProcAddress(pDll, "EDDNewJournalEntry");
                        pActionCommand = NativeMethods.GetProcAddress(pDll, "EDDActionCommand");

                        bool ok = Version != null && Version.Length > 0;

                        if (!ok)
                        {
                            NativeMethods.FreeLibrary(pDll);
                            pDll = IntPtr.Zero;
                        }

                        return pDll != IntPtr.Zero;
                    }
                }
            }

            return false;
        }

        public bool UnLoad()
        {
            if (pDll != IntPtr.Zero)
            {
                IntPtr pAddressOfFunctionToCall = NativeMethods.GetProcAddress(pDll, "EDDTerminate");

                if (pAddressOfFunctionToCall != IntPtr.Zero)
                {
                    EDDDLLIF.EDDTerminate edf = (EDDDLLIF.EDDTerminate)Marshal.GetDelegateForFunctionPointer(
                                                                                        pAddressOfFunctionToCall,
                                                                                        typeof(EDDDLLIF.EDDTerminate));
                    edf();
                }

                NativeMethods.FreeLibrary(pDll);
                pDll = IntPtr.Zero;
                Version = null;
                return true;
            }

            return false;
        }

        public bool Refresh(string cmdr , EDDDLLIF.JournalEntry je)
        {
            if (pDll != IntPtr.Zero)
            {
                IntPtr pAddressOfFunctionToCall = NativeMethods.GetProcAddress(pDll, "EDDRefresh");

                if (pAddressOfFunctionToCall != IntPtr.Zero)
                {
                    EDDDLLIF.EDDRefresh edf = (EDDDLLIF.EDDRefresh)Marshal.GetDelegateForFunctionPointer(
                                                                                        pAddressOfFunctionToCall,
                                                                                        typeof(EDDDLLIF.EDDRefresh));
                    edf(cmdr,je);
                    return true;
                }
            }

            return false;
        }

        public bool NewJournalEntry(EDDDLLIF.JournalEntry nje)
        {
            if (pDll != IntPtr.Zero && pNewJournalEntry != IntPtr.Zero)
            {
                EDDDLLIF.EDDNewJournalEntry edf = (EDDDLLIF.EDDNewJournalEntry)Marshal.GetDelegateForFunctionPointer(
                                                                                    pNewJournalEntry,
                                                                                    typeof(EDDDLLIF.EDDNewJournalEntry));
                edf(nje);
                return true;
            }

            return false;
        }

        public string ActionCommand(string cmd, string[] paras)
        {
            if (pDll != IntPtr.Zero && pActionCommand != IntPtr.Zero)
            {
                EDDDLLIF.EDDActionCommand edf = (EDDDLLIF.EDDActionCommand)Marshal.GetDelegateForFunctionPointer(
                                                                                    pActionCommand,
                                                                                    typeof(EDDDLLIF.EDDActionCommand));
                return edf(cmd,paras);
            }

            return null;
        }

    }
}
