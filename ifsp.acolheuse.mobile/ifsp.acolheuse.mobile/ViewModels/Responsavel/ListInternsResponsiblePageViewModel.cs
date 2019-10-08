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

namespace ifsp.acolheuse.mobile.ViewModels
{
    public class ListInternsResponsiblePageViewModel : ViewModelBase
    {

        #region properties
        private IEnumerable<Intern> internCollection;
        public IEnumerable<Intern> InternCollection
        {
            get { return internCollection; }
            set { internCollection = value; RaisePropertyChanged(); }
        }

        internal async void NavigateToInternResponsible(Intern intern)
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("intern", intern);
            await navigationService.NavigateAsync("InternResponsiblePage", navParameters);
        }
        #endregion

        private INavigationService navigationService;
        private IInternRepository internRepository;

        public ListInternsResponsiblePageViewModel(INavigationService navigationService, IInternRepository internRepository) :
          base(navigationService)
        {
            this.navigationService = navigationService;
            this.internRepository = internRepository;
            BuscarInternCollectionAsync(Settings.UserId);
        }

        private async void BuscarInternCollectionAsync(string KeyResponsible)
        {
            InternCollection = await internRepository.GetInternsByResponsibleIdAsync(KeyResponsible);
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
        }
    }
}
