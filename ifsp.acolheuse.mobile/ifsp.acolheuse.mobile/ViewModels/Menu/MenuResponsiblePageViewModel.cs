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
using System.Threading.Tasks;

namespace ifsp.acolheuse.mobile.ViewModels
{
    public class MenuResponsiblePageViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        private IResponsibleRepository responsibleRepository;
        private IMessageRepository messageRepository;
        public MenuResponsiblePageViewModel(INavigationService navigationService, IResponsibleRepository responsibleRepository, IMessageRepository messageRepository) :
        base(navigationService)
        {
            this.navigationService = navigationService;
            this.responsibleRepository = responsibleRepository;
            this.messageRepository = messageRepository;

            ListMenu = new List<MenuModel>()
                {
                    new MenuModel{Id = 0, Titulo = "Meu perfil"},
                    new MenuModel{Id = 1, Titulo = "Minhas ações"},
                    new MenuModel{Id = 2, Titulo = "Meus estagiários"},
                    new MenuModel{Id = 3, Titulo = "Usuários que atendo"},
                    new MenuModel{Id = 4, Titulo = "Agenda"},
                    new MenuModel{Id = 5, Titulo = "Mensagens"},
                    new MenuModel{Id = 6, Titulo = "Sair"}
                };
        }
        private IEnumerable<MenuModel> listMenu;
        public IEnumerable<MenuModel> ListMenu
        {
            get { return listMenu; }
            set { listMenu = value; RaisePropertyChanged(); }
        }

        private async void OnAppearing()
        {
            string badgeNumber = await GetMessagesCount();

            var messageMenu = ListMenu.FirstOrDefault(x => x.Id == 5);
            messageMenu.Badge = badgeNumber;
        }
        private async Task<string> GetMessagesCount()
        {
            int result = await messageRepository.GetCountNewMessagesAsync(Settings.UserId);
            if (result > 0)
            {
                return result.ToString();
            }
            else
            {
                return string.Empty;
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
                    await navigationService.NavigateAsync("MessagesPage");
                    break;
                case 6:
                    await MessageService.Instance.ShowAsync("Você saiu");
                    Settings.InitializeSettings();
                    await NavigationService.NavigateAsync("/NavigationPage/LoginPage");
                    break;
            }
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            OnAppearing();
        }
    }
}
