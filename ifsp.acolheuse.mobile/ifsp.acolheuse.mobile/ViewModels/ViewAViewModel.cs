using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using ifsp.acolheuse.mobile.Persistence.Repositories;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;

namespace ifsp.acolheuse.mobile.ViewModels
{
    public class ViewAViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        private IAcaoRepository acaoRepository;
        private DelegateCommand _navigateCommand;

        public DelegateCommand NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand(ExecuteNavigateCommand));

        public ViewAViewModel(INavigationService navigationService, IAcaoRepository acaoRepository) :
            base(navigationService)
        {
            this.navigationService = navigationService;
            this.acaoRepository = acaoRepository;

            Title = "My View A";
        }

        async void ExecuteNavigateCommand()
        {

            try
            {
                await acaoRepository.AddAsync(new Acao { Name = "tester" });
                var listAcao = await acaoRepository.GetAllAsync();

                foreach (var item in listAcao)
                {
                    Console.WriteLine("lista: " + item.Name);
                }
            }
            catch (System.Exception e)
            {

            }



            await navigationService.NavigateAsync("ViewB");
        }

    }
}
