using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Administrador
{
    public partial class ListaEstagiariosPage : ContentPage
    {
        public ListaEstagiariosPage()
        {
            InitializeComponent();
        }
    }
    public partial class ListarEstagiariosView : ContentPage
    {
        private readonly ViewModels.Administrador.ListarEstagiariosViewModel _viewModel;
        public ListarEstagiariosView()
        {
            InitializeComponent();
            _viewModel = new ViewModels.Administrador.ListarEstagiariosViewModel();
            this.BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            _viewModel.BuscarEstagiarioCollectionAsync();
        }

        private async void LvEstagiario_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (e.ItemData != null)
            {
                var estagiario = (EstagiarioModel)e.ItemData;
                await NavigationService.Instance.PushAsync(new Views.Estagiario.EstagiarioPerfilView(estagiario.UserId));
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

            var servidor = obj as EstagiarioModel;
            if (servidor.NomeCompleto.ToLower().Contains(searchBar.Text.ToLower()) || servidor.NomeCompleto.ToLower().Contains(searchBar.Text.ToLower()))
                return true;
            else
                return false;
        }
    }
}
