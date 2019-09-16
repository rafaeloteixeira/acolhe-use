using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ifsp.acolheuse.mobile.ViewModels
{
    public class ViewBViewModel : ViewModelBase
    {
        public ViewBViewModel(INavigationService navigationService) :
            base(navigationService)
        {
            Title = "My View B";
        }
    }
}
