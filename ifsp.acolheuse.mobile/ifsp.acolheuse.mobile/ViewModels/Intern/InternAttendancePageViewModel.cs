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
        private DateTime startTime;
        private ObservableCollection<Appointment> appointmentCollection;

        public ObservableCollection<Appointment> AppointmentCollection
        {
            get { return appointmentCollection; }
            set { appointmentCollection = value; RaisePropertyChanged(); }
        }
        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; RaisePropertyChanged(); }
        }
        #endregion

        #region commands
        public DelegateCommand
            _saveCommand { get; set; }

        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(SaveAsync));

        #endregion

        private INavigationService navigationService;
        private IAppointmentRepository appointmentRepository;
        private ICheckOutRepository checkOutRepository;
        public InternAttendancePageViewModel(INavigationService navigationService, IAppointmentRepository appointmentRepository, ICheckOutRepository checkOutRepository) :
            base(navigationService)
        {
            this.navigationService = navigationService;
            this.appointmentRepository = appointmentRepository;
            this.checkOutRepository = checkOutRepository;
            Title = "Comparecimento";
            StartTime = DateTime.Now;
        }

        public async void GetAppointmentsAsync()
        {
            IsBusy = true;
            AppointmentCollection = new ObservableCollection<Appointment>(await appointmentRepository.GetAllByInternIdStartTime(Settings.UserId, StartTime));

            for (int i = 0; i < AppointmentCollection.Count(); i++)
            {
                CheckOut checkout = await checkOutRepository.GetByAppointmentIdDateAsync(AppointmentCollection.ElementAt(i).Id, StartTime);

                if (checkout != null)
                    AppointmentCollection.ElementAt(i).Confirmed = checkout.Confirmed;
            }

            IsBusy = false;
        }
        public async void SaveAsync()
        {
            IsBusy = true;

            for (int i = 0; i < AppointmentCollection.Count(); i++)
            {
                CheckOut checkout = await checkOutRepository.GetByAppointmentIdDateAsync(AppointmentCollection.ElementAt(i).Id, StartTime);

                if (checkout != null)
                {
                    checkout.Confirmed = AppointmentCollection.ElementAt(i).Confirmed;
                    await checkOutRepository.AddOrUpdateAsync(checkout, checkout.Id);
                }
                else
                {
                    checkout = new CheckOut();
                    checkout.Confirmed = true;
                    checkout.Date = StartTime;
                    checkout.AppointmentId = AppointmentCollection.ElementAt(i).Id;
                    await checkOutRepository.AddAsync(checkout);
                }
            }
            IsBusy = false;


            GetAppointmentsAsync();
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            GetAppointmentsAsync();
        }
    }
}
