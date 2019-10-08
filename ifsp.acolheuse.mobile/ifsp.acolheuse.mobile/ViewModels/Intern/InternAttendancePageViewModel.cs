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
    public class InternAttendancePageViewModel : ViewModelBase
    {
        #region properties
        private ObservableCollection<Patient> patientCollection;

        public ObservableCollection<Patient> PatientCollection
        {
            get { return patientCollection; }
            set { patientCollection = value; RaisePropertyChanged(); }
        }
        #endregion

        #region commands
        public DelegateCommand _saveCommand { get; set; }

        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(SaveAsync));
        #endregion

        private INavigationService navigationService;
        public InternAttendancePageViewModel(INavigationService navigationService) :
            base(navigationService)
        {
            this.navigationService = navigationService;
            Title = "Comparecimento";
        }

        public async void SaveAsync()
        {
            await NavigationService.GoBackAsync();
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters["patients"] != null)
            {
                PatientCollection = parameters["patients"] as ObservableCollection<Patient>;
            }
        }
    }
}
