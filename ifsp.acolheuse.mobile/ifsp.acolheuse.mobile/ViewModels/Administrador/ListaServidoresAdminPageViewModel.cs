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

    public class ListaServidoresAdminPageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _novoServidorCommand { get; set; }
        public DelegateCommand NovoServidorCommand => _novoServidorCommand ?? (_novoServidorCommand = new DelegateCommand(NovoServidorCommandAsync));
        #endregion

        #region properties
        private IEnumerable<Servidor> servidoresCollection;
        public IEnumerable<Servidor> ServidoresCollection
        {
            get { return servidoresCollection; }
            set { servidoresCollection = value; RaisePropertyChanged(); }
        }

        #endregion

        private INavigationService navigationService;
        private IServidorRepository servidorRepository;

        public ListaServidoresAdminPageViewModel(INavigationService navigationService, IServidorRepository servidorRepository) :
            base(navigationService)
        {
            this.navigationService = navigationService;
            this.servidorRepository = servidorRepository;
            Title = "My View A";
        }

        internal async void ItemTapped(Servidor servidor)
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("servidor", servidor);
            navParameters.Add("admin", true);
            await navigationService.NavigateAsync("CadastroServidorPage", navParameters);
        }

        public async void NovoServidorCommandAsync()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("admin", true);
            await navigationService.NavigateAsync("CadastroServidorPage", navParameters);
        }

        public async void BuscarServidoresCollectionAsync()
        {
            try
            {
                ServidoresCollection = await servidorRepository.GetAllAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
