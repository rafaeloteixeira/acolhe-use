using ifsp.acolheuse.mobile.ViewModels;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views
{
    public partial class DetailsschedulePage : ContentPage
    {
        private readonly DetailsschedulePageViewModel _viewModel;
        public DetailsschedulePage()
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