using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using ifsp.acolheuse.mobile.Core.Settings;
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
        private IMessageRepository messageRepository;
        private IResponsibleRepository responsibleRepository;

        public SelectionResponsibleInterconsultationtionPageViewModel(INavigationService navigationService, IPatientRepository patientRepository, IActionRepository actionRepository, IMessageRepository messageRepository, IResponsibleRepository responsibleRepository) :
          base(navigationService)
        {
            this.navigationService = navigationService;
            this.patientRepository = patientRepository;
            this.actionRepository = actionRepository;
            this.messageRepository = messageRepository;
            this.responsibleRepository = responsibleRepository;

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
                    ProfessorIdFrom = Settings.UserId,
                    Name = actionDestino.Name,
                    Added = true,
                    IsRelease = false,
                    IsAppointment = false,
                    IsListWaiting = false,
                    IsInterconsultationtion = true
                });
                Responsible responsible = await responsibleRepository.GetAsync(Settings.UserId);

                Message message = new Message
                {
                    Date = DateTime.Now,
                    IdAction = Action.Id,
                    IdFrom = Settings.UserId,
                    NameFrom = responsible.NameCompleto,
                    Body = "Enviou um pedido de interconsulta para o paciente " + Patient.NameCompleto + ", para a ação " + Action.Name
                };
                await messageRepository.AddAsync(message);
                await patientRepository.AddOrUpdateAsync(Patient, Patient.Id);
                await navigationService.GoBackAsync();
            }
            else
            {
                await MessageService.Instance.ShowAsync("Este usuário já está na ação " + actionDestino.Name);
            }
        }

        public async void BuscarActionCollectionAsync()
        {
            IsBusy = true;
            ActionCollection = (await actionRepository.GetAllAsync()).Where(x => x.Id != Action.Id);
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
