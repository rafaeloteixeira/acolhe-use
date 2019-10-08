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
    public class ListPatientsResponsiblePageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _novoPatientCommand { get; set; }
        public DelegateCommand NovoPatientCommand => _novoPatientCommand ?? (_novoPatientCommand = new DelegateCommand(NovoPatientCommandAsync));
        #endregion

        #region properties
        private IEnumerable<Patient> patientsCollection;
        public IEnumerable<Patient> PatientsCollection
        {
            get { return patientsCollection; }
            set { patientsCollection = value; RaisePropertyChanged(); }
        }

        internal async void NavigateToPatientResponsible(Patient patient)
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("patient", patient);
            await navigationService.NavigateAsync("PatientResponsiblePage", navParameters);
        }
        #endregion

        private INavigationService navigationService;
        private IResponsibleRepository responsibleRepository;
        private IActionRepository actionRepository;
        private IPatientRepository patientRepository;
        public ListPatientsResponsiblePageViewModel(INavigationService navigationService, IResponsibleRepository responsibleRepository, IActionRepository actionRepository, IPatientRepository patientRepository) :
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
            IEnumerable<ActionModel> actionesAtendidas = await actionRepository.GetAllByResponsibleId(Settings.UserId);

            //BUSCA OS USUÁRIOS ATENDIDOS PELAS AÇÕES DO SERVIDOR
            PatientsCollection = (await patientRepository.GetAllAsync()).Where(p => p.ActionCollection != null && p.ActionCollection.Any(c => actionesAtendidas.Any(c2 => c2.Id == c.Id)));
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
        }
    }
}
