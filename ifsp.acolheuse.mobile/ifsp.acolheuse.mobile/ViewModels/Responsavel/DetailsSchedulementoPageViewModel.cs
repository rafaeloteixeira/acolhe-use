using ifsp.acolheuse.mobile.Core.Domain;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ifsp.acolheuse.mobile.ViewModels
{
    public class DetailsSchedulementoPageViewModel : ViewModelBase
    {
        #region properties


        private string consultationType;
        public string ConsultationType
        {
            get { return consultationType; }
            set { consultationType = value; RaisePropertyChanged(); }
        }
        private string schedule;
        public string Schedule
        {
            get { return schedule; }
            set { schedule = value; RaisePropertyChanged(); }
        }
        private string patient;
        public string Patient
        {
            get { return patient; }
            set { patient = value; RaisePropertyChanged(); }
        }
        private string interns;
        public string Interns
        {
            get { return interns; }
            set { interns = value; RaisePropertyChanged(); }
        }

        #endregion

        private INavigationService navigationService;
        public DetailsSchedulementoPageViewModel(INavigationService navigationService) :
          base(navigationService)
        {
            this.navigationService = navigationService;
        }


        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            int consultation = -1;
            if (parameters["interns"] != null)
            {
                Interns = string.Join(", ", (parameters["interns"] as IEnumerable<Intern>).Select(x => x.NameCompleto)); ;
            }

     
            if (parameters["type_consultation"] != null)
            {
                consultation = int.Parse(parameters["type_consultation"].ToString());
                switch (int.Parse(parameters["type_consultation"].ToString()))
                {
                    case Appointment._ORIENTACAO:
                        ConsultationType = "Orientação";
                        break;
                    case Appointment._INDIVIDUAL:
                        ConsultationType = "Individual";
                        break;
                    case Appointment._GRUPO:
                        ConsultationType = "Grupo";
                        break;
                }

            }
            if (parameters["patient"] != null)
            {
                if (consultation == Appointment._ORIENTACAO)
                {
                    Patient = (parameters["patient"] as ListEntity).Name;
                }
                else
                {
                    Patient = "";
                }
            }
            if (parameters["schedule"] != null)
            {
                if (consultation == Appointment._ORIENTACAO)
                {
                    DateTime date = ((DateTime)parameters["schedule"]);
                    Schedule = date.ToString("dd/MM/yyyy - hh:mm");
                }
                else
                {
                    DateTime date = ((DateTime)parameters["schedule"]);

                    var culture = new System.Globalization.CultureInfo("pt-BR");
                    var day = culture.DateTimeFormat.GetDayName(date.DayOfWeek);
                    Schedule = day + " - " + date.ToString("hh:mm");
                }
            }


        }
    }
}
