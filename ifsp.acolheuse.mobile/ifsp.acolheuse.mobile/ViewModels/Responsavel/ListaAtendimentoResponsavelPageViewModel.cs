using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using ifsp.acolheuse.mobile.Core.Settings;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ifsp.acolheuse.mobile.ViewModels.Responsavel
{
    public class ListaAtendimentoResponsavelPageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _novoPacienteCommand { get; set; }
        public DelegateCommand NovoPacienteCommand => _novoPacienteCommand ?? (_novoPacienteCommand = new DelegateCommand(NovoPacienteCommandAsync));
        #endregion

        #region properties
        private IEnumerable<Paciente> pacientesCollection;
        private Acao acao;

        internal async void NavigateToAtendimento(Paciente paciente)
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("acao", Acao);
            navParameters.Add("paciente", paciente);
            await navigationService.NavigateAsync("AtendimentoPage", navParameters);

        }

        public IEnumerable<Paciente> PacientesCollection
        {
            get { return pacientesCollection; }
            set { pacientesCollection = value; RaisePropertyChanged(); }
        }
        public Acao Acao
        {
            get { return acao; }
            set { acao = value; RaisePropertyChanged(); }
        }

        #endregion

        private INavigationService navigationService;
        private IServidorRepository servidorRepository;
        private IAcaoRepository acaoRepository;
        private IPacienteRepository pacienteRepository;
        public ListaAtendimentoResponsavelPageViewModel(INavigationService navigationService, IServidorRepository servidorRepository, IAcaoRepository acaoRepository, IPacienteRepository pacienteRepository) :
          base(navigationService)
        {
            this.navigationService = navigationService;
            this.servidorRepository = servidorRepository;
            this.acaoRepository = acaoRepository;
            this.pacienteRepository = pacienteRepository;
        }



        public async void NovoPacienteCommandAsync()
        {
            await navigationService.NavigateAsync("CadastroPacientePage");
        }

        public async void BuscarPacientesCollectionAsync()
        {
            //BUSCA AS AÇÕES ATENDIDAS POR ESSE SERVIDOR
            IEnumerable<Acao> acaoesAtendidas = (await acaoRepository.GetAllAsync()).Where(x => x.ResponsavelCollection != null && x.ResponsavelCollection.FirstOrDefault(m => m.Id == Settings.UserId) != null);

            //BUSCA OS USUÁRIOS ATENDIDOS PELAS AÇÕES DO SERVIDOR
            PacientesCollection = (await pacienteRepository.GetAllAsync()).Where(p => p.AcoesCollection != null && p.AcoesCollection.Any(c => acaoesAtendidas.Any(c2 => c2.Id == c.Id) && c.IsAtendimento == true));
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
            BuscarPacientesCollectionAsync();
        }
    }
}

