using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using ifsp.acolheuse.mobile.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ifsp.acolheuse.mobile.ViewModels.Administrador
{
    public class ScheduleActionPageViewModel : ViewModelBase
    {
        #region properties
        private ObservableCollection<ScheduleAppointment> schedules;
        private ActionModel action;
        private Responsible responsible;
        private DateTime date;
        private string dia;

        public ObservableCollection<ScheduleAppointment> Schedules
        {
            get { return schedules; }
            set { schedules = value; RaisePropertyChanged(); }
        }
        public ActionModel Action
        {
            get { return action; }
            set { action = value; RaisePropertyChanged(); }
        }
        public Responsible Responsible
        {
            get { return responsible; }
            set { responsible = value; RaisePropertyChanged(); }
        }
        public DateTime Date
        {
            get { return date; }
            set { date = value; RaisePropertyChanged(); }
        }

        #endregion

        private INavigationService navigationService;
        private IScheduleActionRepository scheduleRepository;
        private IActionRepository actionRepository;

        public ScheduleActionPageViewModel(INavigationService navigationService, IScheduleActionRepository scheduleRepository, IActionRepository actionRepository) :
            base(navigationService)
        {
            this.navigationService = navigationService;
            this.scheduleRepository = scheduleRepository;
            this.actionRepository = actionRepository;

            Schedules = new ObservableCollection<ScheduleAppointment>();
            Title = "My View A";
        }
        /// <summary>
        /// Creates meetings and stores in a collection.  
        /// </summary>
        public async void CreateAppointments(DateTime hora)
        {
            ScheduleAction schedule = new ScheduleAction(Action.Id, hora);

            var scheduleAppointment = await scheduleRepository.GetAppointmentByIdActionEventIdAsync(Action.Id, schedule.EventId);

            if (scheduleAppointment == null)
            {
                //if (await MessageService.Instance.ShowAsyncYesNo("Deseja excluir este horário da " + dia + "?"))
                //{
                    if (schedule != null)
                    {
                        await scheduleRepository.AddAppointmentByIdActionAsync(Action.Id, schedule);
                        BuscarSchedulesAsync();
                    }
                //}
            }
            else
            {
                //if (await MessageService.Instance.ShowAsyncYesNo("Deseja adicionar este horário da " + dia + "?"))
                //{
                    await scheduleRepository.DeleteAppointmentByIdActionEventIdAsync(Action.Id, schedule.EventId);
                    BuscarSchedulesAsync();
                //}
            }
        }

        public async void BuscarSchedulesAsync()
        {
            try
            {
                IsBusy = true;

                Schedules = new ObservableCollection<ScheduleAppointment>();
                Action = await actionRepository.GetAsync(Action.Id);

                var ex = await scheduleRepository.GetAppointmentsByIdActionAsync(Action.Id);

                for (int i = 0; i < ex.Count(); i++)
                {
                    ScheduleAppointment appointment = new ScheduleAppointment();
                    appointment.Subject = "Fechado";
                    appointment.Color = ex.ElementAt(i).Cor;
                    appointment.StartTime = ex.ElementAt(i).StartTime;
                    appointment.EndTime = ex.ElementAt(i).EndTime;
                    Schedules.Add(appointment);
                }

                IsBusy = false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters["action"] != null)
            {
                Action = parameters["action"] as ActionModel;
            }
            if (parameters["responsible"] != null)
            {
                Responsible = parameters["responsible"] as Responsible;
            }
            if (parameters["dia"] != null)
            {
                dia = parameters["dia"].ToString();
            }

            switch (dia)
            {
                case "Segunda-feira":
                    Date = new DateTime(2001, 01, 01, 08, 0, 0);
                    break;
                case "Terça-feira":
                    Date = new DateTime(2001, 01, 02, 08, 0, 0);
                    break;
                case "Quarta-feira":
                    Date = new DateTime(2001, 01, 03, 08, 0, 0);
                    break;
                case "Quinta-feira":
                    Date = new DateTime(2001, 01, 04, 08, 0, 0);
                    break;
                case "Sexta-feira":
                    Date = new DateTime(2001, 01, 05, 08, 0, 0);
                    break;
            }

            BuscarSchedulesAsync();
        }
    }
}
