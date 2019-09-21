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

                Acao acao = new Acao { Id = "rXb2YcCflgr3FCXSfGfd" };
                var navParameters = new NavigationParameters();
                navParameters.Add("acao", acao);
                await navigationService.NavigateAsync("CadastroAcaoPage", navParameters);

            }
            catch (Exception ex)
            {
            }

            //try
            //{
            //    await acaoRepository.AddOrUpdateAsync(new Acao { Nome = "tester" });
            //    var listAcao = await acaoRepository.GetAllAsync();
            //    var acao = await acaoRepository.GetAsync("D21WkMt3zoTbL1OWdKx4");

            //    foreach (var item in listAcao)
            //    {
            //        Console.WriteLine("lista: " + item.Nome);
            //    }
            //}
            //catch (System.Exception e)
            //{

            //}



            //await navigationService.NavigateAsync("ViewB");
        }

    }
}
