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

        public MenuResponsavelPageViewModel(INavigationService navigationService) :
        base(navigationService)
        {
            this.navigationService = navigationService;
        }
        public List<MenuModel> ListaMenu
        {
            get
            {
                return new List<MenuModel>()
                {
                    new MenuModel{Id = 0, Titulo = "Gerenciar servidores"},
                    new MenuModel{Id = 1, Titulo = "Gerenciar linhas de cuidado"},
                    new MenuModel{Id = 2, Titulo = "Gerenciar ações"},
                    new MenuModel{Id = 3, Titulo = "Gerenciar estagiários"},
                    new MenuModel{Id = 4, Titulo = "Gerenciar usuários"},
                    new MenuModel{Id = 5, Titulo = "Sair"}
                };
            }
        }

        public async void NavegarMenu(int Id)
        {
            switch (Id)
            {
                case 0:
                    await navigationService.NavigateAsync("ListaServidoresResponsavelPage");
                    break;
                case 1:
                    await navigationService.NavigateAsync("ListaLinhasResponsavelPage");
                    break;
                case 2:
                    await navigationService.NavigateAsync("ListaAcoesResponsavelPage");
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
