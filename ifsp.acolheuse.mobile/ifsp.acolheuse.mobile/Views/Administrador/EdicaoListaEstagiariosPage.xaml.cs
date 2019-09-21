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
            _viewModel = BindingContext as EdicaoListaEstagiariosPageViewModel;
        }
        protected override void OnAppearing()
        {
        }
    }
}
