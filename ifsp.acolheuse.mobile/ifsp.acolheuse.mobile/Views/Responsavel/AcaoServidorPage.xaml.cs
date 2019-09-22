using ifsp.acolheuse.mobile.ViewModels.Responsavel;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Responsavel
{
    public partial class AcaoServidorPage : ContentPage
    {
        private readonly AcaoServidorPageViewModel _viewModel;
        public AcaoServidorPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {

        }

        private void LvEstagiario_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {

        }
    }
}