using ifsp.acolheuse.mobile.ViewModels;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Estagio
{
    public partial class CadastroEstagiarioPage : ContentPage
    {
        private readonly ViewModels.Estagio.CadastroEstagiarioPageViewModel _viewModel;

        public CadastroEstagiarioPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as ViewModels.Estagio.CadastroEstagiarioPageViewModel;
        }
      
        protected override void OnAppearing()
        {
        }
    }
}
