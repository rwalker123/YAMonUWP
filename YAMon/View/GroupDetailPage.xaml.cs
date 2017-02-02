using YAMon.Model;
using YAMon.ViewModel;

using Xamarin.Forms;

namespace YAMon.View
{
    public partial class GroupDetailPage : ContentPage
    {
        GroupDetailViewModel viewModel;
        public GroupDetailPage(GroupDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;

            this.viewModel.OnFinished += OnFinished;

            viewModel.OnNavigateToDetails = async (detailsViewModel) =>
            {
                await Navigation.PushAsync(new DetailPage(detailsViewModel));
            };

        }
        void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Model.Device;
            if (item == null)
                return;

            viewModel.GoToDetailsCommand.Execute(item.Id);

            // Manually deselect item
            ListViewItems.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        async void OnFinished(Model.Device item)
        {
            viewModel.OnFinished -= OnFinished;
            await Navigation.PopAsync();
        }
    }
}
