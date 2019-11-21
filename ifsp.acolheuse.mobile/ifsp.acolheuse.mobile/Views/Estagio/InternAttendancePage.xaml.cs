using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.ViewModels;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Estagio
{
    public partial class InternAttendancePage : ContentPage
    {
        private readonly InternAttendancePageViewModel _viewModel;
        public InternAttendancePage()
        {
            InitializeComponent();
            _viewModel = BindingContext as InternAttendancePageViewModel;

        }

        protected override void OnAppearing()
        {
        }
        private void ListTTasks_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {

        }

        private void OnDateSelected(object sender, DateChangedEventArgs e)
        {
            _viewModel.GetAppointmentsAsync();
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            //var sItem = (sender as Switch);
            //var item = (sItem.Parent).BindingContext as Appointment;
            //_viewModel.SaveCheckOutAsync(item.Id, sItem.IsToggled);
        }
    }
}
