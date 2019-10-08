using Newtonsoft.Json;
using Plugin.CloudFirestore.Attributes;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Core.Domain
{
    public class Appointment : BindableBase
    {
        public const int _ORIENTACAO = 0;
        public const int _GRUPO = 1;
        public const int _INDIVIDUAL = 2;
        public const int _INTERCONSULTA = 3;
        public const int _INCLUIDO = 100;

        private string id;
        private int consultationType;
        private string eventName;
        private string idResponsible;
        private ListEntity patient;
        private string idAction;
        private int capacity;
        private DateTime startTime;
        private DateTime endTime;
        private bool allDay;
        private bool repeat;
        private bool confirmed;
        private bool canceled;
        private IEnumerable<string> internsIdCollection;

        [Id]
        public string Id
        {
            get { return id; }
            set { id = value; RaisePropertyChanged(); }
        }
        public int ConsultationType
        {
            get { return consultationType; }
            set { consultationType = value; RaisePropertyChanged(); }
        }

        [Ignored]
        public Color Cor
        {
            get
            {
                switch (ConsultationType)
                {
                    case _ORIENTACAO:
                        return Color.Orange;
                    case _GRUPO:
                        return Color.Green;
                    case _INDIVIDUAL:
                        return Color.Blue;
                    case _INCLUIDO:
                        return Color.FromHex("#7AFF9A");
                    default:
                        return Color.Black;
                }
            }

        }

        [Ignored]
        public string DescricaoConsultation
        {
            get
            {
                switch (ConsultationType)
                {
                    case _ORIENTACAO:
                        return patient.Name;
                    case _GRUPO:
                        return "Grupo";
                    case _INDIVIDUAL:
                        return patient.Name;
                    default:
                        return "";
                }
            }
        }

        [Ignored]
        public string EventId
        {
            get
            {
                switch (ConsultationType)
                {
                    case _INDIVIDUAL:
                    case _GRUPO:
                        return StartTime.DayOfWeek + StartTime.ToString("hhmm") + EndTime.ToString("hhmm") + "-" + ConsultationType;
                    default:
                        return StartTime.ToString("yyyyMMddhhmm") + EndTime.ToString("yyyyMMddhhmm") + "-" + ConsultationType;
                }

            }
        }
        public string EventName
        {
            get { return eventName; }
            set { eventName = value; RaisePropertyChanged(); }
        }
        public string IdResponsible
        {
            get { return idResponsible; }
            set { idResponsible = value; RaisePropertyChanged(); }
        }
        public ListEntity Patient
        {
            get { return patient; }
            set { patient = value; RaisePropertyChanged(); }
        }
        public string IdAction
        {
            get { return idAction; }
            set { idAction = value; RaisePropertyChanged(); }
        }
        public int Capacity
        {
            get { return capacity; }
            set { capacity = value; RaisePropertyChanged(); }
        }
        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; RaisePropertyChanged(); }
        }
        public DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; RaisePropertyChanged(); }
        }

        public bool AllDay
        {
            get { return allDay; }
            set { allDay = value; RaisePropertyChanged(); }
        }
        public bool Repeat
        {
            get { return repeat; }
            set { repeat = value; RaisePropertyChanged(); }
        }
        public bool Confirmed
        {
            get { return confirmed; }
            set { confirmed = value; RaisePropertyChanged(); }
        }
        public bool Canceled
        {
            get { return canceled; }
            set { canceled = value; RaisePropertyChanged(); }
        }
        public IEnumerable<string> InternsIdCollection
        {
            get { return internsIdCollection; }
            set { internsIdCollection = value; RaisePropertyChanged(); }
        }
        public Appointment()
        {

        }

        public Appointment(int consultationType, string eventName, string idResponsible, ListEntity patient, string idAction, int capacity, bool allDay, bool repeat, DateTime? Date, IEnumerable<string> InternsIdCollection)
        {
            this.EventName = eventName;
            this.IdResponsible = idResponsible;
            this.Patient = patient;
            this.IdAction = idAction;
            this.Capacity = capacity;
            this.AllDay = allDay;
            this.Repeat = repeat;
            this.ConsultationType = consultationType;
            this.InternsIdCollection = InternsIdCollection;

            StartTime = new DateTime(Date.Value.Year, Date.Value.Month, Date.Value.Day, Date.Value.Hour, 0, 0);
            EndTime = StartTime.AddMinutes(59).AddSeconds(59);
        }
    }
}
