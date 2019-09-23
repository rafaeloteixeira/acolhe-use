using Newtonsoft.Json;
using Plugin.CloudFirestore.Attributes;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace ifsp.acolheuse.mobile.Core.Domain
{
    public class Servidor : BindableBase
    {
        private string id;
        private string localId;
        private string cpf;
        private string nome;
        private string sobrenome;
        private string telefone;
        private string celular;
        private string email;
        private string senha;
        private string confirmarSenha;
        private bool isProfessor;

        [Id]
        public string Id
        {
            get { return id; }
            set { id = value; RaisePropertyChanged(); }
        }
        public string LocalId
        {
            get { return localId; }
            set { localId = value; RaisePropertyChanged(); }
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
        public string Email
        {
            get { return email; }
            set { email = value; RaisePropertyChanged(); }
        }

        [Ignored]
        public string Senha
        {
            get { return senha; }
            set { senha = value; RaisePropertyChanged(); }
        }

        [Ignored]
        public string ConfirmarSenha
        {
            get { return confirmarSenha; }
            set { confirmarSenha = value; RaisePropertyChanged(); }
        }
        public bool IsProfessor
        {
            get { return isProfessor; }
            set { isProfessor = value; RaisePropertyChanged(); }
        }
    }
}
