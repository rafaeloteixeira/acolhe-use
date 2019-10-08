using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using ifsp.acolheuse.mobile.Persistence.FirebaseConfigurations;
using ifsp.acolheuse.mobile.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ifsp.acolheuse.mobile.ViewModels.Estagio
{
    public class RegisterInternPageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _saveInternCommand { get; set; }

        public DelegateCommand SaveInternCommand => _saveInternCommand ?? (_saveInternCommand = new DelegateCommand(SaveInternAsync));


        #endregion

        #region properties
        private Intern intern;
        private Responsible responsibleOrientador;
        private IEnumerable<Responsible> responsibleCollection;
        private bool admin;
        private bool passHasError;

        public Intern Intern
        {
            get { return intern; }
            set { intern = value; RaisePropertyChanged(); }
        }
        public Responsible ResponsibleOrientador
        {
            get { return responsibleOrientador; }
            set { responsibleOrientador = value; RaisePropertyChanged(); }
        }
        public IEnumerable<Responsible> ResponsibleCollection
        {
            get { return responsibleCollection; }
            set { responsibleCollection = value; RaisePropertyChanged(); }
        }
        public bool PassHasError
        {
            get { return passHasError; }
            set { passHasError = value; RaisePropertyChanged(); }
        }



        #endregion

        FirebaseAccess firebase = new FirebaseAccess();
        private INavigationService navigationService;
        private IInternRepository internRepository;
        private IResponsibleRepository responsibleRepository;
        private IUserRepository userRepository;

        public RegisterInternPageViewModel(INavigationService navigationService, IInternRepository internRepository, IResponsibleRepository responsibleRepository, IUserRepository userRepository) :
            base(navigationService)
        {
            this.navigationService = navigationService;
            this.internRepository = internRepository;
            this.responsibleRepository = responsibleRepository;
            this.userRepository = userRepository;

            Intern = new Intern();
            ResponsibleOrientador = new Responsible();
        }

        public async void SaveInternAsync()
        {
            Intern.IdResponsible = ResponsibleOrientador.Id;

            if (admin && String.IsNullOrEmpty(Intern.Id))
            {
                if (!PassHasError)
                {
                    User user = new User() { Email = Intern.Email, Password = Intern.Password, Type = "intern" };
                    var result = await firebase.CreateUserAsync(user);

                    if (String.IsNullOrEmpty(result))
                    {
                        Intern.AccessToken = user.AccessToken;
                        await internRepository.AddAsync(Intern);
                        await userRepository.AddAsync(user);
                        await navigationService.GoBackAsync();
                    }
                    else
                    {
                        await MessageService.Instance.ShowAsync(result);
                    }
                }
            }
            else
            {
                await internRepository.AddOrUpdateAsync(Intern, Intern.Id);
                await navigationService.GoBackAsync();
            }
        }

        public async void GetInternAsync()
        {
            try
            {
                if (!String.IsNullOrEmpty(Intern.Id))
                {
                    Intern = await internRepository.GetAsync(Intern.Id);
                    ResponsibleOrientador = await responsibleRepository.GetAsync(Intern.IdResponsible);
                }

                ResponsibleCollection = await responsibleRepository.GetAllAsync();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters["intern"] != null)
            {
                Intern = parameters["intern"] as Intern;
            }

            if (parameters["admin"] != null)
            {
                admin = true;
            }
            GetInternAsync();

        }
        internal void CheckPassword()
        {
            if (!String.IsNullOrEmpty(Intern.Password) && String.IsNullOrEmpty(Intern.ConfirmPassword)
                || String.IsNullOrEmpty(Intern.Password) && !String.IsNullOrEmpty(Intern.ConfirmPassword))
            {
                PassHasError = true;
            }
            else if (Intern.Password != Intern.ConfirmPassword)
            {
                PassHasError = true;
            }
            else
            {
                PassHasError = false;
            }
        }

    }
}
