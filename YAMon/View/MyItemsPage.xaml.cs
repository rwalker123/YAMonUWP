using YAMon.Model;
using YAMon.ViewModel;

using Xamarin.Forms;

namespace YAMon.View
{
    public partial class MyItemsPage : ContentPage
    {
        DeviceDetailViewModel viewModel;
        public MyItemsPage()
        {
            InitializeComponent();
            //BindingContext = viewModel = new DeviceDetailViewModel();

            //viewModel.OnNavigateToDetails = async (detailsViewModel) =>
            //{
            //    await Navigation.PushAsync(new DeviceDetailDetailPage(detailsViewModel));
            //};

        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Model.Device;
            if (item == null)
                return;

            //viewModel.EditCommand.Execute(item.Id);

            // Manually deselect item
            ListViewItems.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //if (viewModel.Items.Count == 0 || DeviceDetailViewModel.IsDirty)
            //    viewModel.LoadItemsCommand.Execute(null);
        }
    }
}
