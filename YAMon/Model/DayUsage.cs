using Newtonsoft.Json;

namespace YAMon.Model
{
    public class DayUsage : BaseDataObject
    {
        public DayUsage() : base()
        {
        }

        private string m_day;
        [JsonProperty("day")]
        public string Day
        {
            get { return m_day; }
            set { SetProperty(ref m_day, value); }
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
