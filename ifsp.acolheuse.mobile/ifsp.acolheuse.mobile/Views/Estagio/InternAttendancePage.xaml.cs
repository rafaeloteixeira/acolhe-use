using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.ViewModels;
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
            _viewModel.BuscarAppointmentsAsync();
        }
        private void ListTTasks_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (e.ItemData != null)
            {
                var appointment = (Appointment)e.ItemData;
                popupLayout.Show();
            }
        }
    }
}
