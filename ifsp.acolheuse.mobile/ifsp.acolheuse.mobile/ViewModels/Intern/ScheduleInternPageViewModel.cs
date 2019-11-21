using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using ifsp.acolheuse.mobile.Core.Settings;
using ifsp.acolheuse.mobile.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.ViewModels
{
    public class ScheduleInternPageViewModel : ViewModelBase
    {
        private ObservableCollection<ScheduleAppointment> meetings;
        private Intern intern;

        public ObservableCollection<ScheduleAppointment> Meetings
        {
            get { return meetings; }
            set { meetings = value; RaisePropertyChanged(); }
        }
        public Intern Intern
        {
            get { return intern; }
            set { intern = value; RaisePropertyChanged(); }
        }

        INavigationService navigationService;
        IInternRepository internRepository;
        IAppointmentRepository appointmentRepository;
        IPatientRepository patientRepository;
        public ScheduleInternPageViewModel(INavigationService navigationService, IInternRepository internRepository, IAppointmentRepository appointmentRepository, IPatientRepository patientRepository) : base(navigationService)
        {
            this.navigationService = navigationService;
            this.internRepository = internRepository;
            this.appointmentRepository = appointmentRepository;
            this.patientRepository = patientRepository;

            Meetings = new ObservableCollection<ScheduleAppointment>();
            BuscarAppointmentsAsync();
        }


        /// <summary>
        /// Creates meetings and stores in a collection.  
        /// </summary>
        public async void VisualizeAppointments(string appointmentNotes)
        {
            Appointment appointment = (await appointmentRepository.GetAllAsync()).FirstOrDefault(x => x.Id == appointmentNotes);

            switch(appointment.ConsultationType)
            {
                case Appointment._INDIVIDUAL:
                case Appointment._ORIENTACAO:
                    break;
                case Appointment._GRUPO:
                    var navParameters = new NavigationParameters();
                    navParameters.Add("patients", null);
                    await navigationService.NavigateAsync("InternAttendancePageViewModel", navParameters);
                    break;


            }
        
        }

        public async void BuscarAppointmentsAsync()
        {
            Meetings = new ObservableCollection<ScheduleAppointment>();
            Intern = await internRepository.GetAsync(Settings.UserId);
            var appointments = await appointmentRepository.GetAllByInternId(Intern.Id);

            for (int i = 0; i < appointments.Count(); i++)
            {
                ScheduleAppointment appointment = new ScheduleAppointment();
                appointment.Subject = appointments.ElementAt(i).DescricaoConsultation;

                appointment.StartTime = appointments.ElementAt(i).StartTime;
                appointment.EndTime = appointments.ElementAt(i).EndTime;
                appointment.Notes = appointments.ElementAt(i).Id;

                if (appointments.ElementAt(i).Repeat)
                {
                    RecurrenceProperties recurrenceProperties = new RecurrenceProperties();
                    recurrenceProperties.RecurrenceType = RecurrenceType.Weekly;
                    recurrenceProperties.RecurrenceRange = RecurrenceRange.NoEndDate;

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
                    appointment.RecurrenceRule = DependencyService.Get<IRecurrenceBuilder>().RRuleGenerator(recurrenceProperties, appointment.StartTime, appointment.EndTime);
                }

                Meetings.Add(appointment);

            }

        }

    }
}
