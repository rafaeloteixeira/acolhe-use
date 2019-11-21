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
    public class EditListInternsPageViewModel : ViewModelBase
    {
        #region properties
        private ObservableCollection<ListEntity> internCollection;
        private ActionModel action;

        public ObservableCollection<ListEntity> InternCollection
        {
            get { return internCollection; }
            set { internCollection = value; RaisePropertyChanged(); }
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
        private IInternRepository internRepository;

        public EditListInternsPageViewModel(INavigationService navigationService, IActionRepository actionRepository, IInternRepository internRepository) :
            base(navigationService)
        {
            this.navigationService = navigationService;
            this.actionRepository = actionRepository;
            this.internRepository = internRepository;
            Title = "Editar Estagiários";
        }

        public async void SaveAsync()
        {
            Action.InternCollection = new ObservableCollection<ListEntity>(InternCollection.Where(x => x.Added == true));
            //await actionRepository.AddOrUpdateAsync(Action, Action.Id);
            await NavigationService.GoBackAsync();
        }

        public async void BuscarInternsCollectionAsync()
        {
            try
            {
                InternCollection = new ObservableCollection<ListEntity>();
                IEnumerable<Intern> interns = await internRepository.GetAllAsync();

                for (int i = 0; i < interns.Count(); i++)
                {
                    if (Action.InternCollection?.FirstOrDefault(x => x.Id == interns.ElementAt(i).Id) != null)
                    {
                        InternCollection.Add(new ListEntity
                        {
                            Id = interns.ElementAt(i).Id,
                            Name = interns.ElementAt(i).Name,
                            Added = true
                        });
                    }
                    else
                    {
                        InternCollection.Add(new ListEntity
                        {
                            Id = interns.ElementAt(i).Id,
                            Name = interns.ElementAt(i).Name,
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
                BuscarInternsCollectionAsync();
            }
        }
    }
}
