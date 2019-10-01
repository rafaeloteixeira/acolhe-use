using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using ifsp.acolheuse.mobile.Core.Settings;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ifsp.acolheuse.mobile.ViewModels.Responsavel
{
    public class ListaAcoesResponsavelPageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _novaAcaoCommand { get; set; }
        public DelegateCommand NovaAcaoCommand => _novaAcaoCommand ?? (_novaAcaoCommand = new DelegateCommand(NovaAcaoAsync));
        #endregion

        #region properties
        private IEnumerable<Acao> acaoCollection;
        private ObservableCollection<Linha> linhasCollection;
        private Linha linha;

        public IEnumerable<Acao> AcaoCollection
        {
            get { return acaoCollection; }
            set { acaoCollection = value; RaisePropertyChanged(); }
        }
        public ObservableCollection<Linha> LinhasCollection
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



        private IAcaoRepository acaoRepository;
        private ILinhaRepository linhaRepository;
        public ListaAcoesResponsavelPageViewModel(INavigationService navigationService, IAcaoRepository acaoRepository, ILinhaRepository linhaRepository) :
          base(navigationService)
        {
            this.navigationService = navigationService;
            this.acaoRepository = acaoRepository;
            this.linhaRepository = linhaRepository;
            Linha = new Linha();
        }

        internal async void NavigateToAcao(Acao acao)
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("acao", acao);
            await navigationService.NavigateAsync("AcaoServidorPage", navParameters);
        }
        public async void NovaAcaoAsync()
        {
            await navigationService.NavigateAsync("CadastroAcaoPage");
        }



        private async Task<ConcurrentBag<Linha>> BuscarLinhasCollectionAsync()
        {
            ConcurrentBag<Linha> linhas = new ConcurrentBag<Linha>();
            ObservableCollection<Acao> acoes = new ObservableCollection<Acao>((await acaoRepository.GetAllAsync()).Where(x => x.ResponsavelCollection != null && x.ResponsavelCollection.FirstOrDefault(m => m.Id == Settings.UserId) != null));

            for (int i = 0; i < acoes.Count(); i++)
            {
                Linha linhaResponsavel = linhas.FirstOrDefault(x => x.Id == acoes[i].IdLinha);

                if (linhaResponsavel == null)
                {
                    linhaResponsavel = await linhaRepository.GetAsync(acoes[i].IdLinha);
                    linhas.Add(linhaResponsavel);
                }
            }

            return linhas;
        }

        internal async void BuscarAcoesCollectionAsync()
        {
            AcaoCollection = await acaoRepository.GetAllByIdLinhaAsync(Linha.Id);
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            var navigationMode = parameters.GetNavigationMode();
            if (navigationMode != NavigationMode.Back)
            {

                Task taskA = Task.Run(async () =>
                     LinhasCollection = new ObservableCollection<Linha>(await BuscarLinhasCollectionAsync())
                );
            }
        }
    }
}
