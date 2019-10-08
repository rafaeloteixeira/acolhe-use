using Plugin.CloudFirestore.Attributes;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ifsp.acolheuse.mobile.Core.Domain
{
    public class Line : BindableBase
    {
        private string id;
        private string name;
      
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


        public Line()
        {
        }
    }
}
