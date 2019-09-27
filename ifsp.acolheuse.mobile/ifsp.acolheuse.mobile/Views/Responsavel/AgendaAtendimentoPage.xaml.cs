using ifsp.acolheuse.mobile.ViewModels.Responsavel;
using Syncfusion.SfSchedule.XForms;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Responsavel
{
    public partial class AgendaAtendimentoPage : ContentPage
    {

        private readonly AgendaAtendimentoPageViewModel _viewModel;

        public AgendaAtendimentoPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as AgendaAtendimentoPageViewModel;
        }



        private void ContentPage_Appearing(object sender, EventArgs e)
        {

        }

        private void Schedule_CellTapped(object sender, CellTappedEventArgs e)
        {
            DateTime agendamento = e.Datetime;
            if (agendamento != null)
            {
                _viewModel.LoadAppointment(agendamento);
            }
        }
    }
}