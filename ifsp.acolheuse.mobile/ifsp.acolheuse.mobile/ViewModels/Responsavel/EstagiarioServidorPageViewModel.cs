using ifsp.acolheuse.mobile.Core.Domain;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ifsp.acolheuse.mobile.ViewModels.Responsavel
{
    public class EstagiarioServidorPageViewModel : ViewModelBase
    {
        #region properties

        private Estagiario estagiario;
        public Estagiario Estagiario
        {
            get { return estagiario; }
            set { estagiario = value; RaisePropertyChanged(); }
        }
        #endregion

        private INavigationService navigationService;
        public EstagiarioServidorPageViewModel(INavigationService navigationService) :
          base(navigationService)
        {
            this.navigationService = navigationService;
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters["estagiario"] != null)
            {
                Estagiario = parameters["estagiario"] as Estagiario;
            }
        }
    }
}
