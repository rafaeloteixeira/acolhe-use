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
    public class ListInternsAdminPageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _novoInternCommand { get; set; }
        public DelegateCommand NovoInternCommand => _novoInternCommand ?? (_novoInternCommand = new DelegateCommand(NovoInternCommandAsync));
        #endregion
        
        #region properties
        private IEnumerable<Intern> internCollection;
        public IEnumerable<Intern> InternCollection
        {
            get { return internCollection; }
            set { internCollection = value; RaisePropertyChanged(); }
        }

        #endregion

        private INavigationService navigationService;
        private IInternRepository internRepository;

        public ListInternsAdminPageViewModel(INavigationService navigationService, IInternRepository internRepository) :
            base(navigationService)
        {
            this.navigationService = navigationService;
            this.internRepository = internRepository;
            Title = "My View A";
        }
        internal async void ItemTapped(Intern intern)
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("intern", intern);
            navParameters.Add("admin", true);
            await navigationService.NavigateAsync("RegisterInternPage", navParameters);
        }

        public async void NovoInternCommandAsync()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("admin", true);
            await navigationService.NavigateAsync("RegisterInternPage", navParameters);
        }

        public async void BuscarInternCollectionAsync()
        {
            try
            {
                InternCollection = await internRepository.GetAllAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
