using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace ifsp.acolheuse.mobile.Core.Domain
{
    public class Dia : BindableBase
    {
        private DateTime inicio;
        private DateTime fim;

        public DateTime Inicio
        {
            get { return inicio; }
            set { inicio = value; RaisePropertyChanged(); }
        }
        public DateTime Fim
        {
            get { return fim; }
            set { fim = value; RaisePropertyChanged(); }
        }
    }
}
