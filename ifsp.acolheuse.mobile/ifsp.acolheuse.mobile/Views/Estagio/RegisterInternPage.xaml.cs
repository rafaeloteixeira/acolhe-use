using ifsp.acolheuse.mobile.ViewModels;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Estagio
{
    public partial class RegisterInternPage : ContentPage
    {
        private readonly ViewModels.Estagio.RegisterInternPageViewModel _viewModel;

        public RegisterInternPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as ViewModels.Estagio.RegisterInternPageViewModel;
        }
      
        protected override void OnAppearing()
        {
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.CheckPassword();
        }
    }
}
