using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ifsp.acolheuse.mobile.ViewModels.Acolhimento
{
    public class SelectionActionPageViewModel : ViewModelBase
    {

        #region commands
        public DelegateCommand _saveCommand { get; set; }
        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(SaveAsync));
        #endregion

        #region properties
        private Patient patient;
        private ObservableCollection<ListAppointment> actionCollection;

        public Patient Patient
        {
            get { return patient; }
            set { patient = value; RaisePropertyChanged(); }
        }
        public ObservableCollection<ListAppointment> ActionCollection
        {
            get { return actionCollection; }
            set { actionCollection = value; RaisePropertyChanged(); }
        }
        #endregion

        private INavigationService navigationService;
        private IPatientRepository patientRepository;
        private IActionRepository actionRepository;

        public SelectionActionPageViewModel(INavigationService navigationService, IPatientRepository patientRepository, IActionRepository actionRepository) :
        base(navigationService)
        {
            this.navigationService = navigationService;
            this.patientRepository = patientRepository;
            this.actionRepository = actionRepository;
        }

        public async void SaveAsync()
        {
            Patient.ActionCollection = new ObservableCollection<ListAppointment>(ActionCollection.Where(x => x.Added == true));
            await navigationService.GoBackAsync();
        }

        public async void BuscarActionCollectionAsync()
        {
            ActionCollection = new ObservableCollection<ListAppointment>();
            var action = await actionRepository.GetAllAsync();

            for (int i = 0; i < action.Count(); i++)
            {
                if (Patient.ActionCollection?.FirstOrDefault(x => x.Id == action.ElementAt(i).Id) != null)
                {
                    ActionCollection.Add(new ListAppointment
                    {
                        Id = action.ElementAt(i).Id,
                        Name = action.ElementAt(i).Name,
                        Added = true,
                        IsRelease = false,
                        IsAppointment = false,
                        IsInterconsultationtion = false,
                        IsListWaiting = true
                    });
                }
                else
                {
                    ActionCollection.Add(new ListAppointment
                    {
                        Id = action.ElementAt(i).Id,
                        Name = action.ElementAt(i).Name,
                        Added = false,
                        IsRelease = false,
                        IsAppointment = false,
                        IsInterconsultationtion = false,
                        IsListWaiting = true
                    });
                }
            }
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters["patient"] != null)
            {
                Patient = parameters["patient"] as Patient;
            }
            else
            {
                Patient = new Patient();
            }
        }
    }
    
}
