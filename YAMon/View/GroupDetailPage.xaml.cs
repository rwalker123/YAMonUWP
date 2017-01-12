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
        }

        async void OnFinished(GroupDevice item)
        {
            viewModel.OnFinished -= OnFinished;
            await Navigation.PopAsync();
        }
    }
}
