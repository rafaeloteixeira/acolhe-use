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

    public class ListResponsibleAdminPageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _novoResponsibleCommand { get; set; }
        public DelegateCommand NovoResponsibleCommand => _novoResponsibleCommand ?? (_novoResponsibleCommand = new DelegateCommand(NovoResponsibleCommandAsync));
        #endregion

        #region properties
        private IEnumerable<Responsible> responsibleesCollection;
        public IEnumerable<Responsible> ResponsibleCollection
        {
            get { return responsibleesCollection; }
            set { responsibleesCollection = value; RaisePropertyChanged(); }
        }

        #endregion

        private INavigationService navigationService;
        private IResponsibleRepository responsibleRepository;

        public ListResponsibleAdminPageViewModel(INavigationService navigationService, IResponsibleRepository responsibleRepository) :
            base(navigationService)
        {
            this.navigationService = navigationService;
            this.responsibleRepository = responsibleRepository;
            Title = "My View A";
        }

        internal async void ItemTapped(Responsible responsible)
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("responsible", responsible);
            navParameters.Add("admin", true);
            await navigationService.NavigateAsync("RegisterResponsiblePage", navParameters);
        }

        public async void NovoResponsibleCommandAsync()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("admin", true);
            await navigationService.NavigateAsync("RegisterResponsiblePage", navParameters);
        }

        public async void BuscarResponsibleCollectionAsync()
        {
            try
            {
                ResponsibleCollection = await responsibleRepository.GetAllAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
