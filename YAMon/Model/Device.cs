using Newtonsoft.Json;

namespace YAMon.Model
{
    public class Device : BaseDataObject
    {
        public Device() : base()
        {
        }

        private string m_mac;
        [JsonProperty("mac")]
        public string MacAddress
        {
            get { return m_mac; }
            set { SetProperty(ref m_mac, value); }
        }

        private string m_ip;
        [JsonProperty("ip")]
        public string IpAddress
        {
            get { return m_ip; }
            set { SetProperty(ref m_ip, value); }
        }

        private string m_owner;
        [JsonProperty("owner")]
        public string Owner
        {
            get { return m_owner; }
            set { SetProperty(ref m_owner, value); }
        }

        private string m_name;
        [JsonProperty("name")]
        public string Name
        {
            get { return m_name; }
            set { SetProperty(ref m_name, value); }
        }

        private string m_color;
        [JsonProperty("colour")]
        public string Color
        {
            get { return m_color; }
            set { SetProperty(ref m_color, value); }
        }

        private string m_added;
        [JsonProperty("added")]
        public string Added
        {
            get { return m_added; }
            set { SetProperty(ref m_added, value); }
        }

        private string m_updated;
        [JsonProperty("updated")]
        public string Updated
        {
            get { return m_updated; }
            set { SetProperty(ref m_updated, value); }
        }

        private string m_lastSeen;
        [JsonProperty("last-seen")]
        public string LastSeen
        {
            get { return m_lastSeen; }
            set { SetProperty(ref m_lastSeen, value); }
        }

        public long TotalUsage
        { get; set; }

        public double UsagePercent
        { get; set; }
    }
}
