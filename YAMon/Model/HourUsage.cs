using Newtonsoft.Json;

namespace YAMon.Model
{
    public class HourUsage : BaseDataObject
    {
        public HourUsage() : base()
        {
        }

        private string m_hour;
        [JsonProperty("hour")]
        public string Hour
        {
            get { return m_hour; }
            set { SetProperty(ref m_hour, value); }
        }

        private long m_down;
        [JsonProperty("down")]
        public long Down
        {
            get { return m_down; }
            set { SetProperty(ref m_down, value); }
        }

        private long m_up;
        [JsonProperty("up")]
        public long Up
        {
            get { return m_up; }
            set { SetProperty(ref m_up, value); }
        }

        private string m_mac;
        [JsonProperty("mac")]
        public string MacAddress
        {
            get { return m_mac; }
            set { SetProperty(ref m_mac, value); }
        }
    }
}
