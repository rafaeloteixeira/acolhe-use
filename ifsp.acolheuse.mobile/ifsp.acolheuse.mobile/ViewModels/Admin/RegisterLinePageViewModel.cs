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
    public class RegisterLinePageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _saveLineCommand { get; set; }
        public DelegateCommand _editarListAction { get; set; }
        public DelegateCommand SaveLineCommand => _saveLineCommand ?? (_saveLineCommand = new DelegateCommand(SaveAsync));
        public DelegateCommand EditarListAction => _editarListAction ?? (_editarListAction = new DelegateCommand(SaveAsync));

        #endregion

        #region properties
        private IEnumerable<ActionModel> actionCollection;
        private Line line;
        public IEnumerable<ActionModel> ActionCollection
        {
            get { return actionCollection; }
            set { actionCollection = value; RaisePropertyChanged(); }
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
        private string titlePage = "Line de cuidado";

        public RegisterLinePageViewModel(INavigationService navigationService, ILineRepository lineRepository, IActionRepository actionRepository)
        : base(navigationService)
        {
            this.navigationService = navigationService;
            this.lineRepository = lineRepository;
            this.actionRepository = actionRepository;
            Line = new Line();
            Title = titlePage;

        }
        internal async void OpenRegisterAction(ActionModel action)
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("action", action);
            await navigationService.NavigateAsync("RegisterActionPage", navParameters);
        }

        public async void SaveAsync()
        {
            await lineRepository.AddOrUpdateAsync(Line, Line.Id);
            await navigationService.GoBackAsync();
        }

        public async void GetLineAsync()
        {
            if (!String.IsNullOrEmpty(Line.Id))
            {
                Line = await lineRepository.GetAsync(Line.Id);
                GetActionAsync();
            }
        }

        private async void GetActionAsync()
        {
            if (Line != null && !String.IsNullOrEmpty(Line.Id))
            {
                ActionCollection = new System.Collections.ObjectModel.ObservableCollection<ActionModel>(await actionRepository.GetAllByIdLineAsync(Line.Id));
            }
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters["line"] != null)
            {
                Line = parameters["line"] as Line;
            }
        }
    }
}
