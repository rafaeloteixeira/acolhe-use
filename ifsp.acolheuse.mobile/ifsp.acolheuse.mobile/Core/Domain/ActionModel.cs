using Plugin.CloudFirestore.Attributes;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;

namespace ifsp.acolheuse.mobile.Core.Domain
{
    public class ActionModel : BindableBase
    {
        private string id;
        private string guidAction;
        private string name;
        private int numberVacancies;
        private int isListOpen;
        private bool isOrientation;
        private bool isIndividual;
        private bool isGroup;
        private string idLine;
        private ObservableCollection<ListEntity> responsibleCollection;
        private ObservableCollection<ListEntity> internCollection;

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
        public string GuidAction
        {
            get { return guidAction; }
            set { guidAction = value; RaisePropertyChanged(); }
        }
        public int NumberVacancies
        {
            get { return numberVacancies; }
            set { numberVacancies = value; RaisePropertyChanged(); }
        }
        public int IsListOpen
        {
            get { return isListOpen; }
            set { isListOpen = value; RaisePropertyChanged(); }
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
        public string IdLine
        {
            get { return idLine; }
            set { idLine = value; RaisePropertyChanged(); }
        }

        public ObservableCollection<ListEntity> ResponsibleCollection
        {
            get { return responsibleCollection; }
            set { responsibleCollection = value; RaisePropertyChanged(); }
        }
        public ObservableCollection<ListEntity> InternCollection
        {
            get { return internCollection; }
            set { internCollection = value; RaisePropertyChanged(); }
        }

        public ActionModel()
        {
        }
    }
}
