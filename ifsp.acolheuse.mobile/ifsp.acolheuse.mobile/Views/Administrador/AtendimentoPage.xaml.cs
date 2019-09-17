using ifsp.acolheuse.mobile.ViewModels;
using Syncfusion.SfSchedule.XForms;
using System;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Administrador
{
    public partial class AtendimentoPage : ContentPage
    {
        private readonly AtendimentoPageViewModel _viewModel;
        public AtendimentoPage()
        {
            InitializeComponent();
            BindingContext = _viewModel;
        }

        private void Schedule_CellTapped(object sender, CellTappedEventArgs e)
        {
            DateTime horario = e.Datetime;
            if (horario != null)
            {
                _viewModel.CreateAppointments(horario);
            }

        }
    }
}
