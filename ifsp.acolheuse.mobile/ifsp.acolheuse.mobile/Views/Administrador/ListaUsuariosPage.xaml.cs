using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.ViewModels;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Administrador
{
    public partial class ListaUsuariosPage : ContentPage
    {
        private readonly ListaUsuariosPageViewModel _viewModel;
        public ListaUsuariosPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as ListaUsuariosPageViewModel;
        }
        protected override void OnAppearing()
        {
            _viewModel.BuscarPacientesCollectionAsync();
        }

        private void LvLinhas_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (e.ItemData != null)
            {
                var item = (Paciente)e.ItemData;
                _viewModel.ItemTapped(item);
            }
        }

        SearchBar searchBar = null;
        private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            searchBar = (sender as SearchBar);
            if (lvPaciente.DataSource != null)
            {
                this.lvPaciente.DataSource.Filter = FilterListView;
                this.lvPaciente.DataSource.RefreshFilter();
            }
        }
        private bool FilterListView(object obj)
        {
            if (searchBar == null || searchBar.Text == null)
                return true;

            var paciente = obj as Paciente;
            if (paciente.NomeCompleto.ToLower().Contains(searchBar.Text.ToLower()) || paciente.NomeCompleto.ToLower().Contains(searchBar.Text.ToLower()))
                return true;
            else
                return false;
        }
    }
}
