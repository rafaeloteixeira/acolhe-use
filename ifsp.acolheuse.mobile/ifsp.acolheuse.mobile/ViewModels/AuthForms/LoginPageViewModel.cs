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
        private User usuario;
        public User Usuario
        {
            get { return usuario; }
            set { usuario = value; RaisePropertyChanged(); }
        }

        #endregion

        INavigationService navigationService;
        IUserRepository userRepository;
        IServidorRepository servidorRepository;
        public LoginPageViewModel(INavigationService navigationService, IUserRepository userRepository, IServidorRepository servidorRepository) :
          base(navigationService)
        {
            Usuario = new User();
            this.navigationService = navigationService;
            this.userRepository = userRepository;
            this.servidorRepository = servidorRepository;
        }

        public async void LoginCommandExecute()
        {
            try
            {
                FirebaseAccess firebase = new FirebaseAccess();
                var result = await firebase.LoginAsync(Usuario);

                if (!String.IsNullOrEmpty(result))
                {
                    await MessageService.Instance.ShowAsync(result);
                }
                else
                {
                    var user = await userRepository.GetByLocalIdAsync(Settings.AccessToken);
                    Settings.Email = user.Email;
                    Settings.AccountId = user.Id;
                    Settings.Tipo = user.Tipo;


                    switch (user.Tipo)
                    {
                        case "admin":
                            await NavigationService.NavigateAsync("/NavigationPage/MenuAdminPage");
                            break;
                        case "servidor":
                            Servidor servidor = await servidorRepository.GetByAccountIdAsync(Settings.AccountId);
                            Settings.UserId = servidor.Id;
                            await NavigationService.NavigateAsync("/NavigationPage/MenuResponsavelPage");
                            break;
                        case "estagiario":
                            await NavigationService.NavigateAsync("/NavigationPage/MenuResponsavelPage");
                            break;
                        case "acolhimento":
                            await NavigationService.NavigateAsync("/NavigationPage/MenuResponsavelPage");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                await MessageService.Instance.ShowAsync(ex.ToString());
                throw;
            }
        }
    }
}
