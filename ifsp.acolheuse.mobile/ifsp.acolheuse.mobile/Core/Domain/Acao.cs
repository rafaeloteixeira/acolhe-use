using Plugin.CloudFirestore.Attributes;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ifsp.acolheuse.mobile.Core.Domain
{
    public class Acao : BindableBase
    {
        private string id;
        private string guidAcao;
        private string nome;
        private int numeroVagas;
        private int isListaAberta;
        private bool isOrientation;
        private bool isIndividual;
        private bool isGroup;
        private string idLinha;
        private DiasAcao dias;
        private IEnumerable<Lista> responsavelCollection;
        private IEnumerable<Lista> estagiarioCollection;

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
        public string GuidAcao
        {
            get { return guidAcao; }
            set { guidAcao = value; RaisePropertyChanged(); }
        }
        public int NumeroVagas
        {
            get { return numeroVagas; }
            set { numeroVagas = value; RaisePropertyChanged(); }
        }
        public int IsListaAberta
        {
            get { return isListaAberta; }
            set { isListaAberta = value; RaisePropertyChanged(); }
        }
        public bool IsOrientation
        {
            get { return isOrientation; }
            set { isOrientation = value; RaisePropertyChanged(); }
        }
        public bool IsIndividual
        {
            get { return isIndividual; }
            set { isIndividual = value; RaisePropertyChanged(); }
        }
        public bool IsGroup
        {
            get { return isGroup; }
            set { isGroup = value; RaisePropertyChanged(); }
        }
        public string IdLinha
        {
            get { return idLinha; }
            set { idLinha = value; RaisePropertyChanged(); }
        }

        public DiasAcao Dias
        {
            get { return dias; }
            set { dias = value; RaisePropertyChanged(); }
        }
        public IEnumerable<Lista> ResponsavelCollection
        {
            get { return responsavelCollection; }
            set { responsavelCollection = value; RaisePropertyChanged(); }
        }
        public IEnumerable<Lista> EstagiarioCollection
        {
            get { return estagiarioCollection; }
            set { estagiarioCollection = value; RaisePropertyChanged(); }
        }

        public Acao()
        {
            Dias = new DiasAcao();
        }
    }
}
