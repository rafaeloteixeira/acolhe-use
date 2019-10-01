using ifsp.acolheuse.mobile.Services;
using ifsp.acolheuse.mobile.ViewModels;
using System;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Menu
{
    public partial class MenuResponsavelPage : ContentPage
    {
        public MenuResponsavelPage()
        {
            InitializeComponent();
        }

        private void ListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (e.ItemData != null)
            {
                var item = (MenuModel)e.ItemData;
                MenuResponsavelPageViewModel menu = BindingContext as MenuResponsavelPageViewModel;
                menu.NavegarMenu(item.Id);
            }
        }
    }
}
