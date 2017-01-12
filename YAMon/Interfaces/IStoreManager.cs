using System.Threading.Tasks;

namespace YAMon.Interfaces
{
    public interface IStoreManager
    {
        bool IsInitialized { get; }
        Task InitializeAsync();
        IDeviceStore DeviceStore { get; }
        IMonthUsageStore MonthUsageStore { get; }
        IHourUsageStore HourUsageStore { get; }
        Task<bool> SyncAllAsync(bool syncUserSpecific = true);

        bool UseAuth { get; }
    }
}
