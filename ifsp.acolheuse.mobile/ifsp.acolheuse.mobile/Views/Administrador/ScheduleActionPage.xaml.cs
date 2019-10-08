using ifsp.acolheuse.mobile.ViewModels;
using Syncfusion.SfSchedule.XForms;
using System;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Administrador
{
    public partial class ScheduleActionPage : ContentPage
    {
  
        private readonly ViewModels.Administrador.ScheduleActionPageViewModel _viewModel;

        public ScheduleActionPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as ViewModels.Administrador.ScheduleActionPageViewModel;
        }

        private void Schedule_CellTapped(object sender, CellTappedEventArgs e)
        {
            DateTime schedule = e.Datetime;
            if (schedule != null)
            {
                _viewModel.CreateAppointments(schedule);
            }

        }
    }
}
