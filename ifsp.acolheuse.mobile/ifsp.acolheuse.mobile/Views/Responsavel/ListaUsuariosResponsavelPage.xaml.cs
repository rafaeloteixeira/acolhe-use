using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.ViewModels.Responsavel;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Responsavel
{
    public partial class ListaUsuariosResponsavelPage : ContentPage
    {
        private readonly ListaUsuariosResponsavelPageViewModel _viewModel;
        public ListaUsuariosResponsavelPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as ListaUsuariosResponsavelPageViewModel;
        }

        protected override void OnAppearing()
        {
            _viewModel.BuscarPacientesCollectionAsync();
        }

        private void LvPaciente_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (e.ItemData != null)
            {
                var item = (Paciente)e.ItemData;
                _viewModel.NavigateToPacienteServidor(item);
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

            var servidor = obj as Paciente;
            if (servidor.NomeCompleto.ToLower().Contains(searchBar.Text.ToLower()) || servidor.NomeCompleto.ToLower().Contains(searchBar.Text.ToLower()))
                return true;
            else
                return false;
        }

    }
}