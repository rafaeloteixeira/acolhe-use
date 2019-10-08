using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace ifsp.acolheuse.mobile.Core.Domain
{
    public class Day : BindableBase
    {
        private DateTime start;
        private DateTime end;

        public DateTime Start
        {
            get { return start; }
            set { start = value; RaisePropertyChanged(); }
        }
        public DateTime End
        {
            get { return end; }
            set { end = value; RaisePropertyChanged(); }
        }
    }
}
