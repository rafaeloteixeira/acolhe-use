using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.ViewModels;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Administrador
{
    public partial class ListaEstagiariosAdminPage : ContentPage
    {
        private readonly ViewModels.Administrador.ListaEstagiariosAdminPageViewModel _viewModel;
        public ListaEstagiariosAdminPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as ViewModels.Administrador.ListaEstagiariosAdminPageViewModel;
        }

        protected override void OnAppearing()
        {
            _viewModel.BuscarEstagiarioCollectionAsync();
        }

        private void LvEstagiario_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (e.ItemData != null)
            {
                var estagiario = (Estagiario)e.ItemData;
                _viewModel.ItemTapped(estagiario);
            }
        }

        SearchBar searchBar = null;
        private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            searchBar = (sender as SearchBar);
            if (lvEstagiario.DataSource != null)
            {
                this.lvEstagiario.DataSource.Filter = FilterListView;
                this.lvEstagiario.DataSource.RefreshFilter();
            }
        }
        private bool FilterListView(object obj)
        {
            if (searchBar == null || searchBar.Text == null)
                return true;

            var servidor = obj as Estagiario;
            if (servidor.NomeCompleto.ToLower().Contains(searchBar.Text.ToLower()) || servidor.NomeCompleto.ToLower().Contains(searchBar.Text.ToLower()))
                return true;
            else
                return false;
        }
    }
}
