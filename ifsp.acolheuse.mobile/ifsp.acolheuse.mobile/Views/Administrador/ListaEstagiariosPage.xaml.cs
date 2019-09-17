using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.ViewModels;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Administrador
{
    public partial class ListaEstagiariosPage : ContentPage
    {
        private readonly ListaEstagiariosPageViewModel _viewModel;
        public ListaEstagiariosPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as ListaEstagiariosPageViewModel;
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
