using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using ifsp.acolheuse.mobile.Core.Settings;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ifsp.acolheuse.mobile.ViewModels
{
    public class EstagiarioComparecimentoPageViewModel : ViewModelBase
    {
        private ObservableCollection<Atendimento> atendimentoCollection;

        public ObservableCollection<Atendimento> AtendimentoCollection
        {
            get { return atendimentoCollection; }
            set { atendimentoCollection = value; RaisePropertyChanged(); }
        }

        INavigationService navigationService;
        IEstagiarioRepository estagiarioRepository;
        IAtendimentoRepository atendimentoRepository;

        public EstagiarioComparecimentoPageViewModel(INavigationService navigationService, IEstagiarioRepository estagiarioRepository, IAtendimentoRepository atendimentoRepository) : base(navigationService)
        {
            this.navigationService = navigationService;
            this.estagiarioRepository = estagiarioRepository;
            this.atendimentoRepository = atendimentoRepository;
        }


        public async void BuscarAtendimentosAsync()
        {
            AtendimentoCollection = new ObservableCollection<Atendimento>();

            Estagiario Estagiario = await estagiarioRepository.GetAsync(Settings.UserId);

            var atendimentos = await atendimentoRepository.GetAllByEstagiarioId(Estagiario.Id);
        }
    }
}
