using Plugin.CloudFirestore.Attributes;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Core.Domain
{
    public class ScheduleAction : BindableBase
    {
        private string id;
        private string idAction;
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
            get { return idAction; }
            set { idAction = value; RaisePropertyChanged(); }
        }

        public string IdAction
        {
            get { return idAction; }
            set { idAction = value; RaisePropertyChanged(); }
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
        public ScheduleAction()
        {
           
        }
        public ScheduleAction(string idAction, DateTime? Date)
        {
            this.IdAction = idAction;
            StartTime = new DateTime(Date.Value.Year, Date.Value.Month, Date.Value.Day, Date.Value.Hour, 0, 0);
            EndTime = StartTime.AddMinutes(59).AddSeconds(59);
            EventId = StartTime.ToString("yyyyMMddhhmm") + EndTime.ToString("yyyyMMddhhmm");
        }
    }
}
