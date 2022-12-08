using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDLLPanel
{
    public class CSharpDLLPanelEDDClass
    {
        public CSharpDLLPanelEDDClass()
        {
            System.Diagnostics.Debug.WriteLine("CSharpDLLPanel Made DLL instance");
        }

        public string EDDInitialise(string vstr, string dllfolder, EDDDLLInterfaces.EDDDLLIF.EDDCallBacks cb)
        {
            System.Diagnostics.Debug.WriteLine("CSharpDLLPanel Init func " + vstr + " " + dllfolder);
            if ( cb.ver>=3 && cb.AddPanel != null)
            {
                cb.AddPanel("CSharpDLLPanel-Example-0.0.1", typeof(EliteDangerous.DLL.DemonstrationUserControl), "CSDemo1", "CSDemo1", "CS Demo user panel", Properties.Resources.CaptainsLog);
            }
            return "1.0.0.0";
        }

        public void EDDTerminate()
        {
            System.Diagnostics.Debug.WriteLine("CSharpDLLPanel Unload");
        }
    }
}
