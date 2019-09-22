using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ifsp.acolheuse.mobile.ViewModels.Responsavel
{
    public class UsuarioServidorPageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _salvarPacienteCommand { get; set; }
        public DelegateCommand SalvarPacienteCommand => _salvarPacienteCommand ?? (_salvarPacienteCommand = new DelegateCommand(SalvarPacienteAsync));
        public DelegateCommand _editarListaAcoes { get; set; }
        public DelegateCommand EditarListaAcoes => _editarListaAcoes ?? (_editarListaAcoes = new DelegateCommand(EditarAcoesAsync));
        #endregion

        #region properties
        private Paciente paciente;

        public Paciente Paciente
        {
            get { return paciente; }
            set { paciente = value; RaisePropertyChanged(); }
        }
        #endregion

        private INavigationService navigationService;
        private IServidorRepository servidorRepository;
        private IPacienteRepository pacienteRepository;
        public UsuarioServidorPageViewModel(INavigationService navigationService, IServidorRepository servidorRepository, IPacienteRepository pacienteRepository) :
          base(navigationService)
        {
            this.navigationService = navigationService;
            this.servidorRepository = servidorRepository;
            this.pacienteRepository = pacienteRepository;
        }

        public async void SalvarPacienteAsync()
        {
            await pacienteRepository.AddOrUpdateAsync(Paciente, Paciente.Id);
            GetPacienteAsync();
        }
        public async void EditarAcoesAsync()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("paciente", Paciente);
            await navigationService.NavigateAsync("InclusaoAcaoPage", navParameters);
        }

        public async void GetPacienteAsync()
        {
            Paciente = await pacienteRepository.GetAsync(Paciente.Id);
        }

        public async void ExcluirPacienteAsync(string Key)
        {
            await pacienteRepository.RemoveAsync(Key);
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters["paciente"] != null)
            {
                Paciente = parameters["paciente"] as Paciente;
                GetPacienteAsync();
            }
            else
            {
                Paciente = new Paciente();
            }
        }
    }
}
