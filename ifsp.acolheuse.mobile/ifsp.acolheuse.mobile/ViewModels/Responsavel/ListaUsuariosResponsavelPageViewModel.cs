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
    public class ListaUsuariosResponsavelPageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _novoPacienteCommand { get; set; }
        public DelegateCommand NovoPacienteCommand => _novoPacienteCommand ?? (_novoPacienteCommand = new DelegateCommand(NovoPacienteCommandAsync));
        #endregion

        #region properties
        private IEnumerable<Paciente> pacientesCollection;
        public IEnumerable<Paciente> PacientesCollection
        {
            get { return pacientesCollection; }
            set { pacientesCollection = value; RaisePropertyChanged(); }
        }

        internal async void NavigateToPacienteServidor(Paciente paciente)
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("paciente", paciente);
            await navigationService.NavigateAsync("ListaUsuariosPage", navParameters);
        }
        #endregion

        private INavigationService navigationService;
        private IServidorRepository servidorRepository;
        private IAcaoRepository acaoRepository;
        private IPacienteRepository pacienteRepository;
        public ListaUsuariosResponsavelPageViewModel(INavigationService navigationService, IServidorRepository servidorRepository, IAcaoRepository acaoRepository, IPacienteRepository pacienteRepository) :
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
            //BUSCA OS DADOS DO SERVIDOR LOGADO
            Servidor servidor = await servidorRepository.GetAsync(Settings.UserId);

            //BUSCA AS AÇÕES ATENDIDAS POR ESSE SERVIDOR
            IEnumerable<Acao> acaoesAtendidas = (await acaoRepository.GetAllAsync()).Where(x => x.ResponsavelCollection.FirstOrDefault(m => m.Id == servidor.UserId) != null);

            //BUSCA OS USUÁRIOS ATENDIDOS PELAS AÇÕES DO SERVIDOR
            PacientesCollection = (await pacienteRepository.GetAllAsync()).Where(p => p.AcoesCollection.Any(c => acaoesAtendidas.Any(c2 => c2.Id == c.Id)));
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
        }
    }
}
