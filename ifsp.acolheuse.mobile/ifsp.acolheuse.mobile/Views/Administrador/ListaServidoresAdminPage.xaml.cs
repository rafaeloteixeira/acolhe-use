using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.ViewModels;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Administrador
{
    public partial class ListaServidoresAdminPage : ContentPage
    {
        private readonly ViewModels.Administrador.ListaServidoresAdminPageViewModel _viewModel;
        public ListaServidoresAdminPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as ViewModels.Administrador.ListaServidoresAdminPageViewModel;
        }
        protected override void OnAppearing()
        {
            _viewModel.BuscarServidoresCollectionAsync();
        }

        private void LvServidor_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
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
