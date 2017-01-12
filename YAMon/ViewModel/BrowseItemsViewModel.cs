using YAMon.Helpers;
using YAMon.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using YAMon.Converters;

namespace YAMon.ViewModel
{
    public class BrowseItemsViewModel : ViewModelBase
    {
        const long MaxUsage = ByteToStringConverter.ONETB;
        long m_totalUsage = 0;
        double m_totalUsagePercent = 0.0;

        public ObservableRangeCollection<GroupDevice> Items { get;}
        public Action<GroupDetailViewModel> OnNavigateToDetails { get; set; }
        public BrowseItemsViewModel()
        {
            Title = "Groups and Devices";
            Items = new ObservableRangeCollection<GroupDevice>();
            LoadItemsCommand = new Command(ExecuteLoadItemsCommand);
            GoToDetailsCommand = new Command<string>(ExecuteGoToDetailsCommand);
        }

        public Command LoadItemsCommand { get;}

        async void ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                var devices = StoreManager.DeviceStore.GetItemsAsync(true);
                var monthlyData = StoreManager.MonthUsageStore.GetItemsAsync(true);
                var hourlyData = StoreManager.HourUsageStore.GetItemsAsync(true);
                await Task.WhenAll(new Task[] { devices, monthlyData, hourlyData });

                long totalUsage = 0;
                var usageByDevice = new Dictionary<string, Model.Device>();
                foreach (var d in devices.Result)
                {
                    usageByDevice[d.MacAddress] = d;
                }

                foreach(var x in monthlyData.Result)
                {
                    foreach(var y in x.DailyUsage)
                    {
                        var usage = y.Up + y.Down;
                        usageByDevice[y.MacAddress].TotalUsage += usage;
                        totalUsage += usage;
                    }
                }

                foreach(var x in hourlyData.Result)
                {
                    var usage = x.Up + x.Down;
                    usageByDevice[x.MacAddress].TotalUsage += usage;
                    totalUsage += usage;
                }

                var groupDevice = new Dictionary<string, GroupDevice>();
                foreach (var d in devices.Result)
                {
                    d.UsagePercent = (double)d.TotalUsage/totalUsage;
                    if (!groupDevice.ContainsKey(d.Owner))
                    {
                        groupDevice[d.Owner] = new GroupDevice();
                    }

                    groupDevice[d.Owner].Devices.Add(d);
                }

                Items.ReplaceRange(groupDevice.Values.OrderByDescending(x => x.TotalUsage).Select(x => x).ToList());
                TotalUsage = totalUsage;
                TotalUsagePercent = (double)TotalUsage / (double)MaxUsage;
            }
            catch (Exception ex)
            {
                //Handle exception here
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load items.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public long TotalUsage
        {
            get { return m_totalUsage; }
            set { SetProperty(ref m_totalUsage, value); }
        }

        public double TotalUsagePercent
        {
            get { return m_totalUsagePercent; }
            set { SetProperty(ref m_totalUsagePercent, value); }
        }

        public Command<string> GoToDetailsCommand { get; }
        GroupDetailViewModel detailsViewModel;
        void ExecuteGoToDetailsCommand(string id)
        {
            if (IsBusy)
                return;

            var selectedItem = Items.FirstOrDefault(i => i.Id == id);

            detailsViewModel = new GroupDetailViewModel(selectedItem);
            detailsViewModel.OnFinished += OnFinished;

            OnNavigateToDetails(detailsViewModel);
        }

        void OnFinished(GroupDevice item)
        {
            detailsViewModel.OnFinished -= OnFinished;
        }

    }
}
