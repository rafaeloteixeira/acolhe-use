using ifsp.acolheuse.mobile.ViewModels;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views
{
    public partial class DetailsSchedulementoPage : ContentPage
    {
        private readonly DetailsSchedulementoPageViewModel _viewModel;
        public DetailsSchedulementoPage()
        {
            InitializeComponent();
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