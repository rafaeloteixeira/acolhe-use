using ifsp.acolheuse.mobile.ViewModels.Responsavel;
using Syncfusion.SfSchedule.XForms;
using System;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Responsavel
{
    public partial class AgendaServidorPage : ContentPage
    {
        private readonly AgendaServidorPageViewModel _viewModel;

        public AgendaServidorPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as AgendaServidorPageViewModel;
        }

        private void Schedule_CellTapped(object sender, CellTappedEventArgs e)
        {
            ScheduleAppointment agendamento = e.Appointment as ScheduleAppointment;
            if (agendamento != null)
            {
                _viewModel.VisualizeAppointments(agendamento.Notes);
            }
        }
    }
}