using ifsp.acolheuse.mobile.ViewModels;
using ifsp.acolheuse.mobile.ViewModels.Acolhimento;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Acolhimento
{
    public partial class InclusaoAcaoPage : ContentPage
    {
        private readonly InclusaoAcaoPageViewModel _viewModel;
        public InclusaoAcaoPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as InclusaoAcaoPageViewModel;
        }

        protected override void OnAppearing()
        {
            _viewModel.BuscarAcoesCollectionAsync();
        }
    }
}
