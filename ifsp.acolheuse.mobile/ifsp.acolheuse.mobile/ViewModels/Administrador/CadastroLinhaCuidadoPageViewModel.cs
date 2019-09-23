﻿using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using ifsp.acolheuse.mobile.Core.Repositories;
using Prism.Navigation;
using ifsp.acolheuse.mobile.Core.Domain;

namespace ifsp.acolheuse.mobile.ViewModels.Administrador
{
    public class CadastroLinhaCuidadoPageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _salvarLinhaCommand { get; set; }
        public DelegateCommand _editarListaAcoes { get; set; }
        public DelegateCommand SalvarLinhaCommand => _salvarLinhaCommand ?? (_salvarLinhaCommand = new DelegateCommand(SalvarAsync));
        public DelegateCommand EditarListaAcoes => _editarListaAcoes ?? (_editarListaAcoes = new DelegateCommand(SalvarAsync));

        #endregion

        #region properties
        private IEnumerable<Acao> acaoCollection;
        private Linha linha;
        public IEnumerable<Acao> AcaoCollection
        {
            get { return acaoCollection; }
            set { acaoCollection = value; RaisePropertyChanged(); }
        }
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

        public async void SalvarAsync()
        {
            await linhaRepository.AddOrUpdateAsync(Linha, Linha.Id);
            await navigationService.GoBackAsync();
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
                AcaoCollection = new System.Collections.ObjectModel.ObservableCollection<Acao>(await acaoRepository.GetAllByIdLinhaAsync(Linha.Id));
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
