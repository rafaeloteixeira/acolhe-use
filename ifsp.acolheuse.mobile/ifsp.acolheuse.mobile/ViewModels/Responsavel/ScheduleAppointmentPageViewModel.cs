using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using ifsp.acolheuse.mobile.Core.Settings;
using ifsp.acolheuse.mobile.ExtensionsMethods;
using ifsp.acolheuse.mobile.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.ViewModels
{
    public class ScheduleAppointmentPageViewModel : ViewModelBase
    {
        #region properties
        private ScheduleAppointmentCollection meetings;
        private bool isRemovable;

        public ScheduleAppointmentCollection Meetings
        {
            get { return meetings; }
            set { meetings = value; RaisePropertyChanged(); }
        }
        public bool IsRemovable
        {
            get { return isRemovable; }
            set { isRemovable = value; RaisePropertyChanged(); }
        }

        public Patient Patient { get; set; }
        public string ResponsibleId { get; set; }
        public string IdAction { get; set; }
        public ObservableCollection<ListEntity> InternCollection { get; set; }
        public int ConsultationType { get; set; }

        #endregion

        private INavigationService navigationService;
        private IPageDialogService dialogService;
        private IInternRepository internRepository;
        private IAppointmentRepository appointmentRepository;
        private IScheduleActionRepository scheduleActionRepository;
        private IResponsibleRepository responsibleRepository;
        public ScheduleAppointmentPageViewModel(INavigationService navigationService, IInternRepository internRepository, IAppointmentRepository appointmentRepository, IScheduleActionRepository scheduleActionRepository, IResponsibleRepository responsibleRepository, IPageDialogService dialogService) :
          base(navigationService)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.internRepository = internRepository;
            this.appointmentRepository = appointmentRepository;
            this.scheduleActionRepository = scheduleActionRepository;
            this.responsibleRepository = responsibleRepository;
            InternCollection = new ObservableCollection<ListEntity>();
        }

        internal async void OpenAppointment(Appointment appointment)
        {
            IEnumerable<Intern> interns = (await internRepository.GetAllAsync()).Where(x => appointment.InternsIdCollection.Contains(x.Id));

            var navParameters = new NavigationParameters();
            navParameters.Add("interns", interns);
            navParameters.Add("patient", appointment.Patient);
            navParameters.Add("schedule", appointment.StartTime);
            navParameters.Add("type_consultation", appointment.ConsultationType);
            await navigationService.NavigateAsync("DetailsschedulePage", navParameters);
        }
        internal async void LoadAppointment(DateTime date)
        {

            Appointment appointment = null;

            ListEntity dadosPatient = new ListEntity
            {
                Id = Patient.Id,
                Name = Patient.NameCompleto
            };

            switch (ConsultationType)
            {
                case Appointment._ORIENTACAO:
                    appointment = new Appointment(ConsultationType, "", ResponsibleId, dadosPatient, IdAction, 0, false, false, date, new ObservableCollection<string>(InternCollection.Select(item => item.Id).ToList()));
                    break;
                case Appointment._GRUPO:
                    appointment = new Appointment(ConsultationType, "", ResponsibleId, dadosPatient, IdAction, 0, false, true, date, new ObservableCollection<string>(InternCollection.Select(item => item.Id).ToList()));
                    break;
                case Appointment._INDIVIDUAL:
                    appointment = new Appointment(ConsultationType, "", ResponsibleId, dadosPatient, IdAction, 0, false, true, date, new ObservableCollection<string>(InternCollection.Select(item => item.Id).ToList()));
                    break;
            }

            var appointmentMarcado = (await appointmentRepository.GetAppointmentByEventIdActionIdAsync(appointment.EventId, IdAction, appointment.Patient.Id));

            if (appointmentMarcado == null)
            {
                IEnumerable<ScheduleAction> schedulesIndisponiveis = await scheduleActionRepository.GetAppointmentsByIdActionAsync(IdAction);

                if (schedulesIndisponiveis.FirstOrDefault(x => x.StartTime.DayOfWeek == date.DayOfWeek && x.StartTime.Hour == date.Hour) == null)
                {
                    var action = await dialogService.DisplayActionSheetAsync("Deseja agendar o atendimento?", "Cancelar", "Agendar");

                    switch (action)
                    {
                        case "Cancelar":
                            return;
                        case "Agendar":
                            Create(appointment);
                            break;
                    }
                }
            }
            else
            {
                appointment = appointmentMarcado;
                var action = await dialogService.DisplayActionSheetAsync("Selecione a operação desejada:", "Cancelar", null, "Ver detalhes", "Desmarcar");

                switch (action)
                {
                    case "Cancelar":
                        return;
                    case "Ver detalhes":
                        OpenAppointment(appointment);
                        break;
                    case "Desmarcar":
                        Delete(appointment);
                        break;
                }

            }
        }

        private async void Create(Appointment appointment)
        {
            if (appointment != null)
            {
                await appointmentRepository.AddAsync(appointment);
                BuscarTodos();
            }
        }
        private async void Delete(Appointment appointment)
        {
            bool excluir = true;
            if (appointment.ConsultationType == Appointment._GRUPO)
                excluir = await MessageService.Instance.ShowAsyncYesNo("Esta ação excluirá os horários de grupo para todas as semanas. Continuar?");

            if (excluir)
            {
                await appointmentRepository.RemoveAsync(appointment.Id);
                BuscarTodos();
            }
        }
        public async void BuscarTodos()
        {
            Meetings = new ScheduleAppointmentCollection();
            try
            {
                IsBusy = true;

                IEnumerable<Appointment> appointments = await appointmentRepository.GetAllByResponsibleId(ResponsibleId);
                IEnumerable<ScheduleAction> schedulesDisponiveis = await scheduleActionRepository.GetAppointmentsByIdActionAsync(IdAction);

                GetAppointments(appointments.Where(x => x.ConsultationType != Appointment._GRUPO));

                IEnumerable<Appointment> groupAppointments = appointments
                    .Where(x => x.ConsultationType == Appointment._GRUPO)
                    .GroupBy(x => x.StartTime)
                    .Select(g => new Appointment()
                    {
                        Canceled = g.FirstOrDefault().Canceled,
                        Confirmed = g.FirstOrDefault().Confirmed,
                        StartTime = g.FirstOrDefault().StartTime,
                        EndTime = g.FirstOrDefault().EndTime,
                        Id = g.FirstOrDefault().Id,
                        EventName = g.FirstOrDefault().EventName,
                        IdResponsible = g.FirstOrDefault().IdResponsible,
                        Patient = g.FirstOrDefault().Patient,
                        IdAction = g.FirstOrDefault().IdAction,
                        Capacity = g.FirstOrDefault().Capacity,
                        AllDay = g.FirstOrDefault().AllDay,
                        Repeat = g.FirstOrDefault().Repeat,
                        ConsultationType = g.FirstOrDefault().ConsultationType,
                        InternsIdCollection = g.FirstOrDefault().InternsIdCollection,
                    });

                GetAppointments(groupAppointments);
                GetFreeSchedules(schedulesDisponiveis);

                IsBusy = false;
            }
            catch (Exception)
            {
                IsBusy = false;
                throw;
            }
        }

        private void GetAppointments(IEnumerable<Appointment> appointments)
        {
            for (int i = 0; i < appointments.Count(); i++)
            {
                ScheduleAppointment appointment = new ScheduleAppointment();
                appointment.Subject = appointments.ElementAt(i).DescricaoConsultation;
                appointment.Color = appointments.ElementAt(i).Cor;
                appointment.StartTime = appointments.ElementAt(i).StartTime;
                appointment.EndTime = appointments.ElementAt(i).EndTime;


                if (appointments.ElementAt(i).Repeat)
                {
                    RecurrenceProperties recurrenceProperties = new RecurrenceProperties();
                    recurrenceProperties.RecurrenceType = RecurrenceType.Weekly;
                    recurrenceProperties.RecurrenceRange = RecurrenceRange.Count;
                    recurrenceProperties.RecurrenceCount = 10;
                    recurrenceProperties.StartDate = appointment.StartTime;

                    switch (appointment.StartTime.DayOfWeek)
                    {
                        case DayOfWeek.Monday:
                            recurrenceProperties.WeekDays = WeekDays.Monday;
                            break;
                        case DayOfWeek.Tuesday:
                            recurrenceProperties.WeekDays = WeekDays.Tuesday;
                            break;
                        case DayOfWeek.Wednesday:
                            recurrenceProperties.WeekDays = WeekDays.Wednesday;
                            break;
                        case DayOfWeek.Thursday:
                            recurrenceProperties.WeekDays = WeekDays.Thursday;
                            break;
                        case DayOfWeek.Friday:
                            recurrenceProperties.WeekDays = WeekDays.Friday;
                            break;
                    }

                    appointment.RecurrenceRule = Xamarin.Forms.DependencyService.Get<IRecurrenceBuilder>().RRuleGenerator(recurrenceProperties, appointment.StartTime, appointment.EndTime);
                }

                Meetings.Add(appointment);
            }
        }
        private void GetFreeSchedules(IEnumerable<ScheduleAction> schedulesDisponiveis)
        {
            RecurrenceProperties recurrenceProperties = new RecurrenceProperties();
            recurrenceProperties.RecurrenceType = RecurrenceType.Weekly;
            recurrenceProperties.RecurrenceRange = RecurrenceRange.EndDate;
            recurrenceProperties.EndDate = DateTime.Now.AddYears(1);
            recurrenceProperties.StartDate = DateTime.Now;

            for (int i = 0; i < schedulesDisponiveis.Count(); i++)
            {
                ScheduleAppointment appointment = new ScheduleAppointment();
                appointment.Subject = "";
                appointment.Color = schedulesDisponiveis.ElementAt(i).Cor;

                DateTime startWeek = DateTime.Now.StartOfWeek(schedulesDisponiveis.ElementAt(i).StartTime.DayOfWeek);
                DateTime endWeek = DateTime.Now.StartOfWeek(schedulesDisponiveis.ElementAt(i).EndTime.DayOfWeek);

                appointment.StartTime = new DateTime(
                    startWeek.Year,
                    startWeek.Month,
                    startWeek.Day,
                    schedulesDisponiveis.ElementAt(i).StartTime.Hour, schedulesDisponiveis.ElementAt(i).StartTime.Minute, 0);

                appointment.EndTime = new DateTime(
                    endWeek.Year,
                    endWeek.Month,
                    endWeek.Day,
                    schedulesDisponiveis.ElementAt(i).EndTime.Hour, schedulesDisponiveis.ElementAt(i).EndTime.Minute, 0);


                switch (schedulesDisponiveis.ElementAt(i).StartTime.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        recurrenceProperties.WeekDays = WeekDays.Monday;
                        break;
                    case DayOfWeek.Tuesday:
                        recurrenceProperties.WeekDays = WeekDays.Tuesday;
                        break;
                    case DayOfWeek.Wednesday:
                        recurrenceProperties.WeekDays = WeekDays.Wednesday;
                        break;
                    case DayOfWeek.Thursday:
                        recurrenceProperties.WeekDays = WeekDays.Thursday;
                        break;
                    case DayOfWeek.Friday:
                        recurrenceProperties.WeekDays = WeekDays.Friday;
                        break;
                }

                //if (Meetings.FirstOrDefault(x => x.StartTime == appointment.StartTime) != null)
                //    continue;

                appointment.RecurrenceRule = Xamarin.Forms.DependencyService.Get<IRecurrenceBuilder>().RRuleGenerator(recurrenceProperties, appointment.StartTime, appointment.EndTime);
                Meetings.Add(appointment);
            }
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            ResponsibleId = Settings.UserId;

            if (parameters["patient"] != null)
            {
                Patient = parameters["patient"] as Patient;
            }
            if (parameters["id_action"] != null)
            {
                IdAction = parameters["id_action"].ToString();
            }
            if (parameters["interns"] != null)
            {
                InternCollection = parameters["interns"] as ObservableCollection<ListEntity>;
            }
            if (parameters["type_consultation"] != null)
            {
                ConsultationType = int.Parse(parameters["type_consultation"].ToString());
            }

            BuscarTodos();
        }
    }
}
