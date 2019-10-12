﻿using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using ifsp.acolheuse.mobile.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ifsp.acolheuse.mobile.ViewModels
{
    public class SelectionResponsibleInterconsultationtionPageViewModel : ViewModelBase
    {
        #region properties

        private ActionModel action;
        private Patient patient;
        private int consultationType;
        private IEnumerable<ActionModel> actionCollection;

        public ActionModel Action
        {
            get { return action; }
            set { action = value; RaisePropertyChanged(); }
        }
        public Patient Patient
        {
            get { return patient; }
            set { patient = value; RaisePropertyChanged(); }
        }
        public IEnumerable<ActionModel> ActionCollection
        {
            get { return actionCollection; }
            set { actionCollection = value; RaisePropertyChanged(); }
        }
        #endregion

        private INavigationService navigationService;
        private IPatientRepository patientRepository;
        private IActionRepository actionRepository;

        public SelectionResponsibleInterconsultationtionPageViewModel(INavigationService navigationService, IPatientRepository patientRepository, IActionRepository actionRepository) :
          base(navigationService)
        {
            this.navigationService = navigationService;
            this.patientRepository = patientRepository;
            this.actionRepository = actionRepository;

        }


        public async void EnviarInterconsultationtionAsync(ActionModel actionDestino)
        {
            if (actionDestino != null)
            {

                if (await MessageService.Instance.ShowAsyncYesNo("Deseja enviar este usuário a interconsulta da ação " + actionDestino.Name + "?"))
                {
                    PromoverInterconsultationtion(actionDestino);
                }
            }
        }
        public async void PromoverInterconsultationtion(ActionModel actionDestino)
        {
            if (Patient.ActionCollection.FirstOrDefault(x => x.Id == actionDestino.Id) == null)
            {
                Patient.ActionCollection.Add(new ListAppointment
                {
                    Id = actionDestino.Id,
                    Name = actionDestino.Name,
                    Added = true,
                    IsRelease = false,
                    IsAppointment = false,
                    IsListWaiting = false,
                    IsInterconsultationtion = true
                });

                await patientRepository.AddOrUpdateAsync(Patient, Patient.Id);
                await navigationService.GoBackAsync();
            }
        }

        public async void BuscarActionCollectionAsync()
        {
            IsBusy = true;
            ActionCollection = (await actionRepository.GetAllAsync()).Where( x=>x.Id != Action.Id);
            IsBusy = false;
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
        }
    }
}
