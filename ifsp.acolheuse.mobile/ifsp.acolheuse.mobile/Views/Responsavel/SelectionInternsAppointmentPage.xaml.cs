using ifsp.acolheuse.mobile.ViewModels;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views
{
    public partial class SelectionInternsAppointmentPage : ContentPage
    {
        private readonly SelectionInternsAppointmentPageViewModel _viewModel;

        public SelectionInternsAppointmentPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as SelectionInternsAppointmentPageViewModel;
        }
   
        protected override void OnAppearing()
        {
        }
    }
}