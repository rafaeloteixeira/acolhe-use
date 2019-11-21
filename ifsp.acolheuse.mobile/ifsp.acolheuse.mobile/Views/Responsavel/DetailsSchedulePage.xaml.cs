using ifsp.acolheuse.mobile.ViewModels;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views
{
    public partial class DetailsSchedulePage : ContentPage
    {
        private readonly DetailsSchedulePageViewModel _viewModel;
        public DetailsSchedulePage()
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