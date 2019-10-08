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
    public class ListPatientsAdminPageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _novoPatientCommand { get; set; }
        public DelegateCommand NovoPatientCommand => _novoPatientCommand ?? (_novoPatientCommand = new DelegateCommand(NovoPatientCommandAsync));
        #endregion

        #region properties
        private IEnumerable<Patient> patientCollection;
        public IEnumerable<Patient> PatientCollection
        {
            get { return patientCollection; }
            set { patientCollection = value; RaisePropertyChanged(); }
        }

        #endregion

        private INavigationService navigationService;
        private IPatientRepository patientRepository;

        public ListPatientsAdminPageViewModel(INavigationService navigationService, IPatientRepository patientRepository) :
            base(navigationService)
        {
            this.navigationService = navigationService;
            this.patientRepository = patientRepository;
            Title = "Usuários";
        }

        internal async void ItemTapped(Patient patient)
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("patient", patient);
            await navigationService.NavigateAsync("RegisterPatientPage", navParameters);
        }

        public async void NovoPatientCommandAsync()
        {
            await navigationService.NavigateAsync("RegisterPatientPage");
        }

        public async void BuscarPatientsCollectionAsync()
        {
            try
            {
                PatientCollection = await patientRepository.GetAllAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
