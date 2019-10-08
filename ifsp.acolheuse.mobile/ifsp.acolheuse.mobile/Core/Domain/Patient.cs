using Plugin.CloudFirestore.Attributes;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ifsp.acolheuse.mobile.Core.Domain
{
    public class Patient : BindableBase
    {
        private string id;
        private string cpf;
        private string name;
        private string lastName;
        private string email;
        private string phone;
        private string cellphone;
        private ObservableCollection<ListAppointment> actionCollection;

        [Id]
        public string Id
        {
            get { return id; }
            set { id = value; RaisePropertyChanged(); }
        }
        public string Cpf
        {
            get { return cpf; }
            set { cpf = value; RaisePropertyChanged(); }
        }

        [Ignored]
        public string NameCompleto
        {
            get { return name + " " + lastName; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; RaisePropertyChanged(); }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; RaisePropertyChanged(); }
        }
        public string Email
        {
            get { return email; }
            set { email = value; RaisePropertyChanged(); }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; RaisePropertyChanged(); }
        }

        public string Cellphone
        {
            get { return cellphone; }
            set { cellphone = value; RaisePropertyChanged(); }
        }

        public ObservableCollection<ListAppointment> ActionCollection
        {
            get { return actionCollection; }
            set { actionCollection = value; RaisePropertyChanged(); }
        }

        public Patient()
        {
        }
    }
}
