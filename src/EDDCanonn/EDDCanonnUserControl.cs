using System;
using static EDDDLLInterfaces.EDDDLLIF;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace EDDCanonn
{
    partial class EDDCanonnUserControl : UserControl, IEDDPanelExtension
    {

        private EDDPanelCallbacks PanelCallBack;
        private EDDDLLInterfaces.EDDDLLIF.EDDCallBacks DLLCallBack;

        public EDDCanonnUserControl()
        {
           InitializeComponent();
        }

        //###### IEDDPanelExtension ######

        public bool SupportTransparency => false;

        public bool DefaultTransparent => false;

        public bool AllowClose()
        {
            return true;
        }

        public void Closing()
        {
            throw new NotImplementedException();
        }

        public void ControlTextVisibleChange(bool on)
        {
            throw new NotImplementedException();
        }

        public string HelpKeyOrAddress()
        {
            throw new NotImplementedException();
        }

        public void HistoryChange(int count, string commander, bool beta, bool legacy)
        {
            throw new NotImplementedException();
        }

        public void InitialDisplay()
        {
            throw new NotImplementedException();
        }

        public void Initialise(EDDPanelCallbacks callbacks, int displayid, string themeasjson, string configuration)
        {
            DLLCallBack = EDDCanonn.EDDCanonnClass.DLLCallBack;
            this.PanelCallBack = callbacks;
        }

        public void LoadLayout()
        {
            throw new NotImplementedException();
        }

        public void NewFilteredJournal(JournalEntry je)
        {
            throw new NotImplementedException();
        }

        public void NewTarget(Tuple<string, double, double, double> target)
        {
            throw new NotImplementedException();
        }

        public void NewUIEvent(string jsonui)
        {
            throw new NotImplementedException();
        }

        public void NewUnfilteredJournal(JournalEntry je)
        {
            throw new NotImplementedException();
        }

        public void ScreenShotCaptured(string file, Size s)
        {
            throw new NotImplementedException();
        }

        public void SetTransparency(bool ison, Color curcol)
        {
            this.BackColor = curcol;
        }

        public void ThemeChanged(string themeasjson)
        {
            throw new NotImplementedException();
        }

        public void TransparencyModeChanged(bool on)
        {
            throw new NotImplementedException();
        }

        void IEDDPanelExtension.CursorChanged(JournalEntry je)
        {
            throw new NotImplementedException();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // EDDCanonnUserControl
            // 
            this.Name = "EDDCanonnUserControl";
            this.Load += new System.EventHandler(this.EDDCanonnUserControl_Load);
            this.ResumeLayout(false);

        }

        private void EDDCanonnUserControl_Load(object sender, EventArgs e)
        {

        }

        //###### IEDDPanelExtension ######


    }
}
