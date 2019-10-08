using ifsp.acolheuse.mobile.Services;
using ifsp.acolheuse.mobile.ViewModels;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Menu
{
    public partial class MenuInternPage : ContentPage
    {
        public MenuInternPage()
        {
            InitializeComponent();
        }
        private void ListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (e.ItemData != null)
            {
                var item = (MenuModel)e.ItemData;
                MenuInternPageViewModel menu = BindingContext as MenuInternPageViewModel;
                menu.NavegarMenu(item.Id);
            }
        }
    }
}
