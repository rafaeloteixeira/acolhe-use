using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace ifsp.acolheuse.mobile.Core.Domain
{
    public class ListaEntidade : BindableBase
    {
        private string id;
        private string nome;
        private bool adicionado;

        public string Id
        {
            get { return id; }
            set { id = value; RaisePropertyChanged(); }
        }
        public string Nome
        {
            get { return nome; }
            set { nome = value; RaisePropertyChanged(); }
        }
        public bool Adicionado
        {
            get { return adicionado; }
            set { adicionado = value; RaisePropertyChanged(); }
        }
    }
}
