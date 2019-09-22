using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using ifsp.acolheuse.mobile.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ifsp.acolheuse.mobile.ViewModels.Responsavel
{
    public class InclusaoProfessorInterconsultaPageViewModel : ViewModelBase
    {
        #region properties

        private Acao acao;
        private Paciente paciente;
        private int tipoConsulta;
        private IEnumerable<Acao> acoesCollection;

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
        public IEnumerable<Acao> AcoesCollection
        {
            get { return acoesCollection; }
            set { acoesCollection = value; RaisePropertyChanged(); }
        }
        #endregion

        private INavigationService navigationService;
        private IPacienteRepository pacienteRepository;
        private IAcaoRepository acaoRepository;

        public InclusaoProfessorInterconsultaPageViewModel(INavigationService navigationService, IPacienteRepository pacienteRepository, IAcaoRepository acaoRepository) :
          base(navigationService)
        {
            this.navigationService = navigationService;
            this.pacienteRepository = pacienteRepository;
            this.acaoRepository = acaoRepository;

        }


        public async void EnviarInterconsultaAsync(Acao acaoDestino)
        {
            if (acaoDestino != null)
            {

                if (await MessageService.Instance.ShowAsyncYesNo("Deseja promover este usuário a interconsulta a ação " + acaoDestino.Nome + "?"))
                {
                    PromoverInterconsulta(acaoDestino);
                }
            }
        }
        public async void PromoverInterconsulta(Acao acaoDestino)
        {
            if (Paciente.AcoesCollection.FirstOrDefault(x => x.Id == acaoDestino.Id) == null)
            {
                Paciente.AcoesCollection.Add(new Lista
                {
                    Id = acaoDestino.Id,
                    Nome = acaoDestino.Nome,
                    Adicionado = true,
                    IsAlta = false,
                    IsAtendimento = false,
                    IsListaEspera = false,
                    IsInterconsulta = true
                });

                await pacienteRepository.AddOrUpdateAsync(Paciente, Paciente.Id);
                await navigationService.GoBackAsync();
            }
        }

        public async void BuscarAcoesCollectionAsync()
        {
            AcoesCollection = (await acaoRepository.GetAllAsync()).Where( x=>x.Id != Acao.Id);
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
        }
    }
}
