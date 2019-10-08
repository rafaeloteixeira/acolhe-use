using ifsp.acolheuse.mobile.ViewModels;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views
{
    public partial class ActionResponsiblePage : ContentPage
    {
        private readonly ActionResponsiblePageViewModel _viewModel;
        public ActionResponsiblePage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {

        }

        private void LvIntern_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {

        }
    }
}