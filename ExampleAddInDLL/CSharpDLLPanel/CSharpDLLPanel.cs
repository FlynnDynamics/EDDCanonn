using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDLLPanel2
{
    public class CSharpDLLPanelEDDClass
    {
        public static EDDDLLInterfaces.EDDDLLIF.EDDCallBacks DLLCallBack;

        public CSharpDLLPanelEDDClass()
        {
            System.Diagnostics.Debug.WriteLine("CSharpDLLPanel2 Made DLL instance");
        }

        public string EDDInitialise(string vstr, string dllfolder, EDDDLLInterfaces.EDDDLLIF.EDDCallBacks cb)
        {
            DLLCallBack = cb;
            System.Diagnostics.Debug.WriteLine("CSharpDLLPanel2 Init func " + vstr + " " + dllfolder);
            if ( cb.ver>=3 && cb.AddPanel != null)
            {
                cb.AddPanel("CSharpDLLPanel-Example-0.0.1", typeof(DemoUserControl.DemonstrationUserControl), "CSDemo1A", "CSDemo1A", "CS Demo user panel", CSharpDLLPanel.Properties.Resources.CaptainsLog);
                //cb.AddPanel("CSharpDLLPanel-Example-0.0.2", typeof(DemoUserControl.DemonstrationUserControl), "CSDemo2", "CSDemo2", "CS Demo 2 user panel", CSharpDLLPanel.Properties.Resources.CaptainsLog);
            }
            return "1.0.0.0";
        }

        public void EDDTerminate()
        {
            System.Diagnostics.Debug.WriteLine("CSharpDLLPanel Unload");
        }
        public void EDDDataResult(object requesttag, object usertag, string data)
        {
            DemoUserControl.DemonstrationUserControl uc = usertag as DemoUserControl.DemonstrationUserControl;
            uc.DataResult(data);
        }
    }
}
