using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Administrador
{
    public partial class ListaServidoresPage : ContentPage
    {
        public ListaServidoresPage()
        {
            InitializeComponent();
        }
    }
    public partial class ListarServidoresView : ContentPage
    {
        private readonly ViewModels.Administrador.ListarServidoresViewModel _viewModel;
        public ListarServidoresView()
        {
            InitializeComponent();
            _viewModel = new ViewModels.Administrador.ListarServidoresViewModel();
            this.BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            _viewModel.BuscarServidoresCollectionAsync();
        }

        private async void LvServidor_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (e.ItemData != null)
            {
                var item = (ServidorModel)e.ItemData;
                await NavigationService.Instance.PushAsync(new Views.Servidor.ServidorPerfilView(item.UserId));
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

            var servidor = obj as ServidorModel;
            if (servidor.NomeCompleto.ToLower().Contains(searchBar.Text.ToLower()) || servidor.NomeCompleto.ToLower().Contains(searchBar.Text.ToLower()))
                return true;
            else
                return false;
        }
    }
}
