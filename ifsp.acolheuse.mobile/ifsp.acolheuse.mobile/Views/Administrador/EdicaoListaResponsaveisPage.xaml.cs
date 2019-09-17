using ifsp.acolheuse.mobile.ViewModels;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Administrador
{
    public partial class EdicaoListaResponsaveisPage : ContentPage
    {
        private readonly EdicaoListaResponsaveisPageViewModel _viewModel;
        public EdicaoListaResponsaveisPage()
        {
            InitializeComponent();
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
      
        }
    }
}
