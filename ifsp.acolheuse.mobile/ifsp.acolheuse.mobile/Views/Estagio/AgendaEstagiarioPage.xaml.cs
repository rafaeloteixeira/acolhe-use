using ifsp.acolheuse.mobile.ViewModels;
using Syncfusion.SfSchedule.XForms;
using System;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Estagio
{
    public partial class AgendaEstagiarioPage : ContentPage
    {
        private readonly AgendaEstagiarioPageViewModel _viewModel;

        public AgendaEstagiarioPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as AgendaEstagiarioPageViewModel;
        }

        private void Schedule_CellTapped(object sender, CellTappedEventArgs e)
        {
            DateTime agendamento = e.Datetime;
            if (agendamento != null)
            {
                _viewModel.VisualizeAppointments();
            }
        }
    }
}
