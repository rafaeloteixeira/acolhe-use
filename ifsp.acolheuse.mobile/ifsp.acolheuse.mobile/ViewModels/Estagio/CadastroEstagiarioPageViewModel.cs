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
    public class CadastroEstagiarioPageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _salvarEstagiarioCommand { get; set; }

        public DelegateCommand SalvarEstagiarioCommand => _salvarEstagiarioCommand ?? (_salvarEstagiarioCommand = new DelegateCommand(SalvarEstagiarioAsync));


        #endregion

        #region properties
        private Estagiario estagiario;
        private Servidor professorOrientador;
        private IEnumerable<Servidor> professorCollection;
        private bool admin;
        private bool passHasError;

        public Estagiario Estagiario
        {
            get { return estagiario; }
            set { estagiario = value; RaisePropertyChanged(); }
        }
        public Servidor ProfessorOrientador
        {
            get { return professorOrientador; }
            set { professorOrientador = value; RaisePropertyChanged(); }
        }
        public IEnumerable<Servidor> ProfessorCollection
        {
            get { return professorCollection; }
            set { professorCollection = value; RaisePropertyChanged(); }
        }
        public bool PassHasError
        {
            get { return passHasError; }
            set { passHasError = value; RaisePropertyChanged(); }
        }



        #endregion

        FirebaseAccess firebase = new FirebaseAccess();
        private INavigationService navigationService;
        private IEstagiarioRepository estagiarioRepository;
        private IServidorRepository servidorRepository;
        private IUserRepository userRepository;

        public CadastroEstagiarioPageViewModel(INavigationService navigationService, IEstagiarioRepository estagiarioRepository, IServidorRepository servidorRepository, IUserRepository userRepository) :
            base(navigationService)
        {
            this.navigationService = navigationService;
            this.estagiarioRepository = estagiarioRepository;
            this.servidorRepository = servidorRepository;
            this.userRepository = userRepository;

            Estagiario = new Estagiario();
            ProfessorOrientador = new Servidor();
        }

        public async void SalvarEstagiarioAsync()
        {
            Estagiario.IdProfessor = ProfessorOrientador.Id;

            if (admin && String.IsNullOrEmpty(Estagiario.Id))
            {
                if (!PassHasError)
                {
                    User user = new User() { Email = Estagiario.Email, Password = Estagiario.Senha, Tipo = "estagiario" };
                    var result = await firebase.CreateUserAsync(user);

                    if (String.IsNullOrEmpty(result))
                    {
                        Estagiario.AccountId = user.AcessToken;
                        await estagiarioRepository.AddAsync(Estagiario);
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
                await estagiarioRepository.AddOrUpdateAsync(Estagiario, Estagiario.Id);
                await navigationService.GoBackAsync();
            }
        }

        public async void GetEstagiarioAsync()
        {
            try
            {
                if (!String.IsNullOrEmpty(Estagiario.Id))
                {
                    Estagiario = await estagiarioRepository.GetAsync(Estagiario.Id);
                    ProfessorOrientador = await servidorRepository.GetAsync(Estagiario.IdProfessor);
                }

                ProfessorCollection = await servidorRepository.GetAllAsync();
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
            if (parameters["estagiario"] != null)
            {
                Estagiario = parameters["estagiario"] as Estagiario;
            }

            if (parameters["admin"] != null)
            {
                admin = true;
            }
            GetEstagiarioAsync();

        }
        internal void CheckPassword()
        {
            if (!String.IsNullOrEmpty(Estagiario.Senha) && String.IsNullOrEmpty(Estagiario.ConfirmarSenha)
                || String.IsNullOrEmpty(Estagiario.Senha) && !String.IsNullOrEmpty(Estagiario.ConfirmarSenha))
            {
                PassHasError = true;
            }
            else if (Estagiario.Senha != Estagiario.ConfirmarSenha)
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
