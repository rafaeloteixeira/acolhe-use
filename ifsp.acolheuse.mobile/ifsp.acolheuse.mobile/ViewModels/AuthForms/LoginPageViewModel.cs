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
using System.Linq;

namespace ifsp.acolheuse.mobile.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _loginCommand { get; set; }
        public DelegateCommand LoginCommand => _loginCommand ?? (_loginCommand = new DelegateCommand(LoginCommandExecute));
        #endregion

        #region properties
        private User patient;
        public User Patient
        {
            get { return patient; }
            set { patient = value; RaisePropertyChanged(); }
        }

        #endregion

        INavigationService navigationService;
        IUserRepository userRepository;
        IResponsibleRepository responsibleRepository;
        IInternRepository internRepository;
        public LoginPageViewModel(INavigationService navigationService, IUserRepository userRepository, IResponsibleRepository responsibleRepository, IInternRepository internRepository) :
          base(navigationService)
        {
            Patient = new User();
            this.navigationService = navigationService;
            this.userRepository = userRepository;
            this.responsibleRepository = responsibleRepository;
            this.internRepository = internRepository;
        }

        public async void LoginCommandExecute()
        {
            try
            {
                IsBusy = true;
                FirebaseAccess firebase = new FirebaseAccess();
                var result = await firebase.LoginAsync(Patient);

                if (!String.IsNullOrEmpty(result))
                {
                    await MessageService.Instance.ShowAsync(result);
                }
                else
                {
                    var user = await userRepository.GetByAccessTokenAsync(Settings.AccessToken);
                    Settings.Email = user.Email;
                    Settings.Type = user.Type;

                    
                    switch (user.Type)
                    {
                        case "admin":
                            await NavigationService.NavigateAsync("/NavigationPage/MenuAdminPage");
                            break;
                        case "responsible":
                            Responsible responsible = await responsibleRepository.GetByAccessTokenAsync(Settings.AccessToken);
                            Settings.UserId = responsible.Id;
                            await NavigationService.NavigateAsync("/NavigationPage/MenuResponsiblePage");
                            break;
                        case "intern":
                            Intern intern = await internRepository.GetByAccessTokenAsync(Settings.AccessToken);
                            Settings.UserId = intern.Id;
                            await NavigationService.NavigateAsync("/NavigationPage/MenuInternPage");
                            break;
                        case "host":
                            await NavigationService.NavigateAsync("/NavigationPage/MenuHostPage");
                            break;
                    }
                }
                IsBusy = false;
            }
            catch (Exception ex)
            {
                await MessageService.Instance.ShowAsync(ex.ToString());
                throw;
            }
        }
    }
}
