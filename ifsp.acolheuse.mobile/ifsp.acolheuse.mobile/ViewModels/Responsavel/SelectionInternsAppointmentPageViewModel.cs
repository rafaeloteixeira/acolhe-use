using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ifsp.acolheuse.mobile.ViewModels
{
    public class SelectionInternsAppointmentPageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _schedulerCommand { get; set; }
        public DelegateCommand SchedulerCommand => _schedulerCommand ?? (_schedulerCommand = new DelegateCommand(SchedulerAsync));
        #endregion

        #region properties
        private ObservableCollection<ListEntity> internCollection;
        private ActionModel action;
        private Patient patient;
        private int consultationType;

        public ObservableCollection<ListEntity> InternCollection
        {
            get { return internCollection; }
            set { internCollection = value; RaisePropertyChanged(); }
        }
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
        public int ConsultationType
        {
            get { return consultationType; }
            set { consultationType = value; RaisePropertyChanged(); }
        }
        #endregion

        private INavigationService navigationService;
        private IInternRepository internRepository;
        public SelectionInternsAppointmentPageViewModel(INavigationService navigationService, IInternRepository internRepository) :
          base(navigationService)
        {
            this.navigationService = navigationService;
            this.internRepository = internRepository;
        }


        public async void SchedulerAsync()
        {
            var navParameters = new NavigationParameters();

            if (action.InternCollection != null)
            {
                InternCollection = new ObservableCollection<ListEntity>(action.InternCollection.Where(x => x.Added));
                navParameters.Add("interns", InternCollection);
            }
        
            navParameters.Add("patient", Patient);
            navParameters.Add("id_action", Action.Id);
            navParameters.Add("type_consultation", ConsultationType);

            await navigationService.NavigateAsync("ScheduleAppointmentPage", navParameters);
        }

        public void BuscarInternsCollection()
        {
            IsBusy = true;
            InternCollection = new ObservableCollection<ListEntity>();
            if(Action.InternCollection != null)
            {
                for (int i = 0; i < Action.InternCollection.Count(); i++)
                {
                    InternCollection.Add(new ListEntity
                    {
                        Id = Action.InternCollection[i].Id,
                        Name = Action.InternCollection[i].Name,
                        Added = false
                    });
                }
            }
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
                BuscarInternsCollection();
            }
            if (parameters["type_consultation"] != null)
            {
                ConsultationType = int.Parse(parameters["type_consultation"].ToString());
            }

        }
    }
}
