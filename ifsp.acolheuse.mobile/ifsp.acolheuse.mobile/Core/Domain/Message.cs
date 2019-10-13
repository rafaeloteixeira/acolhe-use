using Plugin.CloudFirestore.Attributes;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace ifsp.acolheuse.mobile.Core.Domain
{
    public class Message : BindableBase
    {
        private DateTime date;
        private string id;
        private string idFrom;
        private string nameFrom;
        private string idTo;
        private string idAction;
        private string body;
        private bool read;

        public DateTime Date
        {
            get { return date; }
            set { date = value; RaisePropertyChanged(); }
        }

        [Id]
        public string Id
        {
            get { return id; }
            set { id = value; RaisePropertyChanged(); }
        }
        public string IdFrom
        {
            get { return idFrom; }
            set { idFrom = value; RaisePropertyChanged(); }
        }
        public string NameFrom
        {
            get { return nameFrom; }
            set { nameFrom = value; RaisePropertyChanged(); }
        }
        public string IdTo
        {
            get { return idTo; }
            set { idTo = value; RaisePropertyChanged(); }
        }
        public string IdAction
        {
            get { return idAction; }
            set { idAction = value; RaisePropertyChanged(); }
        }
        public string Body
        {
            get { return body; }
            set { body = value; RaisePropertyChanged(); }
        }
        public bool Read
        {
            get { return read; }
            set { read = value; RaisePropertyChanged(); }
        }
    }
}
