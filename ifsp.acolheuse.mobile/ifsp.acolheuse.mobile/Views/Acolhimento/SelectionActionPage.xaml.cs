using ifsp.acolheuse.mobile.ViewModels;
using ifsp.acolheuse.mobile.ViewModels.Acolhimento;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Acolhimento
{
    public partial class SelectionActionPage : ContentPage
    {
        private readonly SelectionActionPageViewModel _viewModel;
        public SelectionActionPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as SelectionActionPageViewModel;
        }

        protected override void OnAppearing()
        {
            _viewModel.BuscarActionCollectionAsync();
        }
    }
}
