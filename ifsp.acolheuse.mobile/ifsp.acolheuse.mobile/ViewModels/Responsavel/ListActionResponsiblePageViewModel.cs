using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using ifsp.acolheuse.mobile.Core.Settings;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ifsp.acolheuse.mobile.ViewModels
{
    public class ListActionResponsiblePageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _novaActionCommand { get; set; }
        public DelegateCommand NovaActionCommand => _novaActionCommand ?? (_novaActionCommand = new DelegateCommand(NovaActionAsync));
        #endregion

        #region properties
        private IEnumerable<ActionModel> actionCollection;
        private ObservableCollection<Line> linesCollection;
        private Line line;

        public IEnumerable<ActionModel> ActionCollection
        {
            get { return actionCollection; }
            set { actionCollection = value; RaisePropertyChanged(); }
        }
        public ObservableCollection<Line> LinesCollection
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



        private IActionRepository actionRepository;
        private ILineRepository lineRepository;
        public ListActionResponsiblePageViewModel(INavigationService navigationService, IActionRepository actionRepository, ILineRepository lineRepository) :
          base(navigationService)
        {
            this.navigationService = navigationService;
            this.actionRepository = actionRepository;
            this.lineRepository = lineRepository;
            Line = new Line();
        }

        internal async void NavigateToAction(ActionModel action)
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("action", action);
            await navigationService.NavigateAsync("ActionResponsiblePage", navParameters);
        }
        public async void NovaActionAsync()
        {
            await navigationService.NavigateAsync("RegisterActionPage");
        }

        private async void GetOnNavited()
        {
            IsBusy = true;
            LinesCollection = new ObservableCollection<Line>(await GetLinesCollectionAsync());
            IsBusy = false;
        }

        private async Task<ConcurrentBag<Line>> GetLinesCollectionAsync()
        {
            ConcurrentBag<Line> lines = new ConcurrentBag<Line>();
            ObservableCollection<ActionModel> action = new ObservableCollection<ActionModel>((await actionRepository.GetAllAsync()).Where(x => x.ResponsibleCollection != null && x.ResponsibleCollection.FirstOrDefault(m => m.Id == Settings.UserId) != null));

            for (int i = 0; i < action.Count(); i++)
            {
                Line lineResponsible = lines.FirstOrDefault(x => x.Id == action[i].IdLine);

                if (lineResponsible == null)
                {
                    lineResponsible = await lineRepository.GetAsync(action[i].IdLine);
                    lines.Add(lineResponsible);
                }
            }

            return lines;
        }

        internal async void BuscarActionCollectionAsync()
        {
            IsBusy = true;
            ActionCollection = await actionRepository.GetAllByIdLineAsync(Line.Id);
            IsBusy = false;
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            var navigationMode = parameters.GetNavigationMode();
            if (navigationMode != NavigationMode.Back)
            {
                GetOnNavited();
            }
        }
    }
}
