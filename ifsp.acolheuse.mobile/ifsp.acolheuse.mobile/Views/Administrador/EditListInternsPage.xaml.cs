using ifsp.acolheuse.mobile.ViewModels;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Administrador
{
    public partial class EditListInternsPage : ContentPage
    {
        private readonly ViewModels.Administrador.EditListInternsPageViewModel _viewModel;
        public EditListInternsPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as ViewModels.Administrador.EditListInternsPageViewModel;
        }
        protected override void OnAppearing()
        {
        }
    }
}
