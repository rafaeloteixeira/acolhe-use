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
    public class HorarioAcaoPageViewModel : ViewModelBase
    {
        #region properties
        private ObservableCollection<ScheduleAppointment> horarios;
        private Acao acao;
        private Servidor servidor;
        private DateTime date;
        private string dia;

        public ObservableCollection<ScheduleAppointment> Horarios
        {
            get { return horarios; }
            set { horarios = value; RaisePropertyChanged(); }
        }
        public Acao Acao
        {
            get { return acao; }
            set { acao = value; RaisePropertyChanged(); }
        }
        public Servidor Servidor
        {
            get { return servidor; }
            set { servidor = value; RaisePropertyChanged(); }
        }
        public DateTime Date
        {
            get { return date; }
            set { date = value; RaisePropertyChanged(); }
        }

        #endregion

        private INavigationService navigationService;
        private IHorarioAcaoRepository horarioRepository;
        private IAcaoRepository acaoRepository;

        public HorarioAcaoPageViewModel(INavigationService navigationService, IHorarioAcaoRepository horarioRepository, IAcaoRepository acaoRepository) :
            base(navigationService)
        {
            this.navigationService = navigationService;
            this.horarioRepository = horarioRepository;
            this.acaoRepository = acaoRepository;

            Horarios = new ObservableCollection<ScheduleAppointment>();
            Title = "My View A";
        }
        /// <summary>
        /// Creates meetings and stores in a collection.  
        /// </summary>
        public async void CreateAppointments(DateTime hora)
        {
            HorarioAcao horario = new HorarioAcao(Acao.Id, hora);

            var horarioAtendimento = await horarioRepository.GetAtendimentoByIdAcaoEventIdAsync(Acao.Id, horario.EventId);

            if (horarioAtendimento == null)
            {
                if (await MessageService.Instance.ShowAsyncYesNo("Deseja adicionar este horário do atendimento da " + dia + "?"))
                {
                    if (horario != null)
                    {
                        await horarioRepository.AddAtendimentoByIdAcaoAsync(Acao.Id, horario);
                        BuscarHorariosAsync();
                    }
                }
            }
            else
            {
                if (await MessageService.Instance.ShowAsyncYesNo("Deseja excluir este horário do atendimento da " + dia + "?"))
                {
                    await horarioRepository.DeleteAtendimentoByIdAcaoEventIdAsync(Acao.Id, horario.EventId);
                    BuscarHorariosAsync();
                }
            }
        }

        public async void BuscarHorariosAsync()
        {
            try
            {
                Horarios = new ObservableCollection<ScheduleAppointment>();
                Acao = await acaoRepository.GetAsync(Acao.Id);

                var ex = await horarioRepository.GetAtendimentosByIdAcaoAsync(Acao.Id);

                for (int i = 0; i < ex.Count(); i++)
                {
                    ScheduleAppointment appointment = new ScheduleAppointment();
                    appointment.Subject = "(+) Marcar";
                    appointment.Color = ex.ElementAt(i).Cor;
                    appointment.StartTime = ex.ElementAt(i).StartTime;
                    appointment.EndTime = ex.ElementAt(i).EndTime;
                    Horarios.Add(appointment);
                }
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
            if (parameters["acao"] != null)
            {
                Acao = parameters["acao"] as Acao;
            }
            if (parameters["servidor"] != null)
            {
                Servidor = parameters["servidor"] as Servidor;
            }
            if (parameters["dia"] != null)
            {
                dia = parameters["dia"].ToString();
            }

            switch (dia)
            {
                case "Segunda-feira":
                    Date = new DateTime(0001, 01, 03, 08, 0, 0);
                    break;
                case "Terça-feira":
                    Date = new DateTime(0001, 01, 04, 08, 0, 0);
                    break;
                case "Quarta-feira":
                    Date = new DateTime(0001, 01, 05, 08, 0, 0);
                    break;
                case "Quinta-feira":
                    Date = new DateTime(0001, 01, 06, 08, 0, 0);
                    break;
                case "Sexta-feira":
                    Date = new DateTime(0001, 01, 07, 08, 0, 0);
                    break;
            }

            BuscarHorariosAsync();
        }
    }
}
