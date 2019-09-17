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
    public class CadastroServidorPageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _salvarServidorCommand { get; set; }
        public DelegateCommand _editarListaEstagiarios
        { get; set; }
        public DelegateCommand SalvarServidorCommand => _salvarServidorCommand ?? (_salvarServidorCommand = new DelegateCommand(SalvarServidorAsync));
        public DelegateCommand EditarListaEstagiarios => _editarListaEstagiarios ?? (_editarListaEstagiarios = new DelegateCommand(SalvarServidorAsync));
        #endregion

        private Servidor servidor;
        private string estagiarioCollectionString;
        private bool admin;

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

        private INavigationService navigationService;
        private IServidorRepository servidorRepository;
        private IEstagiarioRepository estagiarioRepository;

        public CadastroServidorPageViewModel(INavigationService navigationService, IServidorRepository servidorRepository, IEstagiarioRepository estagiarioRepository)
            : base(navigationService)
        {
            this.navigationService = navigationService;
            this.servidorRepository = servidorRepository;
            this.estagiarioRepository = estagiarioRepository;
            Servidor = new Servidor();
        }

        internal async void ItemTapped(Estagiario estagiario)
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("estagiario", estagiario);
            await navigationService.NavigateAsync("CadastroEstagiarioPage", navParameters);
        }

        public async void SalvarServidorAsync()
        {
            try
            {
                if (admin)
                {
                    User user = new User() { Email = Servidor.Email, Password = Servidor.Senha, Tipo = "servidor" };
                    //var result = await firebase.CreateUserAsync(user);
                    var result = "";

                    if (String.IsNullOrEmpty(result))
                    {
                        Servidor.UserId = user.UserId;

                        await servidorRepository.AddAsync(Servidor);
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
                    await servidorRepository.AddAsync(Servidor);
                    await navigationService.GoBackAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async void GetServidorAsync()
        {
            try
            {

                if (!String.IsNullOrEmpty(Servidor.UserId))
                {
                    Servidor = await servidorRepository.GetAsync(Servidor.UserId);

                    if (Servidor.IsProfessor)
                    {
                        Servidor.EstagiarioCollection = await estagiarioRepository.GetEstagiariosByResponsavelIdAsync(Servidor.UserId);


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
            else
            {
                admin = true;
            }
        }
    }
}
