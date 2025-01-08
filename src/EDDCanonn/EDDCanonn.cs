
using System.Diagnostics;

namespace EDDCanonn
{
    public class EDDCanonnClass
    {
        public static EDDDLLInterfaces.EDDDLLIF.EDDCallBacks DLLCallBack;

        public EDDCanonnClass()
        {
            Debug.WriteLine("EDDCanonn Made DLL instance");
        }

        public string EDDInitialise(string vstr, string dllfolder, EDDDLLInterfaces.EDDDLLIF.EDDCallBacks cb)
        {
            DLLCallBack = cb;
            Debug.WriteLine("EDDCanonn Init func " + vstr + " " + dllfolder);

            if (cb.ver >= 3 && cb.AddPanel != null)
            {
                string uniqueName = "EDDCanonn";
                cb.AddPanel(uniqueName, typeof(EDDCanonnUserControl), "Canonn", uniqueName, "Canonn", Properties.Resources.canonn);
            }
            else
            {
                Debug.WriteLine("Panel registration failed: Incompatible version or AddPanel is null");
            }

            return "1.0.0.0"; 

        }

        public void EDDTerminate()
        {
            Debug.WriteLine("CSharpDLLPanel Unload");
        }

    }
}
