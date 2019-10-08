using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ifsp.acolheuse.mobile.ViewModels
{
    public class PatientResponsiblePageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _savePatientCommand { get; set; }
        public DelegateCommand SavePatientCommand => _savePatientCommand ?? (_savePatientCommand = new DelegateCommand(SavePatientAsync));
        public DelegateCommand _editarListAction { get; set; }
        public DelegateCommand EditarListAction => _editarListAction ?? (_editarListAction = new DelegateCommand(EditarActionAsync));
        #endregion

        #region properties
        private Patient patient;

        public Patient Patient
        {
            get { return patient; }
            set { patient = value; RaisePropertyChanged(); }
        }
        #endregion

        private INavigationService navigationService;
        private IResponsibleRepository responsibleRepository;
        private IPatientRepository patientRepository;
        public PatientResponsiblePageViewModel(INavigationService navigationService, IResponsibleRepository responsibleRepository, IPatientRepository patientRepository) :
          base(navigationService)
        {
            this.navigationService = navigationService;
            this.responsibleRepository = responsibleRepository;
            this.patientRepository = patientRepository;
        }

        public async void SavePatientAsync()
        {
            await patientRepository.AddOrUpdateAsync(Patient, Patient.Id);
            GetPatientAsync();
        }
        public async void EditarActionAsync()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("patient", Patient);
            await navigationService.NavigateAsync("SelectionActionPage", navParameters);
        }

        public async void GetPatientAsync()
        {
            Patient = await patientRepository.GetAsync(Patient.Id);
        }

        public async void ExcluirPatientAsync(string Key)
        {
            await patientRepository.RemoveAsync(Key);
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters["patient"] != null)
            {
                Patient = parameters["patient"] as Patient;
                GetPatientAsync();
            }
            else
            {
                Patient = new Patient();
            }
        }
    }
}
