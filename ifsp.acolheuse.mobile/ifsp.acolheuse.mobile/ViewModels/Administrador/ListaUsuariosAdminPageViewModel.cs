using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using ifsp.acolheuse.mobile.Core.Repositories;
using Prism.Navigation;
using ifsp.acolheuse.mobile.Core.Domain;

namespace ifsp.acolheuse.mobile.ViewModels.Administrador
{
    public class ListaUsuariosAdminPageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _novoPacienteCommand { get; set; }
        public DelegateCommand NovoPacienteCommand => _novoPacienteCommand ?? (_novoPacienteCommand = new DelegateCommand(NovoPacienteCommandAsync));
        #endregion

        #region properties
        private IEnumerable<Paciente> pacienteCollection;
        public IEnumerable<Paciente> PacienteCollection
        {
            get { return pacienteCollection; }
            set { pacienteCollection = value; RaisePropertyChanged(); }
        }

        #endregion

        private INavigationService navigationService;
        private IPacienteRepository pacienteRepository;

        public ListaUsuariosAdminPageViewModel(INavigationService navigationService, IPacienteRepository pacienteRepository) :
            base(navigationService)
        {
            this.navigationService = navigationService;
            this.pacienteRepository = pacienteRepository;
            Title = "My View A";
        }

        internal async void ItemTapped(Paciente paciente)
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("paciente", paciente);
            await navigationService.NavigateAsync("CadastroPacientePage", navParameters);
        }

        public async void NovoPacienteCommandAsync()
        {
            await navigationService.NavigateAsync("CadastroPacientePage");
        }

        public async void BuscarPacientesCollectionAsync()
        {
            try
            {
                PacienteCollection = await pacienteRepository.GetAllAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
