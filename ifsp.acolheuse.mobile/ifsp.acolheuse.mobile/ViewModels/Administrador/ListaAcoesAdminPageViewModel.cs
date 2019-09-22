using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using ifsp.acolheuse.mobile.Core.Repositories;
using Prism.Navigation;
using ifsp.acolheuse.mobile.Core.Domain;
using System.Collections.ObjectModel;

namespace ifsp.acolheuse.mobile.ViewModels.Administrador
{
    public class ListaAcoesAdminPageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _novaAcaoCommand { get; set; }

        public DelegateCommand NovaAcaoCommand => _novaAcaoCommand ?? (_novaAcaoCommand = new DelegateCommand(NovaAcaoAsync));
        #endregion

        #region properties
        private IEnumerable<Linha> linhasCollection;
        private Linha linha;

        public IEnumerable<Linha> LinhasCollection
        {
            get { return linhasCollection; }
            set { linhasCollection = value; RaisePropertyChanged(); }
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

        public ListaAcoesAdminPageViewModel(INavigationService navigationService, ILinhaRepository linhaRepository, IAcaoRepository acaoRepository) :
            base(navigationService)
        {
            this.navigationService = navigationService;
            this.linhaRepository = linhaRepository;
            this.acaoRepository = acaoRepository;
            Title = "My View A";
        }
        public async void NovaAcaoAsync()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("linha", Linha);
            await navigationService.NavigateAsync("CadastroAcaoPage", navParameters);
        }
        internal async void ItemTapped(Acao acao)
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("acao", acao);
            await navigationService.NavigateAsync("CadastroAcaoPage", navParameters);
        }
        public async void BuscarLinhasCollectionAsync()
        {
            try
            {
                LinhasCollection = await linhaRepository.GetAllAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async void BuscarAcoesCollectionAsync()
        {
            try
            {
                Linha.AcaoCollection = new ObservableCollection<Acao>(await acaoRepository.GetAllByIdLinhaAsync(Linha.Id));
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
        }
    }
}
