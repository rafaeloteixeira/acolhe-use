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
    public class MenuHostPageViewModel : ViewModelBase
    {
        private INavigationService navigationService;

        public MenuHostPageViewModel(INavigationService navigationService) :
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
                    new MenuModel{Id = 0, Titulo = "Visualizar responsáveis"},
                    new MenuModel{Id = 1, Titulo = "Visualizar linhas"},
                    new MenuModel{Id = 2, Titulo = "Visualizar ações"},
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
                    await navigationService.NavigateAsync("ListResponsibleHostPage");
                    break;
                case 1:
                    await navigationService.NavigateAsync("ListLinesHostPage");
                    break;
                case 2:
                    await navigationService.NavigateAsync("ListActionHostPage");
                    break;
                case 3:
                    await navigationService.NavigateAsync("ListInternsHostPage");
                    break;
                case 4:
                    await navigationService.NavigateAsync("ListPatientsHostPage");
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
