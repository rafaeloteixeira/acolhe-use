using ifsp.acolheuse.mobile.ViewModels;
using Syncfusion.SfSchedule.XForms;
using System;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Estagio
{
    public partial class ScheduleInternPage : ContentPage
    {
        private readonly ScheduleInternPageViewModel _viewModel;

        public ScheduleInternPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as ScheduleInternPageViewModel;
        }

        private void Schedule_CellTapped(object sender, CellTappedEventArgs e)
        {
            ScheduleAppointment schedule = e.Appointment as ScheduleAppointment;
            if (schedule != null)
            {
                _viewModel.VisualizeAppointments(schedule.Notes);
            }
        }
    }
}
