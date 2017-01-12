using Newtonsoft.Json;
using System.Collections.Generic;

namespace YAMon.Model
{
    public class MonthUsage : BaseDataObject
    {
        public MonthUsage() : base()
        {
            DailyUsage = new List<DayUsage>();
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

        private int m_reboots;
        [JsonProperty("reboots")]
        public int Reboots
        {
            get { return m_reboots; }
            set { SetProperty(ref m_reboots, value); }
        }

        public List<DayUsage> DailyUsage { get; set; }

    }
}
