using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using ifsp.acolheuse.mobile.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ifsp.acolheuse.mobile.ViewModels.Responsavel
{
    public class AtendimentoPageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _interconsultaCommand { get; set; }
        public DelegateCommand _agendarOrientacaoCommand { get; set; }
        public DelegateCommand _agendarIndividualCommand { get; set; }
        public DelegateCommand _agendarGrupoCommand { get; set; }
        public DelegateCommand _darAltaCommand { get; set; }

        public DelegateCommand InterconsultaCommand => _interconsultaCommand ?? (_interconsultaCommand = new DelegateCommand(EnviarInterconsultaAsync));
        public DelegateCommand AgendarOrientacaoCommand => _agendarOrientacaoCommand ?? (_agendarOrientacaoCommand = new DelegateCommand(AgendarOrientacaoAsync));
        public DelegateCommand AgendarIndividualCommand => _agendarIndividualCommand ?? (_agendarIndividualCommand = new DelegateCommand(AgendarIndividualAsync));
        public DelegateCommand AgendarGrupoCommand => _agendarGrupoCommand ?? (_agendarGrupoCommand = new DelegateCommand(AgendarGrupoAsync));
        public DelegateCommand DarAltaCommand => _darAltaCommand ?? (_darAltaCommand = new DelegateCommand(DarAltaAsync));
        #endregion

        #region properties
        private Paciente paciente;
        private Acao acao;
        private DateTime dataHora;
        private bool isRepetir;
        private bool orientacaoVisible;
        private bool individualVisible;
        private bool grupoVisible;

        public Paciente Paciente
        {
            get { return paciente; }
            set { paciente = value; RaisePropertyChanged(); }
        }
        public Acao Acao
        {
            get { return acao; }
            set { acao = value; RaisePropertyChanged(); }
        }
        public DateTime DataHora
        {
            get { return dataHora; }
            set { dataHora = value; RaisePropertyChanged(); }
        }
        public bool IsRepetir
        {
            get { return isRepetir; }
            set { isRepetir = value; RaisePropertyChanged(); }
        }
        public bool OrientacaoVisible
        {
            get { return orientacaoVisible; }
            set { orientacaoVisible = value; RaisePropertyChanged(); }
        }
        public bool IndividualVisible
        {
            get { return individualVisible; }
            set { individualVisible = value; RaisePropertyChanged(); }
        }
        public bool GrupoVisible
        {
            get { return grupoVisible; }
            set { grupoVisible = value; RaisePropertyChanged(); }
        }

        #endregion

        private INavigationService navigationService;
        private IPacienteRepository pacienteRepository;
        public AtendimentoPageViewModel(INavigationService navigationService, IPacienteRepository pacienteRepository) :
          base(navigationService)
        {
            this.navigationService = navigationService;
            this.pacienteRepository = pacienteRepository;
        }

        public async void AgendarOrientacaoAsync()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("paciente", Paciente);
            navParameters.Add("acao", Acao);
            navParameters.Add("tipo_consulta", Atendimento._ORIENTACAO);
            await navigationService.NavigateAsync("InclusaoEstagiariosAtendimentoPage", navParameters); //resp
        }
        public async void AgendarIndividualAsync()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("paciente", Paciente);
            navParameters.Add("acao", Acao);
            navParameters.Add("tipo_consulta", Atendimento._INDIVIDUAL);
            await navigationService.NavigateAsync("InclusaoEstagiariosAtendimentoPage", navParameters); //resp
        }
        public async void AgendarGrupoAsync()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("paciente", Paciente);
            navParameters.Add("acao", Acao);
            navParameters.Add("tipo_consulta", Atendimento._GRUPO);
            await navigationService.NavigateAsync("InclusaoEstagiariosAtendimentoPage", navParameters); //resp
        }
        public async void EnviarInterconsultaAsync()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("paciente", Paciente);
            navParameters.Add("acao", Acao);
            await navigationService.NavigateAsync("InclusaoProfessorInterconsultaPage", navParameters); //resp
        }

        public async void DarAltaAsync()
        {

            if (await MessageService.Instance.ShowAsyncYesNo("Deseja dar alta ao usuário?"))
            {
                var acao = Paciente.AcoesCollection.FirstOrDefault(x => x.Id == Acao.Id);
                acao.IsAlta = true;
                acao.IsInterconsulta = false;
                acao.IsAtendimento = false;
                acao.IsListaEspera = false;

                await pacienteRepository.AddOrUpdateAsync(Paciente, Paciente.Id);
                await navigationService.GoBackAsync();
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

            if (Acao.IsOrientation)
                OrientacaoVisible = true;

            if (Acao.IsIndividual)
                IndividualVisible = true;

            if (Acao.IsGroup)
                GrupoVisible = true;
        }
    }
}
