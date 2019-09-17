using System;
using System.Collections.Generic;
using System.Text;

namespace ifsp.acolheuse.mobile.Services
{
    public class MenuModel
    {
        private string titulo;
        private int id;

        public string Titulo
        {
            get { return titulo; }
            set { titulo = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public MenuModel()
        {

        }
    }
}
