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
        private ObservableCollection<ListaEntidade> estagiarioCollection;
        private Acao acao;
        private Paciente paciente;
        private int tipoConsulta;

        public ObservableCollection<ListaEntidade> EstagiarioCollection
        {
            get { return estagiarioCollection; }
            set { estagiarioCollection = value; RaisePropertyChanged(); }
        }
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
            var navParameters = new NavigationParameters();

            if (acao.EstagiarioCollection != null)
            {
                EstagiarioCollection = new ObservableCollection<ListaEntidade>(acao.EstagiarioCollection.Where(x => x.Adicionado));
                navParameters.Add("estagiarios", EstagiarioCollection);
            }
        
            navParameters.Add("paciente", Paciente);
            navParameters.Add("id_acao", Acao.Id);
            navParameters.Add("tipo_consulta", TipoConsulta);

            await navigationService.NavigateAsync("AgendaAtendimentoPage", navParameters);
        }

        public void BuscarEstagiariosCollection()
        {
            EstagiarioCollection = new ObservableCollection<ListaEntidade>();
            if(Acao.EstagiarioCollection != null)
            {
                for (int i = 0; i < Acao.EstagiarioCollection.Count(); i++)
                {
                    EstagiarioCollection.Add(new ListaEntidade
                    {
                        Id = Acao.EstagiarioCollection[i].Id,
                        Nome = Acao.EstagiarioCollection[i].Nome,
                        Adicionado = false
                    });
                }
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
                BuscarEstagiariosCollection();
            }
            if (parameters["tipo_consulta"] != null)
            {
                TipoConsulta = int.Parse(parameters["tipo_consulta"].ToString());
            }

        }
    }
}
