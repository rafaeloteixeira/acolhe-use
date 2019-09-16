using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace ifsp.acolheuse.mobile.Core.Domain
{
    public class DiasAcao : BindableBase
    {
        private Dia seg;
        private Dia ter;
        private Dia qua;
        private Dia qui;
        private Dia sex;

        public Dia Seg
        {
            get { return seg; }
            set { seg = value; RaisePropertyChanged(); }
        }

        public Dia Ter
        {
            get { return ter; }
            set { ter = value; RaisePropertyChanged(); }
        }

        public Dia Qua
        {
            get { return qua; }
            set { qua = value; RaisePropertyChanged(); }
        }

        public Dia Qui
        {
            get { return qui; }
            set { qui = value; RaisePropertyChanged(); }
        }

        public Dia Sex
        {
            get { return sex; }
            set { sex = value; RaisePropertyChanged(); }
        }

        public DiasAcao()
        {
            Seg = new Dia();
            Ter = new Dia();
            Qua = new Dia();
            Qui = new Dia();
            Sex = new Dia();
        }
    }
}
