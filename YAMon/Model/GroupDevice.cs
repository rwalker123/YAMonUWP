using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using YAMon.Converters;

namespace YAMon.Model
{
    public class GroupDevice : BaseDataObject
    {
        private long cachedTotal = -1;
        private double cachedPercent = -1.0;

        // todo: make 125 an option.
        const long PersonalAllowance = 125 * ByteToStringConverter.ONEGB;
        public GroupDevice()
        {
            Devices = new List<Model.Device>();
        }

        public string Owner
        {
            get
            {
                return Devices.First().Owner;
            }
        }
        public IList<Model.Device> Devices { get; private set; }

        public long TotalUsage
        {
            get
            {
                if (cachedTotal == -1)
                    cachedTotal = Devices.Sum(x => x.TotalUsage);
                return cachedTotal;
            }
        }

        public double TotalPercent
        {
            get
            {
                if (cachedPercent < 0.0)
                {
                    cachedPercent = Devices.Sum(x => x.UsagePercent);
                }

                return cachedPercent;
            }
        }

        public double GroupUsagePercent
        {
            get
            {
                return (double)TotalUsage / (double)PersonalAllowance;
            }
        }

        public long MonthlyForecast
        {
            get
            {
                double dailyAvg = (double)TotalUsage / (double)DateTime.Now.Day;
                return (long)(dailyAvg * DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));

            }
        }
    }
}
