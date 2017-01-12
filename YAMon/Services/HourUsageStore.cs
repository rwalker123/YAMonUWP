using System.Collections.Generic;
using System.Threading.Tasks;
using YAMon.Interfaces;
using YAMon.Model;
using YAMon.Services.Standard;
using System;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;

[assembly: Xamarin.Forms.Dependency(typeof(HourUsageStore))]
namespace YAMon.Services.Standard
{
    public class HourUsageStore : BaseStore<HourUsage>, IHourUsageStore
    {
        public override string Identifier => "HourUsage";
        public override async Task<IEnumerable<HourUsage>> GetItemsAsync(bool forceRefresh = false)
        {
            if (!forceRefresh && table.Any())
                return await base.GetItemsAsync(forceRefresh);

            table.Clear();

            var monthStr = DateTime.Now.ToString("yyyy-MM-dd");

            var monthlyUri = new Uri(String.Format("http://{0}/user/data3/{1}-hourly_data.js", serverIp, monthStr));

            using (var httpClient = new HttpClient())
            {
                var serializer = new JsonSerializer();

                var result = await httpClient.GetStringAsync(monthlyUri);
                using (StringReader sr = new StringReader(result))
                {
                    string newLine;
                    while ((newLine = sr.ReadLine()) != null)
                    {
                        // cleanup some bad json formatting in data. down/up can have leading 0's and that is
                        // intepreted as octal.
                        newLine = Regex.Replace(newLine, "\"down\":0(?=[^,])", "\"down\":");
                        newLine = Regex.Replace(newLine, "\"up\":0(?=[^,\\}])", "\"up\":");
                        if (newLine.StartsWith("hu("))
                        {
                            var txtReader = new JsonTextReader(new StringReader(newLine.Substring(3, newLine.Length - 4)));
                            var obj = serializer.Deserialize<HourUsage>(txtReader);
                            table.Add(obj);
                        }
                    }
                }
            }

            return await Task.FromResult(table);
        }
    }
}