using ifsp.acolheuse.mobile.Core.Repositories;
using Prism.Navigation;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Services;
using System.Threading.Tasks;
using Prism.Services;

namespace ifsp.acolheuse.mobile.ViewModels.Administrador
{
    public class RegisterActionPageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _editarResponsibleCommand { get; set; }
        public DelegateCommand _editarInternsCommand { get; set; }
        public DelegateCommand _saveActionCommand { get; set; }
        public DelegateCommand _configurarDayCommand { get; set; }

        public DelegateCommand EditarResponsibleCommand => _editarResponsibleCommand ?? (_editarResponsibleCommand = new DelegateCommand(EditarListResponsibleAsync));
        public DelegateCommand EditarInternsCommand => _editarInternsCommand ?? (_editarInternsCommand = new DelegateCommand(EditarListInternsAsync));
        public DelegateCommand SaveActionCommand => _saveActionCommand ?? (_saveActionCommand = new DelegateCommand(SaveActionAsync));
        public DelegateCommand ConfigurarDayCommand => _configurarDayCommand ?? (_configurarDayCommand = new DelegateCommand(ConfigurarDayAsync));
        #endregion

        #region properties
        private ActionModel action;
        private IEnumerable<Line> linesCollection;
        private string dia;
        private int tamanhoLvResponsible;
        private int tamanhoLvInterns;
        private Line line;

        public int TamanhoLvResponsible
        {
            get { return tamanhoLvResponsible; }
            set { tamanhoLvResponsible = value; RaisePropertyChanged(); }
        }
        public int TamanhoLvInterns
        {
            get { return tamanhoLvInterns; }
            set { tamanhoLvInterns = value; RaisePropertyChanged(); }
        }
        public ActionModel Action
        {
            get { return action; }
            set { action = value; RaisePropertyChanged(); }
        }
        public IEnumerable<Line> LinesCollection
        {
            get { return linesCollection; }
            set { linesCollection = value; RaisePropertyChanged(); }
        }
        public string Day
        {
            get { return dia; }
            set { dia = value; RaisePropertyChanged(); }
        }
        public List<string> DaysCollection
        {
            get { return new List<string> { "Segunda-feira", "Terça-feira", "Quarta-feira", "Quinta-feira", "Sexta-feira" }; }
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
        private IPageDialogService dialogService;

        public RegisterActionPageViewModel(INavigationService navigationService, IActionRepository actionRepository, ILineRepository lineRepository, IPageDialogService dialogService) :
           base(navigationService)
        {
            this.navigationService = navigationService;
            this.actionRepository = actionRepository;
            this.lineRepository = lineRepository;
            this.dialogService = dialogService;
            Action = new ActionModel();
            Line = new Line();
            Title = "Register de Ação";
        }

        public async void EditarListResponsibleAsync()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("action", action);
            await navigationService.NavigateAsync("EditListResponsiblePage", navParameters);
        }
        public async void EditarListInternsAsync()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("action", action);
            await navigationService.NavigateAsync("EditListInternsPage", navParameters);
        }
        public async void ConfigurarDayAsync()
        {

            if (String.IsNullOrEmpty(Action.Id))
            {
                var action = await dialogService.DisplayActionSheetAsync("Para configurar é necessário save a ação.", "Cancelar", null, "Save");

                if (action == "Cancelar")
                    return;

                Action.GuidAction = Guid.NewGuid().ToString();
                await actionRepository.AddAsync(Action);
                Action = await actionRepository.GetByGuidAsync(Action.GuidAction.ToString());
            }

            var navParameters = new NavigationParameters();
            navParameters.Add("action", Action);
            navParameters.Add("dia", Day);
            await navigationService.NavigateAsync("ScheduleActionPage", navParameters);
        }

        public async void SaveActionAsync()
        {
            if(String.IsNullOrEmpty(Action.GuidAction))
                Action.GuidAction = Guid.NewGuid().ToString();

            this.Action.IdLine = Line.Id;
            await actionRepository.AddOrUpdateAsync(Action, Action.Id);
            await navigationService.GoBackAsync();
        }

        public async void CarregarLineAction(string IdAction)
        {
            try
            {
                Action = await actionRepository.GetAsync(IdAction);
                Line = await lineRepository.GetAsync(Action.IdLine);
                LinesCollection = await lineRepository.GetAllAsync();
                Day = DaysCollection[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async void CarregarLine()
        {
            LinesCollection = await lineRepository.GetAllAsync();
            Day = DaysCollection[0];
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            var navigationMode = parameters.GetNavigationMode();
            if(navigationMode != NavigationMode.Back)
            {
                if (parameters["line"] != null)
                {
                    Line = parameters["line"] as Line;
                }
                if (parameters["action"] != null)
                {
                    Action = parameters["action"] as ActionModel;
                    CarregarLineAction(Action.Id);
                }
                else
                {
                    CarregarLine();
                }
            }
        }
    }
}
