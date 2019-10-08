using ifsp.acolheuse.mobile.ViewModels;
using Syncfusion.SfSchedule.XForms;
using System;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views
{
    public partial class ScheduleResponsiblePage : ContentPage
    {
        private readonly ScheduleResponsiblePageViewModel _viewModel;

        public ScheduleResponsiblePage()
        {
            InitializeComponent();
            _viewModel = BindingContext as ScheduleResponsiblePageViewModel;
        }

        private void Schedule_CellTapped(object sender, CellTappedEventArgs e)
        {
            ScheduleAppointment schedulemento = e.Appointment as ScheduleAppointment;
            if (schedulemento != null)
            {
                _viewModel.VisualizeAppointments(schedulemento.Notes);
            }
        }
    }
}