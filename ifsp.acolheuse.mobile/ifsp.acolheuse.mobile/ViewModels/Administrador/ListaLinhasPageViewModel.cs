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
    public class ListaLinhasPageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _novaLinhaCommand { get; set; }
        public DelegateCommand NovaLinhaCommand => _novaLinhaCommand ?? (_novaLinhaCommand = new DelegateCommand(NovaLinhaAsync));
        #endregion

        #region properties
        private IEnumerable<Linha> linhasCollection;
        public IEnumerable<Linha> LinhasCollection
        {
            get { return linhasCollection; }
            set { linhasCollection = value; RaisePropertyChanged(); }
        }

        #endregion

        private INavigationService navigationService;
        private ILinhaRepository linhaRepository;

        public ListaLinhasPageViewModel(INavigationService navigationService, ILinhaRepository linhaRepository) :
            base(navigationService)
        {
            this.navigationService = navigationService;
            this.linhaRepository = linhaRepository;
            Title = "My View A";
        }

        internal async void ItemTapped(Linha linha)
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("linha", linha);
            await navigationService.NavigateAsync("CadastroLinhaCuidadoPage", navParameters);
        }

        public async void NovaLinhaAsync()
        {
            await navigationService.NavigateAsync("CadastroLinhaCuidadoPage");
        }

        public async void BuscarLinhasCollectionAsync()
        {
            try
            {
                LinhasCollection = await linhaRepository.GetAllAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
