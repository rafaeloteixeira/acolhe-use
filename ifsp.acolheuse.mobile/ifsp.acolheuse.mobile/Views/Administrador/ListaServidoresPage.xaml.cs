using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.ViewModels;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Administrador
{
    public partial class ListaServidoresPage : ContentPage
    {
        private readonly ListaServidoresPageViewModel _viewModel;
        public ListaServidoresPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as ListaServidoresPageViewModel;
        }
        protected override void OnAppearing()
        {
            _viewModel.BuscarServidoresCollectionAsync();
        }

        private void LvLinhas_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (e.ItemData != null)
            {
                var item = (Servidor)e.ItemData;
                _viewModel.ItemTapped(item);
            }
        }

        SearchBar searchBar = null;
        private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            searchBar = (sender as SearchBar);
            if (lvServidor.DataSource != null)
            {
                this.lvServidor.DataSource.Filter = FilterListView;
                this.lvServidor.DataSource.RefreshFilter();
            }
        }
        private bool FilterListView(object obj)
        {
            if (searchBar == null || searchBar.Text == null)
                return true;

            var servidor = obj as Servidor;
            if (servidor.NomeCompleto.ToLower().Contains(searchBar.Text.ToLower()) || servidor.NomeCompleto.ToLower().Contains(searchBar.Text.ToLower()))
                return true;
            else
                return false;
        }
    }
}
