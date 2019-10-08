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
        private ObservableCollection<Appointment> appointmentCollection;

        public ObservableCollection<Appointment> AppointmentCollection
        {
            get { return appointmentCollection; }
            set { appointmentCollection = value; RaisePropertyChanged(); }
        }

        INavigationService navigationService;
        IInternRepository internRepository;
        IAppointmentRepository appointmentRepository;

        public InternAttendancePageViewModel(INavigationService navigationService, IInternRepository internRepository, IAppointmentRepository appointmentRepository) : base(navigationService)
        {
            this.navigationService = navigationService;
            this.internRepository = internRepository;
            this.appointmentRepository = appointmentRepository;
        }


        public async void BuscarAppointmentsAsync()
        {
            AppointmentCollection = new ObservableCollection<Appointment>();

            Intern Intern = await internRepository.GetAsync(Settings.UserId);

            var appointments = await appointmentRepository.GetAllByInternId(Intern.Id);
        }
    }
}
