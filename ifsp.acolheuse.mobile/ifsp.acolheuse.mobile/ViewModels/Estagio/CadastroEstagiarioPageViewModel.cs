using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using ifsp.acolheuse.mobile.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ifsp.acolheuse.mobile.ViewModels
{
    public class CadastroEstagiarioPageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _salvarEstagiarioCommand { get; set; }

        public DelegateCommand SalvarEstagiarioCommand => _salvarEstagiarioCommand ?? (_salvarEstagiarioCommand = new DelegateCommand(SalvarEstagiarioAsync));

        #endregion

        #region properties
        private Estagiario estagiario;
        private IEnumerable<Servidor> professorCollection;
        private bool admin;
        public Estagiario Estagiario
        {
            get { return estagiario; }
            set { estagiario = value; RaisePropertyChanged(); }
        }
        public IEnumerable<Servidor> ProfessorCollection
        {
            get { return professorCollection; }
            set { professorCollection = value; RaisePropertyChanged(); }
        }
        #endregion

        private INavigationService navigationService;
        private IEstagiarioRepository estagiarioRepository;
        private IServidorRepository servidorRepository;

        public CadastroEstagiarioPageViewModel(INavigationService navigationService, IEstagiarioRepository estagiarioRepository, IServidorRepository servidorRepository) :
            base(navigationService)
        {
            this.navigationService = navigationService;
            this.estagiarioRepository = estagiarioRepository;
            this.servidorRepository = servidorRepository;

            Estagiario = new Estagiario();
        }

        public async void SalvarEstagiarioAsync()
        {
            if (admin)
            {
                User user = new User() { Email = Estagiario.Email, Password = Estagiario.Senha, Tipo = "estagiario" };

                //var result = await firebase.CreateUserAsync(user);
                var result = "";

                if (String.IsNullOrEmpty(result))
                {
                    Estagiario.UserId = user.UserId;
                    await estagiarioRepository.AddAsync(Estagiario);
                    //await firebase.GetConnection().Child("users").Child(user.UserId).PutAsync(user);
                    await navigationService.GoBackAsync();
                }
                else
                {
                    await MessageService.Instance.ShowAsync(result);
                }
            }
            else
            {
                await estagiarioRepository.AddAsync(Estagiario);
                await navigationService.GoBackAsync();
            }
        }

        public async void GetEstagiarioAsync()
        {
            ProfessorCollection = await servidorRepository.GetAllAsync();

            if (!String.IsNullOrEmpty(Estagiario.UserId))
            {
                Estagiario = await estagiarioRepository.GetAsync(Estagiario.UserId);
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
            else
            {
                admin = true;
            }
        }
    }
}
