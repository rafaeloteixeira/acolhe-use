using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Settings;
using ifsp.acolheuse.mobile.ViewModels;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Administrador
{
    public partial class ListaLinhasPage : ContentPage
    {
        private readonly ListaLinhasPageViewModel _viewModel;
        public ListaLinhasPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as ListaLinhasPageViewModel;

            if (Settings.Tipo == "acolhimento")
                ToolbarItems.Clear();
        }


        protected override void OnAppearing()
        {
            _viewModel.BuscarLinhasCollectionAsync();
        }

        private void LvLinhas_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (Settings.Tipo != "acolhimento")
            {
                if (e.ItemData != null)
                {
                    var item = (Linha)e.ItemData;
                    _viewModel.ItemTapped(item);
                }
            }
        }

        SearchBar searchBar = null;
        private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            searchBar = (sender as SearchBar);
            if (lvLinhas.DataSource != null)
            {
                this.lvLinhas.DataSource.Filter = FilterListView;
                this.lvLinhas.DataSource.RefreshFilter();
            }
        }
        private bool FilterListView(object obj)
        {
            if (searchBar == null || searchBar.Text == null)
                return true;

            var linha = obj as Linha;
            if (linha.Nome.ToLower().Contains(searchBar.Text.ToLower()) || linha.Nome.ToLower().Contains(searchBar.Text.ToLower()))
                return true;
            else
                return false;
        }
    }
}
