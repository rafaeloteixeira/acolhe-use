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

        public Paciente Paciente { get; set; }
        public string ServidorId { get; set; }
        public string IdAcao { get; set; }
        public ObservableCollection<ListaEntidade> EstagiarioCollection { get; set; }
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
            EstagiarioCollection = new ObservableCollection<ListaEntidade>();
        }

        internal async void OpenAppointment(Atendimento atendimento)
        {
            IEnumerable<Estagiario> estagiarios = (await estagiarioRepository.GetAllAsync()).Where(x => atendimento.EstagiariosIdCollection.Contains(x.Id));

            var navParameters = new NavigationParameters();
            navParameters.Add("estagiarios", estagiarios);
            await navigationService.NavigateAsync("DetalhesAgendamentoPage", navParameters);
        }
        internal async void LoadAppointment(DateTime date)
        {

            Atendimento atendimento = null;

            ListaEntidade dadosPaciente = new ListaEntidade
            {
                Id = Paciente.Id,
                Nome = Paciente.NomeCompleto
            };

            switch (TipoConsulta)
            {
                case Atendimento._ORIENTACAO:
                    atendimento = new Atendimento(TipoConsulta, "", ServidorId, dadosPaciente, IdAcao, 0, false, false, date, new ObservableCollection<string>(EstagiarioCollection.Select(item => item.Id).ToList()));
                    break;
                case Atendimento._GRUPO:
                    atendimento = new Atendimento(TipoConsulta, "", ServidorId, dadosPaciente, IdAcao, 0, false, true, date, new ObservableCollection<string>(EstagiarioCollection.Select(item => item.Id).ToList()));
                    break;
                case Atendimento._INDIVIDUAL:
                    atendimento = new Atendimento(TipoConsulta, "", ServidorId, dadosPaciente, IdAcao, 0, false, true, date, new ObservableCollection<string>(EstagiarioCollection.Select(item => item.Id).ToList()));
                    break;
            }

            var atendimentoMarcado = (await atendimentoRepository.GetAtendimentoByEventIdAcaoIdAsync(atendimento.EventId, IdAcao));

            if (atendimentoMarcado == null)
            {
                IEnumerable<HorarioAcao> horariosIndisponiveis = await horarioAcaoRepository.GetAtendimentosByIdAcaoAsync(IdAcao);

                if (horariosIndisponiveis.FirstOrDefault(x => x.StartTime.DayOfWeek == date.DayOfWeek && x.StartTime.Hour == date.Hour) == null)
                {
                    var action = await dialogService.DisplayActionSheetAsync("Deseja agendar o atendimento?", "Cancelar", "Agendar");

                    switch (action)
                    {
                        case "Cancelar":
                            return;
                        case "Agendar":
                            Create(atendimento);
                            break;
                    }
                }
            }
            else
            {
                atendimento = atendimentoMarcado;
                var action = await dialogService.DisplayActionSheetAsync("Selectione a operação desejada:", "Cancelar", null, "Ver detalhes", "Desmarcar");

                switch (action)
                {
                    case "Cancelar":
                        return;
                    case "Ver detalhes":
                        OpenAppointment(atendimento);
                        break;
                    case "Desmarcar":
                        Delete(atendimento);
                        break;
                }

            }
        }

        private async void Create(Atendimento atendimento)
        {
            if (atendimento != null)
            {
                await atendimentoRepository.AddAsync(atendimento);
                BuscarTodos();
            }
        }
        private async void Delete(Atendimento atendimento)
        {
            bool excluir = true;
            if (atendimento.TipoConsulta == Atendimento._GRUPO)
                excluir = await MessageService.Instance.ShowAsyncYesNo("Esta ação excluirá os horários de grupo para todas as semanas. Continuar?");

            if (excluir)
            {
                await atendimentoRepository.RemoveAsync(atendimento.Id);
                BuscarTodos();
            }
        }
        public async void BuscarTodos()
        {
            Meetings = new ScheduleAppointmentCollection();
            
            IEnumerable<Atendimento> atendimentos = await atendimentoRepository.GetAllByServidorIdPacienteIdConsultaId(ServidorId, Paciente.Id, TipoConsulta);
            IEnumerable<HorarioAcao> horariosDisponiveis = await horarioAcaoRepository.GetAtendimentosByIdAcaoAsync(IdAcao);

            BuscarAtendimentos(atendimentos);
            BuscarHorariosDisponiveis(atendimentos, horariosDisponiveis);
        }

        private void BuscarAtendimentos(IEnumerable<Atendimento> atendimentos)
        {
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
        }
        private void BuscarHorariosDisponiveis(IEnumerable<Atendimento> atendimentos, IEnumerable<HorarioAcao> horariosDisponiveis)
        {

            for (int i = 0; i < horariosDisponiveis.Count(); i++)
            {
                if (atendimentos.FirstOrDefault(x =>
                (x.TipoConsulta == Atendimento._GRUPO || x.TipoConsulta == Atendimento._INDIVIDUAL)
                && x.StartTime.DayOfWeek == horariosDisponiveis.ElementAt(i).StartTime.DayOfWeek
                && x.StartTime.Hour == horariosDisponiveis.ElementAt(i).StartTime.Hour) != null)
                    continue;

                if (Meetings.FirstOrDefault(x => x.StartTime == horariosDisponiveis.ElementAt(i).StartTime) != null)
                    continue;

                ScheduleAppointment appointment = new ScheduleAppointment();
                appointment.Subject = "Indisponível";
                appointment.Color = horariosDisponiveis.ElementAt(i).Cor;
                appointment.StartTime = horariosDisponiveis.ElementAt(i).StartTime;
                appointment.EndTime = horariosDisponiveis.ElementAt(i).EndTime;
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
            ServidorId = Settings.UserId;

            if (parameters["paciente"] != null)
            {
                Paciente = parameters["paciente"] as Paciente;
            }
            if (parameters["id_acao"] != null)
            {
                IdAcao = parameters["id_acao"].ToString();
            }
            if (parameters["estagiarios"] != null)
            {
                EstagiarioCollection = parameters["estagiarios"] as ObservableCollection<ListaEntidade>;
            }
            if (parameters["tipo_consulta"] != null)
            {
                TipoConsulta = int.Parse(parameters["tipo_consulta"].ToString());
            }

            BuscarTodos();
        }
    }
}
