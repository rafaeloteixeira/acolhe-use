using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ifsp.acolheuse.mobile.ViewModels.Responsavel
{
    public class InclusaoEstagiariosAtendimentoPageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _agendarCommand { get; set; }
        public DelegateCommand AgendarCommand => _agendarCommand ?? (_agendarCommand = new DelegateCommand(AgendarAsync));
        #endregion

        #region properties
        private Acao acao;
        private Paciente paciente;
        private int tipoConsulta;

        public Acao Acao
        {
            get { return acao; }
            set { acao = value; RaisePropertyChanged(); }
        }
        public Paciente Paciente
        {
            get { return paciente; }
            set { paciente = value; RaisePropertyChanged(); }
        }
        public int TipoConsulta
        {
            get { return tipoConsulta; }
            set { tipoConsulta = value; RaisePropertyChanged(); }
        }
        #endregion

        private INavigationService navigationService;
        private IEstagiarioRepository estagiarioRepository;
        public InclusaoEstagiariosAtendimentoPageViewModel(INavigationService navigationService, IEstagiarioRepository estagiarioRepository) :
          base(navigationService)
        {
            this.navigationService = navigationService;
            this.estagiarioRepository = estagiarioRepository;
        }


        public async void AgendarAsync()
        {
            Acao.EstagiarioCollection = new ObservableCollection<Lista>(acao.EstagiarioCollection.Where(x => x.Adicionado));

            var navParameters = new NavigationParameters();
            navParameters.Add("paciente", Paciente);
            navParameters.Add("acao", Acao);
            navParameters.Add("tipo_consulta", TipoConsulta);

            await navigationService.NavigateAsync("AgendaAtendimentoPage", navParameters);
        }

        public async void BuscarEstagiariosCollectionAsync()
        {
            Acao.EstagiarioCollection = new ObservableCollection<Lista>();
            var estagiarios = await estagiarioRepository.GetAllAsync();


            for (int i = 0; i < estagiarios.Count(); i++)
            {
                Acao.EstagiarioCollection.Add(new Lista
                {
                    Id = estagiarios.ElementAt(i).UserId,
                    Nome = estagiarios.ElementAt(i).Nome,
                    Adicionado = false
                });
            }
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters["paciente"] != null)
            {
                Paciente = parameters["paciente"] as Paciente;
            }
            if (parameters["acao"] != null)
            {
                Acao = parameters["acao"] as Acao;
            }
            if (parameters["tipo_consulta"] != null)
            {
                TipoConsulta = int.Parse(parameters["tipo_consulta"].ToString());
            }

        }
    }
}
