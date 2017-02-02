using YAMon.Helpers;
using YAMon.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace YAMon.ViewModel
{
    public class DeviceDetailViewModel : ViewModelBase
    {
        public Action<Model.Device> OnFinished { get; set; }
        public Model.Device Item { get; set; }
        public IReadOnlyList<Page> NavStack { get; }

        public DeviceDetailViewModel(INavigation nav, Model.Device item = null)
        {
            NavStack = nav.NavigationStack;
            Title = BuildBreadCrumb(nav, item.Name);
            Item = item;
            SaveCommand = new Command(async () => await ExecuteSaveCommand());
        }

        public Command SaveCommand { get; }

        async Task ExecuteSaveCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            //var newItem = new MyItem
            //{
            //    DeviceName = Item.DeviceName,
            //    Group = Item.Group,
            //    Quantity = Quantity
            //};

            try
            {
                if (!Settings.IsLoggedIn)
                {
                    if (!await LoginViewModel.TryLoginAsync(StoreManager))
                        return;
                }

                //await StoreManager.MyItemStore.InsertAsync(newItem);
                //MyItemsViewModel.IsDirty = true;

                IsBusy = false;
                OnFinished?.Invoke(Item);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
