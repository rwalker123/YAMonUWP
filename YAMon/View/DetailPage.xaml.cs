using YAMon.Model;
using YAMon.ViewModel;

using Xamarin.Forms;

namespace YAMon.View
{
    public partial class DetailPage : ContentPage
    {
        DeviceDetailViewModel viewModel;
        public DetailPage(DeviceDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;

            this.viewModel.OnFinished += OnFinished;
        }

        async void OnFinished(Model.Device item)
        {
            viewModel.OnFinished -= OnFinished;
            await Navigation.PopAsync();
        }
    }
}
