using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using ifsp.acolheuse.mobile.Core.Settings;
using ifsp.acolheuse.mobile.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ifsp.acolheuse.mobile.ViewModels
{
    public class MenuResponsavelPageViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        private IServidorRepository servidorRepository;
        public MenuResponsavelPageViewModel(INavigationService navigationService, IServidorRepository servidorRepository) :
        base(navigationService)
        {
            this.navigationService = navigationService;
            this.servidorRepository = servidorRepository;
        }
        public List<MenuModel> ListaMenu
        {
            get
            {
                return new List<MenuModel>()
                {
                    new MenuModel{Id = 0, Titulo = "Editar perfil"},
                    new MenuModel{Id = 1, Titulo = "Gerenciar minhas ações"},
                    //new MenuModel{Id = 2, Titulo = "Gerenciar estagiários"},
                    new MenuModel{Id = 3, Titulo = "Visualizar usuários que atendo"},
                    new MenuModel{Id = 4, Titulo = "Visualizar agenda"},
                    new MenuModel{Id = 5, Titulo = "Sair"}
                };
            }
        }

        public async void NavegarMenu(int Id)
        {
            Servidor servidor = await servidorRepository.GetAsync(Settings.UserId);

            switch (Id)
            {
                case 0:
                    var navParameters = new NavigationParameters();
                    navParameters.Add("servidor", servidor);
                    await navigationService.NavigateAsync("CadastroServidorPage", navParameters);
                    break;
                case 1:
                    await navigationService.NavigateAsync("ListaAcoesResponsavelPage");
                    break;
                case 2:
                    await navigationService.NavigateAsync("ListaEstagiariosResponsavelPage");
                    break;
                case 3:
                    await navigationService.NavigateAsync("ListaEstagiariosResponsavelPage");
                    break;
                case 4:
                    await navigationService.NavigateAsync("ListaUsuariosResponsavelPage");
                    break;
                case 5:
                    await MessageService.Instance.ShowAsync("Você saiu");
                    await navigationService.GoBackToRootAsync();
                    break;
            }
        }
    }
}
