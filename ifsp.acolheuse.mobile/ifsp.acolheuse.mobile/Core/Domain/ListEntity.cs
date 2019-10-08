using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace ifsp.acolheuse.mobile.Core.Domain
{
    public class ListEntity : BindableBase
    {
        private string id;
        private string name;
        private bool added;

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
        public bool Added
        {
            get { return added; }
            set { added = value; RaisePropertyChanged(); }
        }
    }
}
