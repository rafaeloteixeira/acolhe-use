using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using ifsp.acolheuse.mobile.Core.Settings;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ifsp.acolheuse.mobile.ViewModels.Responsavel
{
    public class ListaEstagiariosResponsavelPageViewModel : ViewModelBase
    {

        #region properties
        private IEnumerable<Estagiario> estagiarioCollection;
        public IEnumerable<Estagiario> EstagiarioCollection
        {
            get { return estagiarioCollection; }
            set { estagiarioCollection = value; RaisePropertyChanged(); }
        }

        internal async void NavigateToEstagiarioServidor(Estagiario estagiario)
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("estagiario", estagiario);
            await navigationService.NavigateAsync("EstagiarioServidorPage", navParameters);
        }
        #endregion

        private INavigationService navigationService;
        private IEstagiarioRepository estagiarioRepository;

        public ListaEstagiariosResponsavelPageViewModel(INavigationService navigationService, IEstagiarioRepository estagiarioRepository) :
          base(navigationService)
        {
            this.navigationService = navigationService;
            this.estagiarioRepository = estagiarioRepository;
            BuscarEstagiarioCollectionAsync(Settings.UserId);
        }

        private async void BuscarEstagiarioCollectionAsync(string KeyProfessor)
        {
            EstagiarioCollection = await estagiarioRepository.GetEstagiariosByResponsavelIdAsync(KeyProfessor);
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
        }
    }
}
