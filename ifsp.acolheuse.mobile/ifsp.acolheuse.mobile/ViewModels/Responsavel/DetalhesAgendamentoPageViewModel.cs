using ifsp.acolheuse.mobile.Core.Domain;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ifsp.acolheuse.mobile.ViewModels.Responsavel
{
    public class DetalhesAgendamentoPageViewModel : ViewModelBase
    {
        #region properties

        private IEnumerable<Estagiario> estagiariosCollection;

        public IEnumerable<Estagiario> EstagiariosCollection
        {
            get { return estagiariosCollection; }
            private set { estagiariosCollection = value; RaisePropertyChanged(); }
        }
        #endregion

        private INavigationService navigationService;
        public DetalhesAgendamentoPageViewModel(INavigationService navigationService) :
          base(navigationService)
        {
            this.navigationService = navigationService;
        }


        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters["estagiarios"] != null)
            {
                estagiariosCollection = parameters["estagiarios"] as IEnumerable<Estagiario>;
            }
        }
    }
}
