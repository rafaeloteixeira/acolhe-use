using ifsp.acolheuse.mobile.ViewModels;
using Syncfusion.SfSchedule.XForms;
using System;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Administrador
{
    public partial class HorarioAcaoPage : ContentPage
    {
  
        private readonly ViewModels.Administrador.HorarioAcaoPageViewModel _viewModel;

        public HorarioAcaoPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as ViewModels.Administrador.HorarioAcaoPageViewModel;
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
