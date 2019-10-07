using ifsp.acolheuse.mobile.Core.Domain;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ifsp.acolheuse.mobile.ViewModels.Responsavel
{
    public class DetalhesAgendamentoPageViewModel : ViewModelBase
    {
        #region properties


        private string tipoConsulta;
        public string TipoConsulta
        {
            get { return tipoConsulta; }
            set { tipoConsulta = value; RaisePropertyChanged(); }
        }
        private string horario;
        public string Horario
        {
            get { return horario; }
            set { horario = value; RaisePropertyChanged(); }
        }
        private string usuario;
        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; RaisePropertyChanged(); }
        }
        private string estagiarios;
        public string Estagiarios
        {
            get { return estagiarios; }
            set { estagiarios = value; RaisePropertyChanged(); }
        }

        #endregion

        private INavigationService navigationService;
        public DetalhesAgendamentoPageViewModel(INavigationService navigationService) :
          base(navigationService)
        {
            this.navigationService = navigationService;
        }


        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            int consulta = -1;
            if (parameters["estagiarios"] != null)
            {
                Estagiarios = string.Join(", ", (parameters["estagiarios"] as IEnumerable<Estagiario>).Select(x => x.NomeCompleto)); ;
            }

     
            if (parameters["tipo_consulta"] != null)
            {
                consulta = int.Parse(parameters["tipo_consulta"].ToString());
                switch (int.Parse(parameters["tipo_consulta"].ToString()))
                {
                    case Atendimento._ORIENTACAO:
                        TipoConsulta = "Orientação";
                        break;
                    case Atendimento._INDIVIDUAL:
                        TipoConsulta = "Individual";
                        break;
                    case Atendimento._GRUPO:
                        TipoConsulta = "Grupo";
                        break;
                }

            }
            if (parameters["usuario"] != null)
            {
                if (consulta == Atendimento._ORIENTACAO)
                {
                    Usuario = (parameters["usuario"] as ListaEntidade).Nome;
                }
                else
                {
                    Usuario = "";
                }
            }
            if (parameters["horario"] != null)
            {
                if (consulta == Atendimento._ORIENTACAO)
                {
                    DateTime date = ((DateTime)parameters["horario"]);
                    Horario = date.ToString("dd/MM/yyyy - hh:mm");
                }
                else
                {
                    DateTime date = ((DateTime)parameters["horario"]);

                    var culture = new System.Globalization.CultureInfo("pt-BR");
                    var day = culture.DateTimeFormat.GetDayName(date.DayOfWeek);
                    Horario = day + " - " + date.ToString("hh:mm");
                }
            }


        }
    }
}
