using ifsp.acolheuse.mobile.Core.Repositories;
using Prism.Navigation;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using ifsp.acolheuse.mobile.Core.Domain;

namespace ifsp.acolheuse.mobile.ViewModels
{
    public class CadastroAcaoPageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _editarResponsaveisCommand { get; set; }
        public DelegateCommand _editarEstagiariosCommand { get; set; }
        public DelegateCommand _salvarAcaoCommand { get; set; }
        public DelegateCommand _configurarDiaCommand { get; set; }

        public DelegateCommand EditarResponsaveisCommand => _editarResponsaveisCommand ?? (_editarResponsaveisCommand = new DelegateCommand(EditarListaResponsaveisAsync));
        public DelegateCommand EditarEstagiariosCommand => _editarEstagiariosCommand ?? (_editarEstagiariosCommand = new DelegateCommand(EditarListaEstagiariosAsync));
        public DelegateCommand SalvarAcaoCommand => _salvarAcaoCommand ?? (_salvarAcaoCommand = new DelegateCommand(SalvarAcaoAsync));
        public DelegateCommand ConfigurarDiaCommand => _configurarDiaCommand ?? (_configurarDiaCommand = new DelegateCommand(ConfigurarDiaAsync));
        #endregion

        #region properties
        private Acao acao;
        private IEnumerable<Linha> linhasCollection;
        private string dia;
        private int tamanhoLvResponsaveis;
        private int tamanhoLvEstagiarios;

        public int TamanhoLvResponsaveis
        {
            get { return tamanhoLvResponsaveis; }
            set { tamanhoLvResponsaveis = value; RaisePropertyChanged(); }
        }
        public int TamanhoLvEstagiarios
        {
            get { return tamanhoLvEstagiarios; }
            set { tamanhoLvEstagiarios = value; RaisePropertyChanged(); }
        }
        public Acao Acao
        {
            get { return acao; }
            set { acao = value; RaisePropertyChanged(); }
        }
        public IEnumerable<Linha> LinhasCollection
        {
            get { return linhasCollection; }
            set { linhasCollection = value; RaisePropertyChanged(); }
        }
        public string Dia
        {
            get { return dia; }
            set { dia = value; RaisePropertyChanged(); }
        }
        public List<string> DiasCollection
        {
            get { return new List<string> { "Segunda-feira", "Terça-feira", "Quarta-feira", "Quinta-feira", "Sexta-feira" }; }
        }
        #endregion

        private INavigationService navigationService;
        private IAcaoRepository acaoRepository;
        private ILinhaRepository linhaRepository;

        public CadastroAcaoPageViewModel(INavigationService navigationService, IAcaoRepository acaoRepository, ILinhaRepository linhaRepository) :
           base(navigationService)
        {
            this.navigationService = navigationService;
            this.acaoRepository = acaoRepository;
            this.linhaRepository = linhaRepository;
            Title = "Cadastro de Ação";
        }

        public async void EditarListaResponsaveisAsync()
        {
            await navigationService.NavigateAsync("EdicaoListaResponsaveisPage");
        }
        public async void EditarListaEstagiariosAsync()
        {
            await navigationService.NavigateAsync("EdicaoListaEstagiariosPage");
        }
        public async void ConfigurarDiaAsync()
        {
            await navigationService.NavigateAsync("AtendimentoPage");
        }

        public async void SalvarAcaoAsync()
        {
            await acaoRepository.AddAsync(Acao);
        }

        public async void CarregarLinhaAcao(string IdAcao)
        {
            try
            {
                Acao = await acaoRepository.GetAsync(IdAcao);
                LinhasCollection = await linhaRepository.GetAllAsync();
                Dia = DiasCollection[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async void CarregarLinha()
        {
            LinhasCollection = await linhaRepository.GetAllAsync();
            Dia = DiasCollection[0];
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters["acao"] != null)
            {
                Acao = parameters["acao"] as Acao;
                CarregarLinhaAcao(Acao.Id);
            }
            else
            {
                CarregarLinha();

            }
        }
    }
}
