using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ifsp.acolheuse.mobile.ViewModels.Acolhimento
{
    public class InclusaoAcaoPageViewModel : ViewModelBase
    {

        #region commands
        public DelegateCommand _salvarCommand { get; set; }
        public DelegateCommand SalvarCommand => _salvarCommand ?? (_salvarCommand = new DelegateCommand(SalvarAsync));
        #endregion

        #region properties
        private Paciente paciente;
        private ObservableCollection<ListaAtendimento> acoesCollection;

        public Paciente Paciente
        {
            get { return paciente; }
            set { paciente = value; RaisePropertyChanged(); }
        }
        public ObservableCollection<ListaAtendimento> AcoesCollection
        {
            get { return acoesCollection; }
            set { acoesCollection = value; RaisePropertyChanged(); }
        }
        #endregion

        private INavigationService navigationService;
        private IPacienteRepository pacienteRepository;
        private IAcaoRepository acaoRepository;

        public InclusaoAcaoPageViewModel(INavigationService navigationService, IPacienteRepository pacienteRepository, IAcaoRepository acaoRepository) :
        base(navigationService)
        {
            this.navigationService = navigationService;
            this.pacienteRepository = pacienteRepository;
            this.acaoRepository = acaoRepository;
        }

        public async void SalvarAsync()
        {
            Paciente.AcoesCollection = new ObservableCollection<ListaAtendimento>(AcoesCollection.Where(x => x.Adicionado == true));
            await navigationService.GoBackAsync();
        }

        public async void BuscarAcoesCollectionAsync()
        {
            AcoesCollection = new ObservableCollection<ListaAtendimento>();
            var acoes = await acaoRepository.GetAllAsync();

            for (int i = 0; i < acoes.Count(); i++)
            {
                if (Paciente.AcoesCollection?.FirstOrDefault(x => x.Id == acoes.ElementAt(i).Id) != null)
                {
                    AcoesCollection.Add(new ListaAtendimento
                    {
                        Id = acoes.ElementAt(i).Id,
                        Nome = acoes.ElementAt(i).Nome,
                        Adicionado = true,
                        IsAlta = false,
                        IsAtendimento = false,
                        IsInterconsulta = false,
                        IsListaEspera = true
                    });
                }
                else
                {
                    AcoesCollection.Add(new ListaAtendimento
                    {
                        Id = acoes.ElementAt(i).Id,
                        Nome = acoes.ElementAt(i).Nome,
                        Adicionado = false,
                        IsAlta = false,
                        IsAtendimento = false,
                        IsInterconsulta = false,
                        IsListaEspera = true
                    });
                }
            }
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters["paciente"] != null)
            {
                Paciente = parameters["paciente"] as Paciente;
            }
            else
            {
                Paciente = new Paciente();
            }
        }
    }
    
}
