using Plugin.CloudFirestore.Attributes;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace ifsp.acolheuse.mobile.Core.Domain
{
    public class CheckOut: BindableBase
    {
        private string id;
        private string appointmentId;
        private DateTime date;
        private bool confirmed;

        [Id]
        public string Id
        {
            get { return id; }
            set { id = value; RaisePropertyChanged(); }
        }
        public string AppointmentId
        {
            get { return appointmentId; }
            set { appointmentId = value; RaisePropertyChanged(); }
        }
        public DateTime Date
        {
            get { return date; }
            set { date = value; RaisePropertyChanged(); }
        }
        public bool Confirmed
        {
            get { return confirmed; }
            set { confirmed = value; RaisePropertyChanged(); }
        }
    }
}
