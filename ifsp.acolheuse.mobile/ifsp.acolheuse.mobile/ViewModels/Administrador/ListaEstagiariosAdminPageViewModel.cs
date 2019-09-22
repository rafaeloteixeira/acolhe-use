using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using ifsp.acolheuse.mobile.Core.Repositories;
using Prism.Navigation;
using ifsp.acolheuse.mobile.Core.Domain;

namespace ifsp.acolheuse.mobile.ViewModels.Administrador
{
    public class ListaEstagiariosAdminPageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _novoEstagiarioCommand { get; set; }
        public DelegateCommand NovoEstagiarioCommand => _novoEstagiarioCommand ?? (_novoEstagiarioCommand = new DelegateCommand(NovoEstagiarioCommandAsync));
        #endregion
        
        #region properties
        private IEnumerable<Estagiario> estagiarioCollection;
        public IEnumerable<Estagiario> EstagiarioCollection
        {
            get { return estagiarioCollection; }
            set { estagiarioCollection = value; RaisePropertyChanged(); }
        }

        #endregion

        private INavigationService navigationService;
        private IEstagiarioRepository estagiarioRepository;

        public ListaEstagiariosAdminPageViewModel(INavigationService navigationService, IEstagiarioRepository estagiarioRepository) :
            base(navigationService)
        {
            this.navigationService = navigationService;
            this.estagiarioRepository = estagiarioRepository;
            Title = "My View A";
        }
        internal async void ItemTapped(Estagiario estagiario)
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("estagiario", estagiario);
            navParameters.Add("admin", true);
            await navigationService.NavigateAsync("CadastroEstagiarioPage", navParameters);
        }

        public async void NovoEstagiarioCommandAsync()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("admin", true);
            await navigationService.NavigateAsync("CadastroEstagiarioPage", navParameters);
        }

        public async void BuscarEstagiarioCollectionAsync()
        {
            try
            {
                EstagiarioCollection = await estagiarioRepository.GetAllAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
