using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Administrador
{
    public partial class ListaLinhasPage : ContentPage
    {
        public ListaLinhasPage()
        {
            InitializeComponent();
        }
    }
    public partial class ListarLinhasView : ContentPage
    {
        private readonly ViewModels.Administrador.ListarLinhasViewModel _viewModel;
        public ListarLinhasView()
        {
            InitializeComponent();
            _viewModel = new ViewModels.Administrador.ListarLinhasViewModel();
            this.BindingContext = _viewModel;

            if (Settings.Tipo == "acolhimento")
                ToolbarItems.Clear();
        }


        protected override void OnAppearing()
        {
            _viewModel.BuscarLinhasCollectionAsync();
        }

        private async void LvLinhas_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (Settings.Tipo != "acolhimento")
            {
                if (e.ItemData != null)
                {
                    var item = (LinhaModel)e.ItemData;
                    await NavigationService.Instance.PushAsync(new Views.Administrador.CadastroLinhaCuidadoView(item));
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

            var linha = obj as LinhaModel;
            if (linha.Nome.ToLower().Contains(searchBar.Text.ToLower()) || linha.Nome.ToLower().Contains(searchBar.Text.ToLower()))
                return true;
            else
                return false;
        }
    }
}
