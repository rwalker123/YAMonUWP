using YAMon.Interfaces;
using YAMon.Services.Standard;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(StoreManager))]
namespace YAMon.Services.Standard
{
    public class StoreManager : IStoreManager
    {

        public bool UseAuth => true;

		/// <summary>
		/// Syncs all tables.
		/// </summary>
		/// <returns>The all async.</returns>
		/// <param name="syncUserSpecific">If set to <c>true</c> sync user specific.</param>
		public Task<bool> SyncAllAsync(bool syncUserSpecific = true)
		{
            return Task.FromResult(true);
		}

       
        public bool IsInitialized { get; private set; }

#region IStoreManager implementation

        object locker = new object();
        public Task InitializeAsync()
        {
            return Task.FromResult(true);
        }

        IDeviceStore deviceStore;
        public IDeviceStore DeviceStore => deviceStore ?? (deviceStore = DependencyService.Get<IDeviceStore>());

        IHourUsageStore hourUsageStore;
        public IHourUsageStore HourUsageStore => hourUsageStore ?? (hourUsageStore = DependencyService.Get<IHourUsageStore>());

        IMonthUsageStore monthUsageStore;
        public IMonthUsageStore MonthUsageStore => monthUsageStore ?? (monthUsageStore = DependencyService.Get<IMonthUsageStore>());
#endregion


    }
}
