using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using ifsp.acolheuse.mobile.Core.Repositories;
using Prism.Navigation;
using Syncfusion.SfSchedule.XForms;
using ifsp.acolheuse.mobile.Core.Domain;
using System.Collections.ObjectModel;
using ifsp.acolheuse.mobile.Services;

namespace ifsp.acolheuse.mobile.ViewModels
{
    public class AtendimentoPageViewModel : ViewModelBase
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
        private IAtendimentoRepository horarioRepository;
        private IAcaoRepository acaoRepository;

        public AtendimentoPageViewModel(INavigationService navigationService, IAtendimentoRepository horarioRepository, IAcaoRepository acaoRepository) :
            base(navigationService)
        {
            this.navigationService = navigationService;
            this.horarioRepository = horarioRepository;
            this.acaoRepository = acaoRepository;
            Title = "My View A";
        }


        /// <summary>
        /// Creates meetings and stores in a collection.  
        /// </summary>
        public async void CreateAppointments(DateTime hora)
        {
            Atendimento atendimento = new Atendimento(Atendimento._INCLUIDO, "", null, null, Acao.Id, 0, false, false, hora, null);

            var horario = await horarioRepository.GetAtendimentoByIdAcaoEventIdAsync(Acao.Id, atendimento.EventId);

            if (horario != null)
            {
                if (await MessageService.Instance.ShowAsyncYesNo("Deseja adicionar este horário do atendimento da " + dia + "?"))
                {
                    if (atendimento != null)
                    {
                        await horarioRepository.AddAtendimentoByIdAcaoAsync(Acao.Id, atendimento);
                        BuscarHorariosAsync();
                    }
                }
            }
            else
            {
                if (await MessageService.Instance.ShowAsyncYesNo("Deseja excluir este horário do atendimento da " + dia + "?"))
                {
                    await horarioRepository.DeleteAtendimentoByIdAcaoEventIdAsync(Acao.Id, atendimento.EventId);
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
            if (parameters["acao"] != null && parameters["servidor"] != null && parameters["dia"] != null)
            {
                Acao = parameters["acao"] as Acao;
                Servidor = parameters["servidor"] as Servidor;
                dia = parameters["acao"].ToString();

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
}
