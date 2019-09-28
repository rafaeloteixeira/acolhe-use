using Plugin.CloudFirestore.Attributes;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Core.Domain
{
    public class HorarioAcao : BindableBase
    {
        private string id;
        private string idAcao;
        private DateTime startTime;
        private DateTime endTime;

        [Ignored]
        public Color Cor
        {
            get
            {
                return Color.FromHex("#808080");
            }
        }
        [Id]
        public string Id
        {
            get { return id; }
            set { id = value; RaisePropertyChanged(); }
        }
        public string EventId
        {
            get { return idAcao; }
            set { idAcao = value; RaisePropertyChanged(); }
        }

        public string IdAcao
        {
            get { return idAcao; }
            set { idAcao = value; RaisePropertyChanged(); }
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
        public HorarioAcao()
        {
           
        }
        public HorarioAcao(string idAcao, DateTime? Date)
        {
            this.IdAcao = idAcao;
            StartTime = new DateTime(Date.Value.Year, Date.Value.Month, Date.Value.Day, Date.Value.Hour, 0, 0);
            EndTime = StartTime.AddMinutes(59).AddSeconds(59);
            EventId = StartTime.ToString("yyyyMMddhhmm") + EndTime.ToString("yyyyMMddhhmm");
        }
    }
}
