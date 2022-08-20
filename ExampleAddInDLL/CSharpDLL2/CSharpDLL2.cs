using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDLL2
{
    public class CSharpDLL2EDDClass
    {
        public CSharpDLL2EDDClass()
        {
            System.Diagnostics.Debug.WriteLine("CSharpDLL2 Made DLL instance");
        }

        public string EDDInitialise(string vstr, string dllfolder, EDDDLLInterfaces.EDDDLLIF.EDDCallBacks cb)
        {
            System.Diagnostics.Debug.WriteLine("CSharpDLL2 Init func " + vstr + " " + dllfolder);
            System.IO.File.AppendAllText(@"c:\code\csharpdll.txt", Environment.NewLine + "Init " + vstr + " in " + dllfolder + Environment.NewLine);
            return "1.0.0.0";
        }


        public void EDDTerminate()
        {
            System.Diagnostics.Debug.WriteLine("CSharpDLL2 Unload");
        }
    }
}
