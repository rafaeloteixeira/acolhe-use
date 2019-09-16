using Plugin.CloudFirestore.Attributes;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace ifsp.acolheuse.mobile.Core.Domain
{
    public class Acao : BindableBase
    {
        private string id;
        private string name;
        private int numeroVagas;
        private int isOpen;
        private bool isOrientation;
        private bool isIndividual;
        private bool isGroup;

        [Id]
        public string Id
        {
            get { return id; }
            set { id = value; RaisePropertyChanged(); }
        }
        public string Name
        {
            get { return name; }
            set { name = value; RaisePropertyChanged(); }
        }
        public int NumeroVagas
        {
            get { return numeroVagas; }
            set { numeroVagas = value; RaisePropertyChanged(); }
        }
        public int IsOpen
        {
            get { return isOpen; }
            set { isOpen = value; RaisePropertyChanged(); }
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
    }
}
