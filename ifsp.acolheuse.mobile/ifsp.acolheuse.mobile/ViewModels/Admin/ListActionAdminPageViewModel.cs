using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using ifsp.acolheuse.mobile.Core.Repositories;
using Prism.Navigation;
using ifsp.acolheuse.mobile.Core.Domain;
using System.Collections.ObjectModel;

namespace ifsp.acolheuse.mobile.ViewModels.Administrador
{
    public class ListActionAdminPageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _novaActionCommand { get; set; }

        public DelegateCommand NovaActionCommand => _novaActionCommand ?? (_novaActionCommand = new DelegateCommand(NovaActionAsync));
        #endregion

        #region properties
        private IEnumerable<ActionModel> actionCollection;
        private IEnumerable<Line> linesCollection;
        private Line line;

        public IEnumerable<ActionModel> ActionCollection
        {
            get { return actionCollection; }
            set { actionCollection = value; RaisePropertyChanged(); }
        }
        public IEnumerable<Line> LinesCollection
        {
            get { return linesCollection; }
            set { linesCollection = value; RaisePropertyChanged(); }
        }
        public Line Line
        {
            get { return line; }
            set { line = value; RaisePropertyChanged(); }
        }
        #endregion

        private INavigationService navigationService;
        private ILineRepository lineRepository;
        private IActionRepository actionRepository;

        public ListActionAdminPageViewModel(INavigationService navigationService, ILineRepository lineRepository, IActionRepository actionRepository) :
            base(navigationService)
        {
            this.navigationService = navigationService;
            this.lineRepository = lineRepository;
            this.actionRepository = actionRepository;
            Title = "My View A";
        }
        public async void NovaActionAsync()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("line", Line);
            await navigationService.NavigateAsync("RegisterActionPage", navParameters);
        }
        internal async void ItemTapped(ActionModel action)
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("action", action);
            await navigationService.NavigateAsync("RegisterActionPage", navParameters);
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
        public async void BuscarActionCollectionAsync()
        {
            ActionCollection = new ObservableCollection<ActionModel>(await actionRepository.GetAllByIdLineAsync(Line.Id));
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
        }
    }
}
