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
    public class EdicaoListaEstagiariosPageViewModel : ViewModelBase
    {
        #region properties
        private ObservableCollection<ListaEntidade> estagiarioCollection;
        private Acao acao;

        public ObservableCollection<ListaEntidade> EstagiarioCollection
        {
            get { return estagiarioCollection; }
            set { estagiarioCollection = value; RaisePropertyChanged(); }
        }
        public Acao Acao
        {
            get { return acao; }
            set { acao = value; RaisePropertyChanged(); }
        }

        #endregion

        #region commands
        public DelegateCommand _salvarCommand { get; set; }

        public DelegateCommand SalvarCommand => _salvarCommand ?? (_salvarCommand = new DelegateCommand(SalvarAsync));
        #endregion

        private INavigationService navigationService;
        private IAcaoRepository acaoRepository;
        private IEstagiarioRepository estagiarioRepository;

        public EdicaoListaEstagiariosPageViewModel(INavigationService navigationService, IAcaoRepository acaoRepository, IEstagiarioRepository estagiarioRepository) :
            base(navigationService)
        {
            this.navigationService = navigationService;
            this.acaoRepository = acaoRepository;
            this.estagiarioRepository = estagiarioRepository;
            Title = "Editar Estagiários";
        }

        public async void SalvarAsync()
        {
            Acao.EstagiarioCollection = new ObservableCollection<ListaEntidade>(EstagiarioCollection.Where(x => x.Adicionado == true));
            await acaoRepository.AddOrUpdateAsync(Acao, Acao.Id);
            await NavigationService.GoBackAsync();
        }

        public async void BuscarEstagiariosCollectionAsync()
        {
            try
            {
                EstagiarioCollection = new ObservableCollection<ListaEntidade>();
                IEnumerable<Estagiario> estagiarios = await estagiarioRepository.GetAllAsync();

                for (int i = 0; i < estagiarios.Count(); i++)
                {
                    if (Acao.EstagiarioCollection?.FirstOrDefault(x => x.Id == estagiarios.ElementAt(i).Id) != null)
                    {
                        EstagiarioCollection.Add(new ListaEntidade
                        {
                            Id = estagiarios.ElementAt(i).Id,
                            Nome = estagiarios.ElementAt(i).Nome,
                            Adicionado = true
                        });
                    }
                    else
                    {
                        EstagiarioCollection.Add(new ListaEntidade
                        {
                            Id = estagiarios.ElementAt(i).Id,
                            Nome = estagiarios.ElementAt(i).Nome,
                            Adicionado = false
                        });
                    }
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
            if (parameters["acao"] != null)
            {
                Acao = parameters["acao"] as Acao;
                BuscarEstagiariosCollectionAsync();
            }
        }
    }
}
