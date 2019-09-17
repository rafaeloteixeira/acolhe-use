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
    public class ListaEstagiariosPageViewModel : ViewModelBase
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

        public ListaEstagiariosPageViewModel(INavigationService navigationService, IEstagiarioRepository estagiarioRepository) :
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
            await navigationService.NavigateAsync("EstagiarioPerfilPage", navParameters);
        }

        public async void NovoEstagiarioCommandAsync()
        {
            await navigationService.NavigateAsync("EstagiarioPerfilPage");
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
