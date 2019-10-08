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
    public class MenuResponsiblePageViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        private IResponsibleRepository responsibleRepository;
        public MenuResponsiblePageViewModel(INavigationService navigationService, IResponsibleRepository responsibleRepository) :
        base(navigationService)
        {
            this.navigationService = navigationService;
            this.responsibleRepository = responsibleRepository;
        }
        public List<MenuModel> ListMenu
        {
            get
            {
                return new List<MenuModel>()
                {
                    new MenuModel{Id = 0, Titulo = "Editar perfil"},
                    new MenuModel{Id = 1, Titulo = "Gerenciar minhas ações"},
                    new MenuModel{Id = 2, Titulo = "Gerenciar estagiários"},
                    new MenuModel{Id = 3, Titulo = "Visualizar usuários que atendo"},
                    new MenuModel{Id = 4, Titulo = "Visualizar agenda"},
                    new MenuModel{Id = 5, Titulo = "Sair"}
                };
            }
        }

        public async void NavegarMenu(int Id)
        {
        

            switch (Id)
            {
                case 0:
                    await navigationService.NavigateAsync("RegisterResponsiblePage");
                    break;
                case 1:
                    await navigationService.NavigateAsync("ListActionResponsiblePage");
                    break;
                case 2:
                    await navigationService.NavigateAsync("ListInternsResponsiblePage");
                    break;
                case 3:
                    await navigationService.NavigateAsync("ListPatientsResponsiblePage");
                    break;
                case 4:
                    await navigationService.NavigateAsync("ScheduleResponsiblePage");
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
