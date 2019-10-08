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
    public class ListPatientsReleaseResponsiblePageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _novoPatientCommand { get; set; }
        public DelegateCommand NovoPatientCommand => _novoPatientCommand ?? (_novoPatientCommand = new DelegateCommand(NovoPatientCommandAsync));
        #endregion

        #region properties
        private IEnumerable<Patient> patientsCollection;
        private ActionModel action;
        public IEnumerable<Patient> PatientsCollection
        {
            get { return patientsCollection; }
            set { patientsCollection = value; RaisePropertyChanged(); }
        }
        public ActionModel Action
        {
            get { return action; }
            set { action = value; RaisePropertyChanged(); }
        }

        #endregion

        private INavigationService navigationService;
        private IResponsibleRepository responsibleRepository;
        private IActionRepository actionRepository;
        private IPatientRepository patientRepository;
        public ListPatientsReleaseResponsiblePageViewModel(INavigationService navigationService, IResponsibleRepository responsibleRepository, IActionRepository actionRepository, IPatientRepository patientRepository) :
          base(navigationService)
        {
            this.navigationService = navigationService;
            this.responsibleRepository = responsibleRepository;
            this.actionRepository = actionRepository;
            this.patientRepository = patientRepository;
        }

        public async void NovoPatientCommandAsync()
        {
            await navigationService.NavigateAsync("RegisterPatientPage");
        }

        public async void BuscarPatientsCollectionAsync()
        {
            //BUSCA AS AÇÕES ATENDIDAS POR ESSE SERVIDOR
            IEnumerable<ActionModel> actionesAtendidas = (await actionRepository.GetAllAsync()).Where(x => x.ResponsibleCollection.FirstOrDefault(m => m.Id == Settings.UserId) != null);

            //BUSCA OS USUÁRIOS ATENDIDOS PELAS AÇÕES DO SERVIDOR
            PatientsCollection = (await patientRepository.GetAllAsync()).Where(p => p.ActionCollection.Any(c => actionesAtendidas.Any(c2 => c2.Id == c.Id) && c.IsRelease == true));
        }

        public async void PromoverListWaiting(Patient Patient)
        {
            var action = Patient.ActionCollection.FirstOrDefault(x => x.Id == Action.Id);
            action.IsRelease = false;
            action.IsAppointment = false;
            action.IsInterconsultationtion = false;
            action.IsListWaiting = true;

            await patientRepository.AddOrUpdateAsync(Patient, Patient.Id);
            await navigationService.GoBackAsync();
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters["action"] != null)
            {
                Action = parameters["action"] as ActionModel;
            }
            BuscarPatientsCollectionAsync();
        }
    }
}
