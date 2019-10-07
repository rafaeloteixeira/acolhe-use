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
    public class AgendaEstagiarioPageViewModel : ViewModelBase
    {
        private ObservableCollection<ScheduleAppointment> meetings;
        private Estagiario estagiario;

        public ObservableCollection<ScheduleAppointment> Meetings
        {
            get { return meetings; }
            set { meetings = value; RaisePropertyChanged(); }
        }
        public Estagiario Estagiario
        {
            get { return estagiario; }
            set { estagiario = value; RaisePropertyChanged(); }
        }

        INavigationService navigationService;
        IEstagiarioRepository estagiarioRepository;
        IAtendimentoRepository atendimentoRepository;
        public AgendaEstagiarioPageViewModel(INavigationService navigationService, IEstagiarioRepository estagiarioRepository, IAtendimentoRepository atendimentoRepository) : base(navigationService)
        {
            this.navigationService = navigationService;
            this.estagiarioRepository = estagiarioRepository;
            this.atendimentoRepository = atendimentoRepository;

            Meetings = new ObservableCollection<ScheduleAppointment>();
            BuscarAtendimentosAsync();
        }


        /// <summary>
        /// Creates meetings and stores in a collection.  
        /// </summary>
        public async void VisualizeAppointments()
        {
            if (await MessageService.Instance.ShowAsyncYesNo("Deseja visualizar os detalhes do atendimento?"))
            {

            }
        }

        public async void BuscarAtendimentosAsync()
        {
            Meetings = new ObservableCollection<ScheduleAppointment>();
            Estagiario = await estagiarioRepository.GetAsync(Settings.UserId);
            var atendimentos = await atendimentoRepository.GetAllByEstagiarioId(Estagiario.Id);

            for (int i = 0; i < atendimentos.Count(); i++)
            {
                ScheduleAppointment appointment = new ScheduleAppointment();
                appointment.Subject = atendimentos.ElementAt(i).DescricaoConsulta;
                appointment.Color = atendimentos.ElementAt(i).Cor;
                appointment.StartTime = atendimentos.ElementAt(i).StartTime;
                appointment.EndTime = atendimentos.ElementAt(i).EndTime;

                if (atendimentos.ElementAt(i).Repeat)
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
