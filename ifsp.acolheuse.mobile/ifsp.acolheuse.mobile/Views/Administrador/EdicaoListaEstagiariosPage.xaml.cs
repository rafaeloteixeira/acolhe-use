using ifsp.acolheuse.mobile.ViewModels;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Administrador
{
    public partial class EdicaoListaEstagiariosPage : ContentPage
    {
        private readonly EdicaoListaEstagiariosPageViewModel _viewModel;
        public EdicaoListaEstagiariosPage()
        {
            InitializeComponent();
            BindingContext = _viewModel;
        }
        protected override void OnAppearing()
        {
        }
    }
}
