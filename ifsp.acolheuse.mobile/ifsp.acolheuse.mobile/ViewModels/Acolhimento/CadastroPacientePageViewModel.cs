﻿using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ifsp.acolheuse.mobile.ViewModels
{
    public class CadastroPacientePageViewModel : ViewModelBase
    {
        #region commands
        public DelegateCommand _salvarPacienteCommand { get; set; }
        public DelegateCommand _editarListaAcoes { get; set; }
        public DelegateCommand SalvarPacienteCommand => _salvarPacienteCommand ?? (_salvarPacienteCommand = new DelegateCommand(SalvarPacienteAsync));
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
        private IPacienteRepository pacienteRepository;
        private IAcaoRepository acaoRepository;
        public CadastroPacientePageViewModel(INavigationService navigationService, IPacienteRepository pacienteRepository, IAcaoRepository acaoRepository) :
        base(navigationService)
        {
            this.navigationService = navigationService;
            this.pacienteRepository = pacienteRepository;
        }
    
        public async void SalvarPacienteAsync()
        {
            await pacienteRepository.AddAsync(Paciente);
            await navigationService.GoBackAsync();
        }
        public async void EditarAcoesAsync()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("paciente", Paciente);
            await navigationService.NavigateAsync("IncluirAcaoPage", navParameters);
        }

        public async void GetPacienteAsync()
        {
            Paciente = await pacienteRepository.GetAsync(Paciente.Id);
        }

        public async void ExcluirPacienteAsync(string id)
        {
            await pacienteRepository.RemoveAsync(id);
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
