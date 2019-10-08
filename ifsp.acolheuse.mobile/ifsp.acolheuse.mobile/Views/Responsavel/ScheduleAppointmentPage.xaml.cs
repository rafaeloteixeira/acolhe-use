using ifsp.acolheuse.mobile.ViewModels;
using Syncfusion.SfSchedule.XForms;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views
{
    public partial class ScheduleAppointmentPage : ContentPage
    {

        private readonly ScheduleAppointmentPageViewModel _viewModel;

        public ScheduleAppointmentPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as ScheduleAppointmentPageViewModel;
        }



        private void ContentPage_Appearing(object sender, EventArgs e)
        {

        }

        private void Schedule_CellTapped(object sender, CellTappedEventArgs e)
        {
            DateTime schedulemento = e.Datetime;
            if (schedulemento != null)
            {
                _viewModel.LoadAppointment(schedulemento);
            }
        }
    }
}