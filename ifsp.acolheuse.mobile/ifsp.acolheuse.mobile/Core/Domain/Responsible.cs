using Newtonsoft.Json;
using Plugin.CloudFirestore.Attributes;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace ifsp.acolheuse.mobile.Core.Domain
{
    public class Responsible : BindableBase
    {
        private string id;
        private string accessToken;
        private string cpf;
        private string name;
        private string lastName;
        private string phone;
        private string cellphone;
        private string email;
        private string password;
        private string confirmPassword;
        private bool isProfessor;

        [Id]
        public string Id
        {
            get { return id; }
            set { id = value; RaisePropertyChanged(); }
        }
        public string AccessToken
        {
            get { return accessToken; }
            set { accessToken = value; RaisePropertyChanged(); }
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
        public string Email
        {
            get { return email; }
            set { email = value; RaisePropertyChanged(); }
        }

        [Ignored]
        public string Password
        {
            get { return password; }
            set { password = value; RaisePropertyChanged(); }
        }

        [Ignored]
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set { confirmPassword = value; RaisePropertyChanged(); }
        }
        public bool IsProfessor
        {
            get { return isProfessor; }
            set { isProfessor = value; RaisePropertyChanged(); }
        }
    }
}
