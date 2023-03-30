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
                // make sure panel unique id and winref name is based on a producer-panel naming system to make it unique
                string uniquename = "CSharpDLLPanel-Demo1";
                cb.AddPanel(uniquename, typeof(DemoUserControl.DemonstrationUserControl), "DLLUC-1", uniquename, "UC DLL Demo user panel", CSharpDLLPanel.Properties.Resources.CaptainsLog);
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
