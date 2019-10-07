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

namespace ifsp.acolheuse.mobile.ViewModels.Responsavel
{
    public class AgendaServidorPageViewModel : ViewModelBase
    {
        #region properties

        private ObservableCollection<ScheduleAppointment> meetings;
        private Servidor servidor;

        public ObservableCollection<ScheduleAppointment> Meetings
        {
            get { return meetings; }
            set { meetings = value; RaisePropertyChanged(); }
        }
        public Servidor Servidor
        {
            get { return servidor; }
            set { servidor = value; RaisePropertyChanged(); }
        }
        #endregion

        private INavigationService navigationService;
        private IServidorRepository servidorRepository;
        private IAtendimentoRepository atendimentoRepository;
        private IEstagiarioRepository estagiarioRepository;

        public AgendaServidorPageViewModel(INavigationService navigationService, IServidorRepository servidorRepository, IAtendimentoRepository atendimentoRepository, IEstagiarioRepository estagiarioRepository) :
          base(navigationService)
        {
            this.navigationService = navigationService;
            this.servidorRepository = servidorRepository;
            this.atendimentoRepository = atendimentoRepository;
            this.estagiarioRepository = estagiarioRepository;
            Meetings = new ObservableCollection<ScheduleAppointment>();
            BuscarAtendimentosAsync();
        }

        /// <summary>
        /// Creates meetings and stores in a collection.  
        /// </summary>
        public async void VisualizeAppointments(string appointmentNotes)
        {
            Atendimento atendimento = (await atendimentoRepository.GetAllAsync()).FirstOrDefault(x => x.Id == appointmentNotes);
            if (await MessageService.Instance.ShowAsyncYesNo("Deseja visualizar os detalhes do atendimento?"))
            {
                IEnumerable<Estagiario> estagiarios = (await estagiarioRepository.GetAllAsync()).Where(x => atendimento.EstagiariosIdCollection.Contains(x.Id));

                var navParameters = new NavigationParameters();
                navParameters.Add("estagiarios", estagiarios);
                navParameters.Add("usuario", atendimento.Paciente);
                navParameters.Add("horario", atendimento.StartTime);
                navParameters.Add("tipo_consulta", atendimento.TipoConsulta);
                await navigationService.NavigateAsync("DetalhesAgendamentoPage", navParameters);
            }
        }

        public async void BuscarAtendimentosAsync()
        {
            Meetings = new ObservableCollection<ScheduleAppointment>();
            Servidor = await servidorRepository.GetAsync(Settings.UserId);

            var atendimentos = (await atendimentoRepository.GetAllAsync()).Where(x => x.IdServidor == Servidor.Id);

            for (int i = 0; i < atendimentos.Count(); i++)
            {
                ScheduleAppointment appointment = new ScheduleAppointment();
                appointment.Subject = atendimentos.ElementAt(i).DescricaoConsulta;
                appointment.Color = atendimentos.ElementAt(i).Cor;
                appointment.StartTime = atendimentos.ElementAt(i).StartTime;
                appointment.EndTime = atendimentos.ElementAt(i).EndTime;
                appointment.Notes = atendimentos.ElementAt(i).Id;
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
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
        }
    }
}
