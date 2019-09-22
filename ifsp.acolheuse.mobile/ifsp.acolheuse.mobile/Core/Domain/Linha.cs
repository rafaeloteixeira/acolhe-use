using Plugin.CloudFirestore.Attributes;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ifsp.acolheuse.mobile.Core.Domain
{
    public class Linha : BindableBase
    {
        private string id;
        private string nome;
        private ObservableCollection<Acao> acaoCollection;

        [Id]
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
        public ObservableCollection<Acao> AcaoCollection
        {
            get { return acaoCollection; }
            set { acaoCollection = value; RaisePropertyChanged(); }
        }

        public Linha()
        {
        }
    }
}
