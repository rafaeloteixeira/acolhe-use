using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace ifsp.acolheuse.mobile.Core.Domain
{
    public class DaysAction : BindableBase
    {
        private Day seg;
        private Day ter;
        private Day qua;
        private Day qui;
        private Day sex;

        public Day Seg
        {
            get { return seg; }
            set { seg = value; RaisePropertyChanged(); }
        }

        public Day Ter
        {
            get { return ter; }
            set { ter = value; RaisePropertyChanged(); }
        }

        public Day Qua
        {
            get { return qua; }
            set { qua = value; RaisePropertyChanged(); }
        }

        public Day Qui
        {
            get { return qui; }
            set { qui = value; RaisePropertyChanged(); }
        }

        public Day Sex
        {
            get { return sex; }
            set { sex = value; RaisePropertyChanged(); }
        }

        public DaysAction()
        {
            Seg = new Day();
            Ter = new Day();
            Qua = new Day();
            Qui = new Day();
            Sex = new Day();
        }
    }
}
