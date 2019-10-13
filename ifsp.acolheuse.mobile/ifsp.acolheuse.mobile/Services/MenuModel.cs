using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace ifsp.acolheuse.mobile.Services
{
    public class MenuModel : BindableBase
    {
        private string titulo;
        private string badge;
        private int id;

        public string Titulo
        {
            get { return titulo; }
            set { titulo = value;RaisePropertyChanged(); }
        }
        public string Badge
        {
            get { return badge; }
            set { badge = value; RaisePropertyChanged(); }
        }
        public int Id
        {
            get { return id; }
            set { id = value; RaisePropertyChanged(); }
        }



        public MenuModel()
        {

        }
    }
}
