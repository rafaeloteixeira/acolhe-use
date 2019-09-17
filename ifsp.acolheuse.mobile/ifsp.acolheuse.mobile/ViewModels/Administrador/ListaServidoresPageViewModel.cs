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

    public class ListaServidoresPageViewModel : ViewModelBase
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

        public ListaServidoresPageViewModel(INavigationService navigationService, IServidorRepository servidorRepository) :
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
            await navigationService.NavigateAsync("CadastroServidorPage", navParameters);
        }

        public async void NovoServidorCommandAsync()
        {
            await navigationService.NavigateAsync("CadastroServidorPage");
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
