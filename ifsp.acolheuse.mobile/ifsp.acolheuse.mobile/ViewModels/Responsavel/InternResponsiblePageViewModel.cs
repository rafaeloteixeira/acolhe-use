using ifsp.acolheuse.mobile.Core.Domain;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ifsp.acolheuse.mobile.ViewModels
{
    public class InternResponsiblePageViewModel : ViewModelBase
    {
        #region properties

        private Intern intern;
        public Intern Intern
        {
            get { return intern; }
            set { intern = value; RaisePropertyChanged(); }
        }
        #endregion

        private INavigationService navigationService;
        public InternResponsiblePageViewModel(INavigationService navigationService) :
          base(navigationService)
        {
            this.navigationService = navigationService;
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters["intern"] != null)
            {
                Intern = parameters["intern"] as Intern;
            }
        }
    }
}
