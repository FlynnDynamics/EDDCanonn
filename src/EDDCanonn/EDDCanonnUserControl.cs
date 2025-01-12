using System;
using static EDDDLLInterfaces.EDDDLLIF;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using QuickJSON;
using System.Threading.Tasks;
using System.Linq;

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
                // Fetch the whitelist from a remote source
                dataHandler.FetchCanonnAsync(CanonnHelper.WhitelistUrl,
                jsonResponse =>
                {
                    try
                    {
                        JArray whitelistItems = jsonResponse.JSONParse().Array();

                        if (whitelistItems == null)
                            return;

                        AddToWhitelist(whitelistItems);
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

        private void AddToWhitelist(JArray whitelistItems)
        {
            // Build the in-memory whitelist
            for (int i = 0; i < whitelistItems.Count; i++)
            {
                JObject itemObject = whitelistItems[i].Object();
                string definitionRaw = itemObject["definition"].Str();
                if (string.IsNullOrEmpty(definitionRaw))
                    continue;

                JObject definitionObject = definitionRaw.JSONParse().Object();

                // Default key to identify the type
                string typeKey = "event";
                string typeValue = definitionObject[typeKey].Str();

                if (typeValue == "")
                    typeValue = "undefined";

                WhitelistEvent existingEvent = null;
                for (int e = 0; e < _globalWhitelist.Events.Count; e++)
                {
                    if (_globalWhitelist.Events[e].Type.Equals(typeValue, StringComparison.InvariantCultureIgnoreCase))
                    {
                        existingEvent = _globalWhitelist.Events[e];
                        break;
                    }
                }

                if (existingEvent == null)
                {
                    existingEvent = new WhitelistEvent();
                    existingEvent.Type = typeValue;
                    _globalWhitelist.Events.Add(existingEvent);
                }

                Dictionary<string, object> dataBlock = new Dictionary<string, object>();
                List<string> keys = new List<string>(definitionObject.PropertyNames());

                // Collect every property except the type key
                for (int kk = 0; kk < keys.Count; kk++)
                {
                    string key = keys[kk];
                    if (key.Equals(typeKey, StringComparison.InvariantCultureIgnoreCase))
                        continue;

                    object val = definitionObject[key].Value;
                    dataBlock[key] = val;
                }

                if (dataBlock.Count > 0)
                    existingEvent.DataBlocks.Add(dataBlock);
            }
        }

        private WhitelistData _globalWhitelist = new WhitelistData();

        public class WhitelistData
        {
            // Holds all events of various types
            public List<WhitelistEvent> Events { get; set; }

            public WhitelistData()
            {
                Events = new List<WhitelistEvent>();
            }
        }

        public class WhitelistEvent
        {
            public string Type { get; set; }
            public List<Dictionary<string, object>> DataBlocks { get; set; }

            public WhitelistEvent()
            {
                DataBlocks = new List<Dictionary<string, object>>();
            }
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
            dataHandler.Closing();
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
