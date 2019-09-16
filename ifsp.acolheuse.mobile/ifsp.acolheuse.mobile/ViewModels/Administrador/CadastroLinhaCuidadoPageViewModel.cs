using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using ifsp.acolheuse.mobile.Core.Repositories;
using Prism.Navigation;
using ifsp.acolheuse.mobile.Core.Domain;

namespace ifsp.acolheuse.mobile.ViewModels
{
    public class CadastroLinhaCuidadoPageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _salvarLinhaCommand { get; set; }
        public DelegateCommand _editarListaAcoes { get; set; }
        public DelegateCommand SalvarLinhaCommand => _salvarLinhaCommand ?? (_salvarLinhaCommand = new DelegateCommand(EditarListaResponsaveisAsync));
        public DelegateCommand EditarListaAcoes => _editarListaAcoes ?? (_editarListaAcoes = new DelegateCommand(EditarListaResponsaveisAsync));

        #endregion

        #region properties
        private Linha linha;
        public Linha Linha
        {
            get { return linha; }
            set { linha = value; RaisePropertyChanged(); }
        }
        #endregion

        private INavigationService navigationService;
        private ILinhaRepository linhaRepository;
        private IAcaoRepository acaoRepository;
        private string titlePage = "title";

        public CadastroLinhaCuidadoPageViewModel(INavigationService navigationService, ILinhaRepository linhaRepository, IAcaoRepository acaoRepository)
        : base(navigationService)
        {
            this.navigationService = navigationService;
            this.linhaRepository = linhaRepository;
            this.acaoRepository = acaoRepository;
            Linha = new Linha();
            Title = titlePage;

        }
        internal async void OpenCadastroAcao(Acao acao)
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("acao", acao);
            await navigationService.NavigateAsync("CadastroAcaoPage", navParameters);
        }

        public async void EditarListaResponsaveisAsync()
        {
            if (String.IsNullOrEmpty(Linha.Id))
                Linha.Id = Guid.NewGuid().ToString("N");

            await linhaRepository.AddAsync(Linha);

            GetLinhaAsync();
        }

        public async void GetLinhaAsync()
        {
            if (!String.IsNullOrEmpty(Linha.Id))
            {
                Linha = await linhaRepository.GetAsync(Linha.Id);
                GetAcoesAsync();
            }
        }

        private async void GetAcoesAsync()
        {
            if (Linha != null && !String.IsNullOrEmpty(Linha.Id))
            {
                Linha.AcaoCollection = await acaoRepository.GetAllByIdLinhaAsync(Linha.Id);
            }
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
    
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters["linha"] != null)
            {
                Linha = parameters["linha"] as Linha;
            }
        }
    }
}
