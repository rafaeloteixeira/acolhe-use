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
    public class MenuAdminPageViewModel : ViewModelBase
    {
        private INavigationService navigationService;

        public MenuAdminPageViewModel(INavigationService navigationService) :
        base(navigationService)
        {
            this.navigationService = navigationService;
        }
        public List<MenuModel> ListMenu
        {
            get
            {
                return new List<MenuModel>()
                {
                    new MenuModel{Id = 0, Titulo = "Gerenciar responsáveis"},
                    new MenuModel{Id = 1, Titulo = "Gerenciar linhas"},
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
                    await navigationService.NavigateAsync("ListResponsibleAdminPage");
                    break;
                case 1:
                    await navigationService.NavigateAsync("ListLinesAdminPage");
                    break;
                case 2:
                    await navigationService.NavigateAsync("ListActionAdminPage");
                    break;
                case 3:
                    await navigationService.NavigateAsync("ListInternsAdminPage");
                    break;
                case 4:
                    await navigationService.NavigateAsync("ListPatientsAdminPage");
                    break;
                case 5:
                    await MessageService.Instance.ShowAsync("Você saiu");
                    Settings.InitializeSettings();
                    await NavigationService.NavigateAsync("/NavigationPage/LoginPage");
                    break;
            }
        }
    }
}
