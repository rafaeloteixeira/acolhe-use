using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using ifsp.acolheuse.mobile.Core.Settings;
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

namespace ifsp.acolheuse.mobile.ViewModels.Responsavel
{
    public class AgendaAtendimentoPageViewModel : ViewModelBase
    {
        #region properties
        private ObservableCollection<ScheduleAppointment> meetings;
        private bool isRemovable;
        private Atendimento atendimento;
        public ObservableCollection<ScheduleAppointment> Meetings
        {
            get { return meetings; }
            set { meetings = value; RaisePropertyChanged(); }
        }
        public bool IsRemovable
        {
            get { return isRemovable; }
            set { isRemovable = value; RaisePropertyChanged(); }
        }

        public Paciente Paciente { get; set; }
        public Servidor Servidor { get; set; }
        public Acao Acao { get; set; }
        public int TipoConsulta { get; set; }

        #endregion

        private INavigationService navigationService;
        private IPageDialogService dialogService;
        private IEstagiarioRepository estagiarioRepository;
        private IAtendimentoRepository atendimentoRepository;
        private IHorarioAcaoRepository horarioAcaoRepository;
        private IServidorRepository servidorRepository;
        public AgendaAtendimentoPageViewModel(INavigationService navigationService, IEstagiarioRepository estagiarioRepository, IAtendimentoRepository atendimentoRepository, IHorarioAcaoRepository horarioAcaoRepository, IServidorRepository servidorRepository, IPageDialogService dialogService) :
          base(navigationService)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.estagiarioRepository = estagiarioRepository;
            this.atendimentoRepository = atendimentoRepository;
            this.horarioAcaoRepository = horarioAcaoRepository;
            this.servidorRepository = servidorRepository;

            Meetings = new ObservableCollection<ScheduleAppointment>();
        }

        internal async void OpenAppointment()
        {
            IEnumerable<Estagiario> estagiarios = (await estagiarioRepository.GetAllAsync()).Where(x => atendimento.EstagiariosIdCollection.Contains(x.Id));

            var navParameters = new NavigationParameters();
            navParameters.Add("estagiarios", estagiarios);
            await navigationService.NavigateAsync("DetalhesAgendamentoPage", navParameters);
        }
        internal async void LoadAppointment(DateTime agendamento)
        {

            atendimento = null;
            switch (TipoConsulta)
            {
                case Atendimento._ORIENTACAO:
                    atendimento = new Atendimento(TipoConsulta, "", Servidor.UserId, Paciente, Acao.Id, 0, false, false, agendamento, new ObservableCollection<string>(Acao.EstagiarioCollection.Select(item => item.Id).ToList()));
                    break;
                case Atendimento._GRUPO:
                    atendimento = new Atendimento(TipoConsulta, "", Servidor.UserId, Paciente, Acao.Id, 0, false, true, agendamento, new ObservableCollection<string>(Acao.EstagiarioCollection.Select(item => item.Id).ToList()));
                    break;
                case Atendimento._INDIVIDUAL:
                    atendimento = new Atendimento(TipoConsulta, "", Servidor.UserId, Paciente, Acao.Id, 0, false, true, agendamento, new ObservableCollection<string>(Acao.EstagiarioCollection.Select(item => item.Id).ToList()));
                    break;
            }

            var atendimentos = (await atendimentoRepository.GetAllAsync()).Where(x => x.EventId == atendimento.EventId);

            if (atendimentos.Count() == 0)
                IsRemovable = false;
            else
                IsRemovable = true;
        }

        /// <summary>
        /// Creates meetings and stores in a collection.  
        /// </summary>
        public async void CreateDeleteAppointments(DateTime? Date)
        {
            if (IsRemovable)
            {
                Delete();
            }
            else
            {
                var atendimentos = (await atendimentoRepository.GetAllAsync()).Where(x => x.EventId == atendimento.EventId);

                if (atendimentos.Count() == 0)
                {
                    var horariosDisponiveis = (await horarioAcaoRepository.GetAtendimentosByIdAcaoAsync(Acao.Id))
                        .Where
                        (
                            x => x.StartTime.DayOfWeek == atendimento.StartTime.DayOfWeek
                            && x.StartTime.Hour == atendimento.StartTime.Hour
                        );

                    if (horariosDisponiveis.Count() > 0)
                    {
                        Create();
                    }
                }
            }
        }

        private async void Create()
        {
            if (await MessageService.Instance.ShowAsyncYesNo("Deseja agendar o atendimento?"))
            {
                if (atendimento != null)
                {
                    await atendimentoRepository.AddAsync(atendimento);
                    BuscarAtendimentos();
                }
            }
        }
        private async void Delete()
        {
            bool excluir = false;
            if (atendimento.TipoConsulta == Atendimento._GRUPO)
                excluir = await MessageService.Instance.ShowAsyncYesNo("Deseja excluir o atendimento? Esta ação excluirá os horários de grupo para todas as semanas");
            else
                excluir = await MessageService.Instance.ShowAsyncYesNo("Deseja excluir o atendimento?");

            if (excluir)
            {
                await atendimentoRepository.RemoveAsync(atendimento.Id);
                BuscarAtendimentos();
            }
        }
        public async void BuscarAtendimentos()
        {
            Meetings = new ObservableCollection<ScheduleAppointment>();
            Servidor = await servidorRepository.GetAsync(Settings.UserId);

            var atendimentos = (await atendimentoRepository.GetAllAsync())
                .Where
                (
                    x => x.IdServidor == Servidor.UserId
                    && x.Paciente.Id == Paciente.Id && x.TipoConsulta == TipoConsulta
                );

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

                    appointment.RecurrenceRule = Xamarin.Forms.DependencyService.Get<IRecurrenceBuilder>().RRuleGenerator(recurrenceProperties, appointment.StartTime, appointment.EndTime);
                }


                Meetings.Add(appointment);
            }

            //DISPONÍVEIS

            var ex = await horarioAcaoRepository.GetAtendimentosByIdAcaoAsync(Acao.Id);

            for (int i = 0; i < ex.Count(); i++)
            {
                if (atendimentos.FirstOrDefault(x => x.StartTime.DayOfWeek == ex.ElementAt(i).StartTime.DayOfWeek && x.StartTime.Hour == ex.ElementAt(i).StartTime.Hour) != null)
                    continue;

                ScheduleAppointment appointment = new ScheduleAppointment();
                appointment.Subject = "(+) Marcar";
                appointment.Color = ex.ElementAt(i).Cor;
                appointment.StartTime = ex.ElementAt(i).StartTime;
                appointment.EndTime = ex.ElementAt(i).EndTime;
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
                appointment.RecurrenceRule = Xamarin.Forms.DependencyService.Get<IRecurrenceBuilder>().RRuleGenerator(recurrenceProperties, appointment.StartTime, appointment.EndTime);

                Meetings.Add(appointment);
            }
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters["paciente"] != null)
            {
                Paciente = parameters["paciente"] as Paciente;
            }
            if (parameters["acao"] != null)
            {
                Acao = parameters["acao"] as Acao;
            }
            if (parameters["tipoConsulta"] != null)
            {
                TipoConsulta = int.Parse(parameters["tipoConsulta"].ToString());
            }
        }
    }
}
