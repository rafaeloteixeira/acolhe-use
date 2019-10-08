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
    public class MenuInternPageViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        private IResponsibleRepository responsibleRepository;
        public MenuInternPageViewModel(INavigationService navigationService, IResponsibleRepository responsibleRepository) :
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
                    new MenuModel{Id = 1, Titulo = "Minha agenda"},
                    new MenuModel{Id = 2, Titulo = "Confirmar comparecimento"},
                    new MenuModel{Id = 3, Titulo = "Sair"}
                };
            }
        }

        public async void NavegarMenu(int Id)
        {
            switch (Id)
            {
                case 0:
                    await navigationService.NavigateAsync("RegisterInternPage");
                    break;
                case 1:
                    await navigationService.NavigateAsync("ScheduleInternPage");
                    break;
                case 2:
                    await navigationService.NavigateAsync("InternAttendancePage");
                    break;
                case 3:
                    await MessageService.Instance.ShowAsync("Você saiu");
                    Settings.InitializeSettings();
                    await NavigationService.NavigateAsync("/NavigationPage/LoginPage");
                    break;
            }
        }
    }
}
