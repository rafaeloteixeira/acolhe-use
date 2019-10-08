using ifsp.acolheuse.mobile.ViewModels;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Administrador
{
    public partial class RegisterActionPage : ContentPage
    {
        private readonly ViewModels.Administrador.RegisterActionPageViewModel _viewModel;

        public RegisterActionPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as ViewModels.Administrador.RegisterActionPageViewModel;
        }
        protected override void OnAppearing()
        {
        }
        private void LvInterns_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
        private void LvResponsible_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}
