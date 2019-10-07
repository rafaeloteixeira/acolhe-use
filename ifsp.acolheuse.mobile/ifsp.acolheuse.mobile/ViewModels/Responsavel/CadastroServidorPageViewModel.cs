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

namespace ifsp.acolheuse.mobile.ViewModels.Responsavel
{
    public class CadastroServidorPageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _salvarServidorCommand { get; set; }
        public DelegateCommand _editarListaEstagiarios { get; set; }
        public DelegateCommand SalvarServidorCommand => _salvarServidorCommand ?? (_salvarServidorCommand = new DelegateCommand(SalvarServidorAsync));
        public DelegateCommand EditarListaEstagiarios => _editarListaEstagiarios ?? (_editarListaEstagiarios = new DelegateCommand(SalvarServidorAsync));


        #endregion

        private Servidor servidor;
        private string estagiarioCollectionString;
        private bool isAdmin;
        private bool passHasError;

        public Servidor Servidor
        {
            get { return servidor; }
            set { servidor = value; RaisePropertyChanged(); }
        }

        public string EstagiarioCollectionString
        {
            get { return estagiarioCollectionString; }
            set { estagiarioCollectionString = value; RaisePropertyChanged(); }
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
        private IServidorRepository servidorRepository;
        private IEstagiarioRepository estagiarioRepository;
        private IUserRepository userRepository;
        FirebaseAccess firebase = new FirebaseAccess();

        public CadastroServidorPageViewModel(INavigationService navigationService, IServidorRepository servidorRepository, IEstagiarioRepository estagiarioRepository, IUserRepository userRepository)
            : base(navigationService)
        {
            this.navigationService = navigationService;
            this.servidorRepository = servidorRepository;
            this.estagiarioRepository = estagiarioRepository;
            this.userRepository = userRepository;

            Servidor = new Servidor();
        }

        internal async void ItemTapped(Estagiario estagiario)
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("estagiario", estagiario);
            navParameters.Add("admin", true);
            await navigationService.NavigateAsync("CadastroEstagiarioPage", navParameters);
        }

        public async void SalvarServidorAsync()
        {
            if (String.IsNullOrEmpty(Servidor.Id) && IsAdmin)
            {
                if (!PassHasError)
                {
                    User user = new User() { Email = Servidor.Email, Password = Servidor.Senha, Tipo = "servidor" };
                    var result = await firebase.CreateUserAsync(user);


                    if (String.IsNullOrEmpty(result))
                    {
                        Servidor.AccountId = user.Id;
                        await servidorRepository.AddAsync(Servidor);
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
                await servidorRepository.AddOrUpdateAsync(Servidor, Servidor.Id);
                await navigationService.GoBackAsync();
            }
        }

        public async void GetServidorAsync()
        {
            try
            {
                if (!IsAdmin)
                {
                    Servidor = await servidorRepository.GetAsync(Settings.UserId);
                }
                else
                {
                    if (!String.IsNullOrEmpty(Servidor.Id))
                    {
                        Servidor = await servidorRepository.GetAsync(Servidor.Id);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async void ExcluirEstagiarioAsync(string id)
        {
            try
            {
                await estagiarioRepository.RemoveAsync(id);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public void AdicionarEstagiario() { }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters["servidor"] != null)
            {
                Servidor = parameters["servidor"] as Servidor;
            }

            if (parameters["admin"] != null)
            {
                IsAdmin = true;
            }

            GetServidorAsync();

        }

        internal void CheckPass()
        {
            if (!String.IsNullOrEmpty(Servidor.Senha) && String.IsNullOrEmpty(Servidor.ConfirmarSenha) 
                || String.IsNullOrEmpty(Servidor.Senha) && !String.IsNullOrEmpty(Servidor.ConfirmarSenha))
            {
                PassHasError = true;
            }
            else if(Servidor.Senha != Servidor.ConfirmarSenha)
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
