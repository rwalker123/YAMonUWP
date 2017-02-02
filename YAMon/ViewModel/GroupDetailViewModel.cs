using System;
using System.Linq;
using Xamarin.Forms;
using YAMon.Helpers;
using YAMon.Model;

namespace YAMon.ViewModel
{
    public class GroupDetailViewModel : ViewModelBase
    {
        private INavigation m_navigation;

        public Action<Model.Device> OnFinished { get; set; }
        public ObservableRangeCollection<Model.Device> Items { get; }
        public GroupDevice Item { get; set; }
        public Action<DeviceDetailViewModel> OnNavigateToDetails { get; set; }

        public GroupDetailViewModel(INavigation nav, GroupDevice item = null)
        {
            m_navigation = nav;
            Title = BuildBreadCrumb(nav, item.Owner);
            Item = item;
            Items = new ObservableRangeCollection<Model.Device>(item.Devices.OrderByDescending(x => x.TotalUsage).Select(x => x).ToList());
            GoToDetailsCommand = new Command<string>(ExecuteGoToDetailsCommand);
        }

        public Command<string> GoToDetailsCommand { get; }
        void ExecuteGoToDetailsCommand(string id)
        {
            if (IsBusy)
                return;

            var selectedItem = Items.FirstOrDefault(i => i.Id == id);

            var detailsViewModel = new DeviceDetailViewModel(m_navigation, selectedItem);
            detailsViewModel.OnFinished += OnFinished;

            OnNavigateToDetails(detailsViewModel);
        }
    }
}
