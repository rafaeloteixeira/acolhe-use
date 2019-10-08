using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using ifsp.acolheuse.mobile.Core.Repositories;
using Prism.Navigation;
using ifsp.acolheuse.mobile.Core.Domain;
using System.Collections.ObjectModel;

namespace ifsp.acolheuse.mobile.ViewModels.Administrador
{

    public class EditListResponsiblePageViewModel : ViewModelBase
    {
        #region properties
        private ObservableCollection<ListEntity> responsibleCollection;
        private ActionModel action;

        public ObservableCollection<ListEntity> ResponsibleCollection
        {
            get { return responsibleCollection; }
            set { responsibleCollection = value; RaisePropertyChanged(); }
        }
        public ActionModel Action
        {
            get { return action; }
            set { action = value; RaisePropertyChanged(); }
        }

        #endregion

        #region commands
        public DelegateCommand _saveCommand { get; set; }

        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(SaveAsync));
        #endregion

        private INavigationService navigationService;
        private IActionRepository actionRepository;
        private IResponsibleRepository responsibleRepository;

        public EditListResponsiblePageViewModel(INavigationService navigationService, IActionRepository actionRepository, IResponsibleRepository responsibleRepository) :
            base(navigationService)
        {
            this.navigationService = navigationService;
            this.actionRepository = actionRepository;
            this.responsibleRepository = responsibleRepository;
            Title = "Editar Responsáveis";
        }

        public async void SaveAsync()
        {
            Action.ResponsibleCollection = new ObservableCollection<ListEntity>(ResponsibleCollection.Where(x => x.Added == true));
            await actionRepository.AddOrUpdateAsync(Action, Action.Id);
            await NavigationService.GoBackAsync();
        }

        public async void BuscarResponsibleCollectionAsync()
        {
            try
            {
                ResponsibleCollection = new ObservableCollection<ListEntity>();

                IEnumerable<Responsible> responsiblees = await responsibleRepository.GetAllAsync();

                for (int i = 0; i < responsiblees.Count(); i++)
                {
                    if (Action.ResponsibleCollection?.FirstOrDefault(x => x.Id == responsiblees.ElementAt(i).Id) != null)
                    {
                        ResponsibleCollection.Add(new ListEntity
                        {
                            Id = responsiblees.ElementAt(i).Id,
                            Name = responsiblees.ElementAt(i).Name,
                            Added = true
                        });
                    }
                    else
                    {
                        ResponsibleCollection.Add(new ListEntity
                        {
                            Id = responsiblees.ElementAt(i).Id,
                            Name = responsiblees.ElementAt(i).Name,
                            Added = false
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters["action"] != null)
            {
                Action = parameters["action"] as ActionModel;
                BuscarResponsibleCollectionAsync();
            }

        }
    }
}
