using YAMon.Model;
using YAMon.ViewModel;

using Xamarin.Forms;

namespace YAMon.View
{
    public partial class BrowseItemsPage : ContentPage
    {
        BrowseItemsViewModel viewModel;
        public BrowseItemsPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new BrowseItemsViewModel(this.Navigation);

            viewModel.OnNavigateToDetails = async (detailsViewModel) =>
            {
                await Navigation.PushAsync(new GroupDetailPage(detailsViewModel));
            };
        }

        void OnItemSelected (object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as GroupDevice;
            if (item == null)
                return;

            viewModel.GoToDetailsCommand.Execute(item.Id);

            // Manually deselect item
            ListViewItems.SelectedItem = null;
        }

        System.Threading.Timer m_refreshTimer;

        protected override void OnAppearing()
        {
            base.OnAppearing();

            m_refreshTimer = new System.Threading.Timer((o) =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() => { viewModel.LoadItemsCommand.Execute(null); });
            }, null, 0, 90000);

        }

        protected override void OnDisappearing()
        {
            m_refreshTimer.Dispose();
            base.OnDisappearing();
        }

    }
}
