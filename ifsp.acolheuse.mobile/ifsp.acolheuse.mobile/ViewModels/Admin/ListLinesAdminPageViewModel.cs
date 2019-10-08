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
    public class ListLinesAdminPageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _novaLineCommand { get; set; }
        public DelegateCommand NovaLineCommand => _novaLineCommand ?? (_novaLineCommand = new DelegateCommand(NovaLineAsync));
        #endregion

        #region properties
        private IEnumerable<Line> linesCollection;
        public IEnumerable<Line> LinesCollection
        {
            get { return linesCollection; }
            set { linesCollection = value; RaisePropertyChanged(); }
        }

        #endregion

        private INavigationService navigationService;
        private ILineRepository lineRepository;

        public ListLinesAdminPageViewModel(INavigationService navigationService, ILineRepository lineRepository) :
            base(navigationService)
        {
            this.navigationService = navigationService;
            this.lineRepository = lineRepository;
            Title = "My View A";
        }

        internal async void ItemTapped(Line line)
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("line", line);
            await navigationService.NavigateAsync("RegisterLinePage", navParameters);
        }

        public async void NovaLineAsync()
        {
            await navigationService.NavigateAsync("RegisterLinePage");
        }

        public async void BuscarLinesCollectionAsync()
        {
            try
            {
                LinesCollection = await lineRepository.GetAllAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
