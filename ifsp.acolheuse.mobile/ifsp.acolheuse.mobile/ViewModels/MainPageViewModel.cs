using ifsp.acolheuse.mobile.Core.Settings;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ifsp.acolheuse.mobile.ViewModels
{
    class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService) :
          base(navigationService)
        { }

        internal async void NavigateServ()
        {
            Settings.UserId = "MUpZRnWd2vMfPCBt3c0N";
            await NavigationService.NavigateAsync("/NavigationPage/MenuResponsavelPage");
        }

        internal async void NavigateAdmin()
        {
            await NavigationService.NavigateAsync("/NavigationPage/MenuAdminPage");
        }
    }
}
