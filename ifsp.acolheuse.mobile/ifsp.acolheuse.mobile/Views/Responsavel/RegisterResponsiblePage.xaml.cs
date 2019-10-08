using ifsp.acolheuse.mobile.ViewModels;
using ifsp.acolheuse.mobile.Core.Domain;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views
{
    public partial class RegisterResponsiblePage : ContentPage
    {
        private readonly RegisterResponsiblePageViewModel _viewModel;
        public RegisterResponsiblePage()
        {
            InitializeComponent();
            _viewModel = BindingContext as RegisterResponsiblePageViewModel;
        }

        protected override void OnAppearing()
        {
        }

        private void LvIntern_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                var intern = (Intern)e.Item;
                _viewModel.ItemTapped(intern);
            }
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.CheckPass();
        }
    }
}
