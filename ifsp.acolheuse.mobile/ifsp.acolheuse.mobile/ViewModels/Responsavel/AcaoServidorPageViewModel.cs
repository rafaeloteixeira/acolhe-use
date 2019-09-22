using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using ifsp.acolheuse.mobile.Core.Domain;
using System.Collections.ObjectModel;
using Prism.Navigation;
using ifsp.acolheuse.mobile.Core.Repositories;

namespace ifsp.acolheuse.mobile.ViewModels.Responsavel
{
    public class AcaoServidorPageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _editarResponsaveisCommand { get; set; }
        public DelegateCommand _editarEstagiariosCommand { get; set; }
        public DelegateCommand _salvarAcaoCommand { get; set; }
        public DelegateCommand _listaInterconsultaCommand { get; set; }
        public DelegateCommand _listaAtendimentoCommand { get; set; }
        public DelegateCommand _listaEsperaCommand { get; set; }
        public DelegateCommand _listarPacientesAltaCommand { get; set; }


        public DelegateCommand EditarResponsaveisCommand => _editarResponsaveisCommand ?? (_editarResponsaveisCommand = new DelegateCommand(EditarListaResponsaveisAsync));
        public DelegateCommand EditarEstagiariosCommand => _editarEstagiariosCommand ?? (_editarEstagiariosCommand = new DelegateCommand(EditarListaEstagiariosAsync));
        public DelegateCommand SalvarAcaoCommand => _salvarAcaoCommand ?? (_salvarAcaoCommand = new DelegateCommand(SalvarAcaoAsync));
        public DelegateCommand ListaInterconsultaCommand => _listaInterconsultaCommand ?? (_listaInterconsultaCommand = new DelegateCommand(ListaInterconsultaAsync));
        public DelegateCommand ListaAtendimentoCommand => _listaAtendimentoCommand ?? (_listaAtendimentoCommand = new DelegateCommand(ListaAtendimentoAsync));
        public DelegateCommand ListaEsperaCommand => _listaEsperaCommand ?? (_listaEsperaCommand = new DelegateCommand(ListaEsperaAsync));
        public DelegateCommand ListarPacientesAltaCommand => _listarPacientesAltaCommand ?? (_listarPacientesAltaCommand = new DelegateCommand(ListaAltaAsync));
        #endregion

        #region properties
        private Acao acao;
        private IEnumerable<Linha> linhasCollection;

        public Acao Acao
        {
            get { return acao; }
            set { acao = value; RaisePropertyChanged(); }
        }
        public IEnumerable<Linha> LinhasCollection
        {
            get { return linhasCollection; }
            private set { linhasCollection = value; RaisePropertyChanged(); }
        }
        #endregion

        private INavigationService navigationService;
        private ILinhaRepository linhaRepository;
        private IAcaoRepository acaoRepository;

        public AcaoServidorPageViewModel(INavigationService navigationService, ILinhaRepository linhaRepository, IAcaoRepository acaoRepository) :
          base(navigationService)
        {
            this.navigationService = navigationService;
            this.linhaRepository = linhaRepository;
            this.acaoRepository = acaoRepository;
        }

        public async void EditarListaResponsaveisAsync()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("acao", Acao);
            await navigationService.NavigateAsync("ListaServidoresPage", navParameters); //adm
        }

        public async void EditarListaEstagiariosAsync()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("acao", Acao);
            await navigationService.NavigateAsync("ListaEstagiariosPage", navParameters); //adm
        }
        public async void ListaAtendimentoAsync()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("acao", Acao);
            await navigationService.NavigateAsync("ListaAtendimentoPage", navParameters); //resp
        }
        public async void ListaInterconsultaAsync()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("acao", Acao);
            await navigationService.NavigateAsync("ListaInterconsultaPage", navParameters); //resp
        }
        public async void ListaEsperaAsync()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("acao", Acao);
            await navigationService.NavigateAsync("ListaEsperaPage", navParameters); //resp
        }
        public async void ListaAltaAsync()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("acao", Acao);
            await navigationService.NavigateAsync("ListaUsuariosAltaPage", navParameters); //resp
        }
        public async void SalvarAcaoAsync()
        {
            await acaoRepository.AddOrUpdateAsync(Acao, Acao.Id);
            await navigationService.GoBackAsync();
        }

        public async void GetLinhasAsync()
        {
            LinhasCollection = await linhaRepository.GetAllAsync();
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
            else
            {
                Acao = new Acao();
            }
        }
    }
}
