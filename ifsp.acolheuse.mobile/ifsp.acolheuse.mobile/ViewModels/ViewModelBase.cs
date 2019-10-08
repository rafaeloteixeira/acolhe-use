using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace ifsp.acolheuse.mobile.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }

        private string _title;
        private bool _isBusy;
        private bool _canNavigate = true;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value, () => RaisePropertyChanged(nameof(IsNotBusy))); }
        }
        public bool IsNotBusy
        {
            get { return !IsBusy; }
        }
        public bool CanNavigate
        {
            get { return _canNavigate; }
            set { SetProperty(ref _canNavigate, value); }
        }
        public CultureInfo MaskedEditCultureInfo
        {
            get { return new CultureInfo("en-US"); }
        }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
            isNavigating = false;
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }
        public virtual void OnNavigatingTo(INavigationParameters parameters) { }

        public virtual void Destroy()
        {

        }

        #region Navigation

        public virtual bool OnBackButtonPressed => true;

        public void BackButtonPressed()
        {
            if (NavigationService != null)
                if (OnBackButtonPressed)
                    NavigationService.GoBackAsync();
        }

        private bool isNavigating;

        protected Task SafeNavigateAsync(string name, NavigationParameters parameters = null, bool? useModalNavigation = null, bool animated = true)
        {
            if (isNavigating)
                return Task.CompletedTask;
            isNavigating = true;
            try { return NavigationService.NavigateAsync(name, parameters, useModalNavigation, animated); }
            catch { return Task.CompletedTask; }
        }

        #endregion
    }
}
