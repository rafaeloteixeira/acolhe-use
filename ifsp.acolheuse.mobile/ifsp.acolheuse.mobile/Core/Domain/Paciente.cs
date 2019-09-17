using Plugin.CloudFirestore.Attributes;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace ifsp.acolheuse.mobile.Core.Domain
{
    public class Paciente : BindableBase
    {
        private string cpf;
        private string nome;
        private string sobrenome;
        private string email;
        private string telefone;
        private string celular;
        private IEnumerable<Lista> acoesCollection;

        public string Id
        {
            get { return String.IsNullOrEmpty(cpf) ? "" : cpf.Replace("-", "").Replace(".", ""); }
        }
        public string Cpf
        {
            get { return cpf; }
            set { cpf = value; RaisePropertyChanged(); }
        }

        [Ignored]
        public string NomeCompleto
        {
            get { return nome + " " + sobrenome; }
        }
        public string Nome
        {
            get { return nome; }
            set { nome = value; RaisePropertyChanged(); }
        }

        public string Sobrenome
        {
            get { return sobrenome; }
            set { sobrenome = value; RaisePropertyChanged(); }
        }
        public string Email
        {
            get { return email; }
            set { email = value; RaisePropertyChanged(); }
        }

        public string Telefone
        {
            get { return telefone; }
            set { telefone = value; RaisePropertyChanged(); }
        }

        public string Celular
        {
            get { return celular; }
            set { celular = value; RaisePropertyChanged(); }
        }

        public IEnumerable<Lista> AcoesCollection
        {
            get { return acoesCollection; }
            set { acoesCollection = value; RaisePropertyChanged(); }
        }

        public Paciente()
        {
        }
    }
}
