using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using ifsp.acolheuse.mobile.Core.Settings;
using ifsp.acolheuse.mobile.Persistence.FirebaseConfigurations;
using ifsp.acolheuse.mobile.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ifsp.acolheuse.mobile.ViewModels
{
    public class RegisterResponsiblePageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _saveResponsibleCommand { get; set; }
        public DelegateCommand _editarListInterns { get; set; }
        public DelegateCommand SaveResponsibleCommand => _saveResponsibleCommand ?? (_saveResponsibleCommand = new DelegateCommand(SaveResponsibleAsync));
        public DelegateCommand EditarListInterns => _editarListInterns ?? (_editarListInterns = new DelegateCommand(SaveResponsibleAsync));


        #endregion

        private Responsible responsible;
        private string internCollectionString;
        private bool isAdmin;
        private bool passHasError;

        public Responsible Responsible
        {
            get { return responsible; }
            set { responsible = value; RaisePropertyChanged(); }
        }

        public string InternCollectionString
        {
            get { return internCollectionString; }
            set { internCollectionString = value; RaisePropertyChanged(); }
        }
        public bool IsAdmin
        {
            get { return isAdmin; }
            set { isAdmin = value; RaisePropertyChanged(); }
        }
        public bool PassHasError
        {
            get { return passHasError; }
            set { passHasError = value; RaisePropertyChanged(); }
        }

        

        private INavigationService navigationService;
        private IResponsibleRepository responsibleRepository;
        private IInternRepository internRepository;
        private IUserRepository userRepository;
        FirebaseAccess firebase = new FirebaseAccess();

        public RegisterResponsiblePageViewModel(INavigationService navigationService, IResponsibleRepository responsibleRepository, IInternRepository internRepository, IUserRepository userRepository)
            : base(navigationService)
        {
            this.navigationService = navigationService;
            this.responsibleRepository = responsibleRepository;
            this.internRepository = internRepository;
            this.userRepository = userRepository;

            Responsible = new Responsible();
        }

        internal async void ItemTapped(Intern intern)
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("intern", intern);
            navParameters.Add("admin", true);
            await navigationService.NavigateAsync("RegisterInternPage", navParameters);
        }

        public async void SaveResponsibleAsync()
        {
            if (String.IsNullOrEmpty(Responsible.Id) && IsAdmin)
            {
                if (!PassHasError)
                {
                    User user = new User() { Email = Responsible.Email, Password = Responsible.Password, Type = "responsible" };
                    var result = await firebase.CreateUserAsync(user);


                    if (String.IsNullOrEmpty(result))
                    {
                        Responsible.AccessToken = user.AccessToken;
                        await responsibleRepository.AddAsync(Responsible);
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
                await responsibleRepository.AddOrUpdateAsync(Responsible, Responsible.Id);
                await navigationService.GoBackAsync();
            }
        }

        public async void GetResponsibleAsync()
        {
            try
            {
                IsBusy = true;
                if (!IsAdmin)
                {
                    Responsible = await responsibleRepository.GetAsync(Settings.UserId);
                }
                else
                {
                    if (!String.IsNullOrEmpty(Responsible.Id))
                    {
                        Responsible = await responsibleRepository.GetAsync(Responsible.Id);
                    }
                }
                IsBusy = false;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async void ExcluirInternAsync(string id)
        {
            try
            {
                await internRepository.RemoveAsync(id);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public void AdicionarIntern() { }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters["responsible"] != null)
            {
                Responsible = parameters["responsible"] as Responsible;
            }

            if (parameters["admin"] != null)
            {
                IsAdmin = true;
            }

            GetResponsibleAsync();

        }

        internal void CheckPass()
        {
            if (!String.IsNullOrEmpty(Responsible.Password) && String.IsNullOrEmpty(Responsible.ConfirmPassword) 
                || String.IsNullOrEmpty(Responsible.Password) && !String.IsNullOrEmpty(Responsible.ConfirmPassword))
            {
                PassHasError = true;
            }
            else if(Responsible.Password != Responsible.ConfirmPassword)
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
