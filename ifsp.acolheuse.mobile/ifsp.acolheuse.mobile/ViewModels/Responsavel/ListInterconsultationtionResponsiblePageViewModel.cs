using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using ifsp.acolheuse.mobile.Core.Settings;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ifsp.acolheuse.mobile.ViewModels
{
    public class ListInterconsultationtionResponsiblePageViewModel : ViewModelBase
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
        private IActionRepository actionRepository;
        private IPatientRepository patientRepository;
        private IPageDialogService dialogService;
        private IMessageRepository messageRepository;
        private IResponsibleRepository responsibleRepository;
        public ListInterconsultationtionResponsiblePageViewModel(INavigationService navigationService, IActionRepository actionRepository, IPatientRepository patientRepository, IPageDialogService dialogService, IMessageRepository messageRepository, IResponsibleRepository responsibleRepository) :
          base(navigationService)
        {
            this.navigationService = navigationService;
            this.actionRepository = actionRepository;
            this.patientRepository = patientRepository;
            this.dialogService = dialogService;
            this.messageRepository = messageRepository;
            this.responsibleRepository = responsibleRepository;
        }

        public async void NovoPatientCommandAsync()
        {
            await navigationService.NavigateAsync("RegisterPatientPage");
        }

        public async void BuscarPatientsCollectionAsync()
        {
            IsBusy = true;
            //BUSCA AS AÇÕES ATENDIDAS POR ESSE SERVIDOR
            IEnumerable<ActionModel> actionesAtendidas = (await actionRepository.GetAllAsync()).Where(x => x.ResponsibleCollection.FirstOrDefault(m => m.Id == Settings.UserId) != null);

            //BUSCA OS USUÁRIOS ATENDIDOS PELAS AÇÕES DO SERVIDOR
            PatientsCollection = (await patientRepository.GetAllAsync()).Where(p => p.ActionCollection.Any(c => actionesAtendidas.Any(c2 => c2.Id == c.Id) && c.IsInterconsultationtion == true));
            IsBusy = false;
        }
        public async void PromoverAppointment(Patient Patient)
        {
            var result = await dialogService.DisplayActionSheetAsync("Selecione a operação desejada para o pedido de interconsulta", "Cancelar", null, "Aceitar", "Negar");
            var action = Patient.ActionCollection.FirstOrDefault(x => x.Id == Action.Id);
            switch (result)
            {
                case "Aceitar":

                    action.IsRelease = false;
                    action.IsAppointment = true;
                    action.IsListWaiting = false;
                    action.IsInterconsultationtion = false;

                    await patientRepository.AddOrUpdateAsync(Patient, Patient.Id);
                    await navigationService.GoBackAsync();
                    return;
                case "Negar":
                    Responsible responsible = await responsibleRepository.GetAsync(Settings.UserId);
                    Message message = new Message
                    {
                        Date = DateTime.Now,
                        IdAction = Action.Id,
                        IdFrom = Settings.UserId,
                        NameFrom = responsible.NameCompleto,
                        IdTo = action.ProfessorIdFrom,
                        Body = "Recusou o pedido de interconsulta para o paciente " + Patient.NameCompleto + ", para a ação " + Action.Name
                    };
                    await messageRepository.AddAsync(message);

                    Patient.ActionCollection.Remove(action);
                    await patientRepository.AddOrUpdateAsync(Patient, Patient.Id);
                    await navigationService.GoBackAsync();
                    break;
            }
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
