using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using ifsp.acolheuse.mobile.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ifsp.acolheuse.mobile.ViewModels
{
    public class AppointmentPageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _interconsultationtionCommand { get; set; }
        public DelegateCommand _schedulerOrientactionCommand { get; set; }
        public DelegateCommand _schedulerIndividualCommand { get; set; }
        public DelegateCommand _schedulerGrupoCommand { get; set; }
        public DelegateCommand _darReleaseCommand { get; set; }

        public DelegateCommand InterconsultationtionCommand => _interconsultationtionCommand ?? (_interconsultationtionCommand = new DelegateCommand(EnviarInterconsultationtionAsync));
        public DelegateCommand SchedulerOrientactionCommand => _schedulerOrientactionCommand ?? (_schedulerOrientactionCommand = new DelegateCommand(SchedulerOrientactionAsync));
        public DelegateCommand SchedulerIndividualCommand => _schedulerIndividualCommand ?? (_schedulerIndividualCommand = new DelegateCommand(SchedulerIndividualAsync));
        public DelegateCommand SchedulerGrupoCommand => _schedulerGrupoCommand ?? (_schedulerGrupoCommand = new DelegateCommand(SchedulerGrupoAsync));
        public DelegateCommand DarReleaseCommand => _darReleaseCommand ?? (_darReleaseCommand = new DelegateCommand(DarReleaseAsync));
        #endregion

        #region properties
        private Patient patient;
        private ActionModel action;
        private DateTime dataHora;
        private bool isRepetir;
        private bool orientactionVisible;
        private bool individualVisible;
        private bool grupoVisible;

        public Patient Patient
        {
            get { return patient; }
            set { patient = value; RaisePropertyChanged(); }
        }
        public ActionModel Action
        {
            get { return action; }
            set { action = value; RaisePropertyChanged(); }
        }
        public DateTime DataHora
        {
            get { return dataHora; }
            set { dataHora = value; RaisePropertyChanged(); }
        }
        public bool IsRepetir
        {
            get { return isRepetir; }
            set { isRepetir = value; RaisePropertyChanged(); }
        }
        public bool OrientactionVisible
        {
            get { return orientactionVisible; }
            set { orientactionVisible = value; RaisePropertyChanged(); }
        }
        public bool IndividualVisible
        {
            get { return individualVisible; }
            set { individualVisible = value; RaisePropertyChanged(); }
        }
        public bool GrupoVisible
        {
            get { return grupoVisible; }
            set { grupoVisible = value; RaisePropertyChanged(); }
        }

        #endregion

        private INavigationService navigationService;
        private IPatientRepository patientRepository;
        public AppointmentPageViewModel(INavigationService navigationService, IPatientRepository patientRepository) :
          base(navigationService)
        {
            this.navigationService = navigationService;
            this.patientRepository = patientRepository;
        }

        public async void SchedulerOrientactionAsync()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("patient", Patient);
            navParameters.Add("action", Action);
            navParameters.Add("type_consultation", Appointment._ORIENTACAO);
            await navigationService.NavigateAsync("SelectionInternsAppointmentPage", navParameters); //resp
        }
        public async void SchedulerIndividualAsync()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("patient", Patient);
            navParameters.Add("action", Action);
            navParameters.Add("type_consultation", Appointment._INDIVIDUAL);
            await navigationService.NavigateAsync("SelectionInternsAppointmentPage", navParameters); //resp
        }
        public async void SchedulerGrupoAsync()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("patient", Patient);
            navParameters.Add("action", Action);
            navParameters.Add("type_consultation", Appointment._GRUPO);
            await navigationService.NavigateAsync("SelectionInternsAppointmentPage", navParameters); //resp
        }
        public async void EnviarInterconsultationtionAsync()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("patient", Patient);
            navParameters.Add("action", Action);
            await navigationService.NavigateAsync("SelectionResponsibleInterconsultationtionPage", navParameters); //resp
        }

        public async void DarReleaseAsync()
        {

            if (await MessageService.Instance.ShowAsyncYesNo("Deseja dar alta ao usuário?"))
            {
                var action = Patient.ActionCollection.FirstOrDefault(x => x.Id == Action.Id);
                action.IsRelease = true;
                action.IsInterconsultationtion = false;
                action.IsAppointment = false;
                action.IsListWaiting = false;

                await patientRepository.AddOrUpdateAsync(Patient, Patient.Id);
                await navigationService.GoBackAsync();
            }
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters["patient"] != null)
            {
                Patient = parameters["patient"] as Patient;
            }
            if (parameters["action"] != null)
            {
                Action = parameters["action"] as ActionModel;
            }

            if (Action.IsOrientation)
                OrientactionVisible = true;

            if (Action.IsIndividual)
                IndividualVisible = true;

            if (Action.IsGroup)
                GrupoVisible = true;
        }
    }
}
