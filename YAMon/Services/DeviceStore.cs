using System.Collections.Generic;
using System.Threading.Tasks;
using YAMon.Interfaces;
using YAMon.Model;
using System.Linq;
using YAMon.Services.Standard;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(DeviceStore))]
namespace YAMon.Services.Standard
{
    public class DeviceStore : BaseStore<Device>, IDeviceStore
    {
        public override string Identifier => "Device";
        public override async Task<IEnumerable<Device>> GetItemsAsync(bool forceRefresh = false)
        {
            if (!forceRefresh && table.Any())
                return await base.GetItemsAsync(forceRefresh);

            table.Clear();

            var usersUri = new Uri(String.Format("http://{0}/user/data3/users.js", serverIp));

            // Always catch network exceptions for async methods
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var serializer = new JsonSerializer();

                    var users = await httpClient.GetStringAsync(usersUri);
                    using (StringReader sr = new StringReader(users))
                    {
                        string newLine;
                        while ((newLine = sr.ReadLine()) != null)
                        {
                            if (newLine.StartsWith("ud_a("))
                            {
                                var txtReader = new JsonTextReader(new StringReader(newLine.Substring(5, newLine.Length - 6)));
                                var obj = serializer.Deserialize<Device>(txtReader);
                                table.Add(obj);
                            }
                        }
                    }
                }
            }
            catch
            {
                // Details in ex.Message and ex.HResult.       
            }

            return await Task.FromResult(table);
        }
    }
}