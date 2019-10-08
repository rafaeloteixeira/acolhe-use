using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ifsp.acolheuse.mobile.ViewModels.Acolhimento
{
    public class RegisterPatientPageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _savePatientCommand { get; set; }
        public DelegateCommand _editarListActionCommand { get; set; }
        public DelegateCommand SavePatientCommand => _savePatientCommand ?? (_savePatientCommand = new DelegateCommand(SavePatientAsync));
        public DelegateCommand EditarListActionCommand => _editarListActionCommand ?? (_editarListActionCommand = new DelegateCommand(EditarActionAsync));
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
        private IPatientRepository patientRepository;

        public RegisterPatientPageViewModel(INavigationService navigationService, IPatientRepository patientRepository) :
        base(navigationService)
        {
            this.navigationService = navigationService;
            this.patientRepository = patientRepository;
            Patient = new Patient();
            Title = "Usuário";
        }
    
        public async void SavePatientAsync()
        {
            await patientRepository.AddOrUpdateAsync(Patient, Patient.Id);
            await navigationService.GoBackAsync();
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

        public async void ExcluirPatientAsync(string id)
        {
            await patientRepository.RemoveAsync(id);
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
        }
    }
}

