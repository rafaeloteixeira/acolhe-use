using ifsp.acolheuse.mobile.Services;
using ifsp.acolheuse.mobile.ViewModels;
using System;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Menu
{
    public partial class MenuResponsiblePage : ContentPage
    {
        public MenuResponsiblePage()
        {
            InitializeComponent();
        }

        private void ListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (e.ItemData != null)
            {
                var item = (MenuModel)e.ItemData;
                MenuResponsiblePageViewModel menu = BindingContext as MenuResponsiblePageViewModel;
                menu.NavegarMenu(item.Id);
            }
        }
    }
}
