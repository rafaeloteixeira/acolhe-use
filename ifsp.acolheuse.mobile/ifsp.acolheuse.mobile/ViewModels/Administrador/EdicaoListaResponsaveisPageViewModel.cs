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

    public class EdicaoListaResponsaveisPageViewModel : ViewModelBase
    {
        #region properties
        private ObservableCollection<ListaEntidade> responsavelCollection;
        private Acao acao;

        public ObservableCollection<ListaEntidade> ResponsavelCollection
        {
            get { return responsavelCollection; }
            set { responsavelCollection = value; RaisePropertyChanged(); }
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
        private IServidorRepository servidorRepository;

        public EdicaoListaResponsaveisPageViewModel(INavigationService navigationService, IAcaoRepository acaoRepository, IServidorRepository servidorRepository) :
            base(navigationService)
        {
            this.navigationService = navigationService;
            this.acaoRepository = acaoRepository;
            this.servidorRepository = servidorRepository;
            Title = "Editar Responsáveis";
        }

        public async void SalvarAsync()
        {
            Acao.ResponsavelCollection = new ObservableCollection<ListaEntidade>(ResponsavelCollection.Where(x => x.Adicionado == true));
            await acaoRepository.AddOrUpdateAsync(Acao, Acao.Id);
            await NavigationService.GoBackAsync();
        }

        public async void BuscarServidoresCollectionAsync()
        {
            try
            {
                ResponsavelCollection = new ObservableCollection<ListaEntidade>();

                IEnumerable<Servidor> servidores = await servidorRepository.GetAllAsync();

                for (int i = 0; i < servidores.Count(); i++)
                {
                    if (Acao.ResponsavelCollection?.FirstOrDefault(x => x.Id == servidores.ElementAt(i).Id) != null)
                    {
                        ResponsavelCollection.Add(new ListaEntidade
                        {
                            Id = servidores.ElementAt(i).Id,
                            Nome = servidores.ElementAt(i).Nome,
                            Adicionado = true
                        });
                    }
                    else
                    {
                        ResponsavelCollection.Add(new ListaEntidade
                        {
                            Id = servidores.ElementAt(i).Id,
                            Nome = servidores.ElementAt(i).Nome,
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
                BuscarServidoresCollectionAsync();
            }

        }
    }
}
