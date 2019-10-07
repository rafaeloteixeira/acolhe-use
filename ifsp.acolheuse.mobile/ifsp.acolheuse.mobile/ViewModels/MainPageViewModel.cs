using ifsp.acolheuse.mobile.Core.Settings;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ifsp.acolheuse.mobile.ViewModels
{
    class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService) :
          base(navigationService)
        { }

        internal async void NavigateServ()
        {
            await NavigationService.NavigateAsync("/NavigationPage/MenuResponsavelPage");
        }

        internal async void NavigateAdmin()
        {
            IsBusy = true;
            await Task.Delay(4000);
            IsBusy = false;
            await NavigationService.NavigateAsync("/NavigationPage/MenuAdminPage");
        }
    }
}
