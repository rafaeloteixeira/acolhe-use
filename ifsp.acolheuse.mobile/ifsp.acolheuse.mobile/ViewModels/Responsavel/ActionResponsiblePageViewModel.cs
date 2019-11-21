using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using ifsp.acolheuse.mobile.Core.Domain;
using System.Collections.ObjectModel;
using Prism.Navigation;
using ifsp.acolheuse.mobile.Core.Repositories;

namespace ifsp.acolheuse.mobile.ViewModels
{
    public class ActionResponsiblePageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _editInternsCommand { get; set; }
        public DelegateCommand _saveActionCommand { get; set; }
        public DelegateCommand _listInterconsultationtionCommand { get; set; }
        public DelegateCommand _listAppointmentCommand { get; set; }
        public DelegateCommand _listWaitingCommand { get; set; }
        public DelegateCommand _listrPatientsReleaseCommand { get; set; }


        public DelegateCommand EditInternsCommand => _editInternsCommand ?? (_editInternsCommand = new DelegateCommand(EditListInternsAsync));
        public DelegateCommand SaveActionCommand => _saveActionCommand ?? (_saveActionCommand = new DelegateCommand(SaveActionAsync));
        public DelegateCommand ListInterconsultationtionCommand => _listInterconsultationtionCommand ?? (_listInterconsultationtionCommand = new DelegateCommand(ListInterconsultationtionAsync));
        public DelegateCommand ListAppointmentCommand => _listAppointmentCommand ?? (_listAppointmentCommand = new DelegateCommand(ListAppointmentAsync));
        public DelegateCommand ListWaitingCommand => _listWaitingCommand ?? (_listWaitingCommand = new DelegateCommand(ListWaitingAsync));
        public DelegateCommand ListrPatientsReleaseCommand => _listrPatientsReleaseCommand ?? (_listrPatientsReleaseCommand = new DelegateCommand(ListReleaseAsync));
        #endregion

        #region properties
        private Line line;
        private ActionModel action;
        private int numberOfPatients;
        private IEnumerable<Line> linesCollection;
        public Line Line
        {
            get { return line; }
            set { line = value; RaisePropertyChanged(); }
        }
        public ActionModel Action
        {
            get { return action; }
            set { action = value; RaisePropertyChanged(); }
        }
        public int NumberOfPatients
        {
            get { return numberOfPatients; }
            set { numberOfPatients = value; RaisePropertyChanged(); }
        }
        public IEnumerable<Line> LinesCollection
        {
            get { return linesCollection; }
            private set { linesCollection = value; RaisePropertyChanged(); }
        }
        #endregion

        private INavigationService navigationService;
        private ILineRepository lineRepository;
        private IActionRepository actionRepository;
        private IPatientRepository patientRepository;

        public ActionResponsiblePageViewModel(INavigationService navigationService, ILineRepository lineRepository, IActionRepository actionRepository, IPatientRepository patientRepository) :
          base(navigationService)
        {
            this.navigationService = navigationService;
            this.lineRepository = lineRepository;
            this.actionRepository = actionRepository;
            this.patientRepository = patientRepository;

            Action = new ActionModel();
        }


        public async void EditListInternsAsync()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("action", Action);
            await navigationService.NavigateAsync("ListInternsResponsiblePage", navParameters); //adm
        }
        public async void ListAppointmentAsync()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("action", Action);
            await navigationService.NavigateAsync("ListAppointmentResponsiblePage", navParameters); //resp
        }
        public async void ListInterconsultationtionAsync()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("action", Action);
            await navigationService.NavigateAsync("ListInterconsultationtionResponsiblePage", navParameters); //resp
        }
        public async void ListWaitingAsync()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("action", Action);
            await navigationService.NavigateAsync("ListWaitingResponsiblePage", navParameters); //resp
        }
        public async void ListReleaseAsync()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("action", Action);
            await navigationService.NavigateAsync("ListPatientsReleaseResponsiblePage", navParameters); //resp
        }
        public async void SaveActionAsync()
        {
            //await actionRepository.AddOrUpdateAsync(Action, Action.Id);
            await navigationService.GoBackAsync();
        }

        public async void GetLinesAsync()
        {
            LinesCollection = await lineRepository.GetAllAsync();
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters["action"] != null)
            {
                GetActionData(parameters["action"] as ActionModel);
            }
        }
        private async void GetActionData(ActionModel action)
        {
            IsBusy = true;

            Action = action;
            NumberOfPatients = (await patientRepository.GetAllByActionIdAsync(Action.Id)).Count();
            Line = await lineRepository.GetAsync(Action.IdLine);

            IsBusy = false;
        }
    }
}
