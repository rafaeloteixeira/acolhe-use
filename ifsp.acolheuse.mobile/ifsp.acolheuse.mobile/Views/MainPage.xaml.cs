using ifsp.acolheuse.mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Admin_Clicked(object sender, EventArgs e)
        {

            (BindingContext as MainPageViewModel).NavigateAdmin(); 
        }

        private void Responsible_Clicked(object sender, EventArgs e)
        {
            (BindingContext as MainPageViewModel).NavigateServ();
           
    
        }
    }
}