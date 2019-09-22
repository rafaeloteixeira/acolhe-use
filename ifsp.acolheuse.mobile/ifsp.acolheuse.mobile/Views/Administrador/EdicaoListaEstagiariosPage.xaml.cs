using ifsp.acolheuse.mobile.ViewModels;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Administrador
{
    public partial class EdicaoListaEstagiariosPage : ContentPage
    {
        private readonly ViewModels.Administrador.EdicaoListaEstagiariosPageViewModel _viewModel;
        public EdicaoListaEstagiariosPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as ViewModels.Administrador.EdicaoListaEstagiariosPageViewModel;
        }
        protected override void OnAppearing()
        {
        }
    }
}
