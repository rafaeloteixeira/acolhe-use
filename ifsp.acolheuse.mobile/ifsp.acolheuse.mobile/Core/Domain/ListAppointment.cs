using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace ifsp.acolheuse.mobile.Core.Domain
{
    public class ListAppointment : BindableBase
    {
        private string id;
        private string professorIdFrom;
        private string name;
        private bool added;
        private bool isListWaiting;
        private bool isAppointment;
        private bool isRelease;
        private bool isInterconsultationtion;

        public string Id
        {
            get { return id; }
            set { id = value; RaisePropertyChanged(); }
        }
        public string ProfessorIdFrom
        {
            get { return professorIdFrom; }
            set { professorIdFrom = value; RaisePropertyChanged(); }
        }
        public string Name
        {
            get { return name; }
            set { name = value; RaisePropertyChanged(); }
        }
        public bool Added
        {
            get { return added; }
            set { added = value; RaisePropertyChanged(); }
        }
        public bool IsListWaiting
        {
            get { return isListWaiting; }
            set { isListWaiting = value; RaisePropertyChanged(); }
        }
        public bool IsAppointment
        {
            get { return isAppointment; }
            set { isAppointment = value; RaisePropertyChanged(); }
        }
        public bool IsRelease
        {
            get { return isRelease; }
            set { isRelease = value; RaisePropertyChanged(); }
        }
        public bool IsInterconsultationtion
        {
            get { return isInterconsultationtion; }
            set { isInterconsultationtion = value; RaisePropertyChanged(); }
        }
    }
}
