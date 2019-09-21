using Newtonsoft.Json;
using Plugin.CloudFirestore.Attributes;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace ifsp.acolheuse.mobile.Core.Domain
{
    public class Estagiario : BindableBase
    {
        private string id;
        private string userId;
        private string ra;
        private string nome;
        private string sobrenome;
        private string telefone;
        private string celular;
        private string email;
        private string senha;
        private string confirmarSenha;
        private string idProfessor;

        [Id]
        public string Id
        {
            get { return id; }
            set { id = value; RaisePropertyChanged(); }
        }
        public string UserId
        {
            get { return userId; }
            set { userId = value; RaisePropertyChanged(); }
        }
        public string Ra
        {
            get { return ra; }
            set { ra = value; RaisePropertyChanged(); }
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

        public string IdProfessor
        {
            get { return idProfessor; }
            set { idProfessor = value; RaisePropertyChanged(); }
        }
    }
}
