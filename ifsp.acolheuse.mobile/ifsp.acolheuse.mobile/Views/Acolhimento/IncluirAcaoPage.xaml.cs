using ifsp.acolheuse.mobile.ViewModels;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Acolhimento
{
    public partial class IncluirAcaoPage : ContentPage
    {
        private readonly IncluirAcaoPageViewModel _viewModel;
        public IncluirAcaoPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as IncluirAcaoPageViewModel;
        }

        protected override void OnAppearing()
        {
            _viewModel.BuscarAcoesCollectionAsync();
        }
    }
}
