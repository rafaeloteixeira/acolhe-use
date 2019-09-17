using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Settings;
using ifsp.acolheuse.mobile.ViewModels;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Administrador
{
    public partial class ListaAcoesPage : ContentPage
    {
        private readonly ListaAcoesPageViewModel _viewModel;
        public ListaAcoesPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as ListaAcoesPageViewModel;
            if (Settings.Tipo == "acolhimento")
                ToolbarItems.Clear();
        }

        protected override void OnAppearing()
        {
            _viewModel.BuscarLinhasCollectionAsync();
        }

        private void LvAcoes_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
          
            if (Settings.Tipo != "acolhimento")
            {
                if (e.ItemData != null)
                {
                    var item = (Acao)e.ItemData;
                    _viewModel.ItemTapped(item);
                }
            }
        }

        private void SfComboBox_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            _viewModel.BuscarAcoesCollectionAsync();
        }
    }
}
