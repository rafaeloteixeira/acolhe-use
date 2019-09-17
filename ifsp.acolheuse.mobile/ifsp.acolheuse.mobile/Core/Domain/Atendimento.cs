using Newtonsoft.Json;
using Plugin.CloudFirestore.Attributes;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Core.Domain
{
    public class Atendimento : BindableBase
    {
        public const int _ORIENTACAO = 0;
        public const int _GRUPO = 1;
        public const int _INDIVIDUAL = 2;
        public const int _INTERCONSULTA = 3;
        public const int _INCLUIDO = 100;

        private int tipoConsulta;
        private string eventName;
        private string idServidor;
        private Paciente paciente;
        private string idAcao;
        private int capacidade;
        private DateTime startTime;
        private DateTime endTime;
        private bool allDay;
        private bool repeat;
        private bool confirmado;
        private bool cancelado;
        private IEnumerable<string> estagiariosIdCollection;

        public int TipoConsulta
        {
            get { return tipoConsulta; }
            set { tipoConsulta = value; RaisePropertyChanged(); }
        }

        [Ignored]
        public Color Cor
        {
            get
            {
                switch (TipoConsulta)
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
        public string DescricaoConsulta
        {
            get
            {
                switch (TipoConsulta)
                {
                    case _ORIENTACAO:
                        return paciente.NomeCompleto;
                    case _GRUPO:
                        return "Grupo";
                    case _INDIVIDUAL:
                        return paciente.NomeCompleto;
                    default:
                        return "";
                }
            }
        }

        [Id]
        public string EventId
        {
            get
            {
                switch (TipoConsulta)
                {
                    case _INDIVIDUAL:
                    case _GRUPO:
                        return StartTime.DayOfWeek + StartTime.ToString("hhmm") + EndTime.ToString("hhmm") + "-" + TipoConsulta;
                    default:
                        return StartTime.ToString("yyyyMMddhhmm") + EndTime.ToString("yyyyMMddhhmm") + "-" + TipoConsulta;
                }

            }
        }
        public string EventName
        {
            get { return eventName; }
            set { eventName = value; RaisePropertyChanged(); }
        }
        public string IdServidor
        {
            get { return idServidor; }
            set { idServidor = value; RaisePropertyChanged(); }
        }
        public Paciente Paciente
        {
            get { return paciente; }
            set { paciente = value; RaisePropertyChanged(); }
        }
        public string IdAcao
        {
            get { return idAcao; }
            set { idAcao = value; RaisePropertyChanged(); }
        }
        public int Capacidade
        {
            get { return capacidade; }
            set { capacidade = value; RaisePropertyChanged(); }
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
        public bool Confirmado
        {
            get { return confirmado; }
            set { confirmado = value; RaisePropertyChanged(); }
        }
        public bool Cancelado
        {
            get { return cancelado; }
            set { cancelado = value; RaisePropertyChanged(); }
        }
        public IEnumerable<string> EstagiariosIdCollection
        {
            get { return estagiariosIdCollection; }
            set { estagiariosIdCollection = value; RaisePropertyChanged(); }
        }
        public Atendimento()
        {

        }

        public Atendimento(int tipoConsulta, string eventName, string idServidor, Paciente paciente, string idAcao, int capacidade, bool allDay, bool repeat, DateTime? Date, IEnumerable<string> EstagiariosIdCollection)
        {
            this.EventName = eventName;
            this.IdServidor = idServidor;
            this.Paciente = paciente;
            this.IdAcao = idAcao;
            this.Capacidade = capacidade;
            this.AllDay = allDay;
            this.Repeat = repeat;
            this.TipoConsulta = tipoConsulta;
            this.EstagiariosIdCollection = EstagiariosIdCollection;

            StartTime = new DateTime(Date.Value.Year, Date.Value.Month, Date.Value.Day, Date.Value.Hour, 0, 0);
            EndTime = StartTime.AddMinutes(59).AddSeconds(59);
        }
    }
}
