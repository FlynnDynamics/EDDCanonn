using System;
using static EDDDLLInterfaces.EDDDLLIF;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using QuickJSON;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;

namespace EDDCanonn
{
    partial class EDDCanonnUserControl : UserControl, IEDDPanelExtension
    {
        private CanonnDataHandler dataHandler;
        private EDDPanelCallbacks PanelCallBack;
        private EDDCallBacks DLLCallBack;


        public EDDCanonnUserControl()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            dataHandler = new CanonnDataHandler();
            InitializeWhitelist();
        }


        #region WhiteList
        private void InitializeWhitelist()
        {
            try
            {
                dataHandler.FetchCanonnAsync(CanonnHelper.WhitelistUrl,
                jsonResponse =>
                {
                    try
                    {
                        JArray whitelistItems = jsonResponse.JSONParse().Array();

                        if (whitelistItems != null)
                        {
                            foreach (JObject item in whitelistItems)
                            {
                                string definition = item["definition"].Str();
                                if (string.IsNullOrEmpty(definition))
                                {
                                    Console.Error.WriteLine("Skipping empty definition.");
                                    continue;
                                }

                                try
                                {
                                    JObject definitionObject = definition.JSONParse().Object();
                                    AddToWhitelist(definitionObject);
                                }
                                catch (Exception ex)
                                {
                                    Console.Error.WriteLine($"Error parsing definition: {definition}. Exception: {ex.Message}");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Error processing whitelist: {ex.Message}");
                    }
                },
                ex =>
                {
                    Console.Error.WriteLine($"Error fetching whitelist: {ex.Message}");
                });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Unexpected error in InitializeWhitelist: {ex.Message}");
            }
        }

        private void AddToWhitelist(JObject definitionObject)
        {
           
        }
        #endregion


        #region IEDDPanelExtension
        public bool SupportTransparency => false;

        public bool DefaultTransparent => false;

        public bool AllowClose()
        {
            return true;
        }

        public void Closing()
        {
            Task.WaitAll(dataHandler._tasks.ToArray());
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
           
        }

        public void InitialDisplay()
        {

        }

        public void Initialise(EDDPanelCallbacks callbacks, int displayid, string themeasjson, string configuration)
        {
            DLLCallBack = EDDCanonnEDDClass.DLLCallBack;
            PanelCallBack = callbacks;
        }

        public void LoadLayout()
        {

        }

        public void NewFilteredJournal(JournalEntry je)
        {
            //    eventOutput.AppendText(je.eventid + Environment.NewLine);
            //    eventOutput.AppendText(je.json + Environment.NewLine);
            //    eventOutput.AppendText(IsJournalEntryAllowed(je) + Environment.NewLine);
            //    eventOutput.AppendText("#########################" + Environment.NewLine);
        }

        public void NewTarget(Tuple<string, double, double, double> target)
        {
            eventOutput.AppendText(target + Environment.NewLine);
        }

        public void NewUIEvent(string jsonui)
        {

        }

        public void NewUnfilteredJournal(JournalEntry je)
        {

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

        }
        #endregion



    }
}
