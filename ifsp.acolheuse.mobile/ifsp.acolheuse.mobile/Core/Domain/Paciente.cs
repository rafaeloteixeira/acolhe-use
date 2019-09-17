using System;
using System.Collections.Generic;
using System.Text;

namespace ifsp.acolheuse.mobile.Core.Domain
{
    public class Paciente
    {
        private string cpf;
        private string nome;
        private string sobrenome;
        private string email;
        private string telefone;
        private string celular;
        private ObservableCollection<IncluirModel> acoesCollection;

        public string Id
        {
            get { return String.IsNullOrEmpty(cpf) ? "" : cpf.Replace("-", "").Replace(".", ""); }
        }
        public string Cpf
        {
            get { return cpf; }
            set { cpf = value; OnPropertyChanged(); }
        }

        [Ignored]
        public string NomeCompleto
        {
            get { return nome + " " + sobrenome; }
        }
        public string Nome
        {
            get { return nome; }
            set { nome = value; OnPropertyChanged(); }
        }

        public string Sobrenome
        {
            get { return sobrenome; }
            set { sobrenome = value; OnPropertyChanged(); }
        }
        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged(); }
        }

        public string Telefone
        {
            get { return telefone; }
            set { telefone = value; OnPropertyChanged(); }
        }

        public string Celular
        {
            get { return celular; }
            set { celular = value; OnPropertyChanged(); }
        }

        public ObservableCollection<IncluirModel> AcoesCollection
        {
            get { return acoesCollection; }
            set { acoesCollection = value; OnPropertyChanged(); }
        }

        public PacienteModel()
        {
            AcoesCollection = new ObservableCollection<IncluirModel>();
        }
    }
}
