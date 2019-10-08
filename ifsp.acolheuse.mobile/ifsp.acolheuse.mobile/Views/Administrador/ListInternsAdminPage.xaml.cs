using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.ViewModels;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Administrador
{
    public partial class ListInternsAdminPage : ContentPage
    {
        private readonly ViewModels.Administrador.ListInternsAdminPageViewModel _viewModel;
        public ListInternsAdminPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as ViewModels.Administrador.ListInternsAdminPageViewModel;
        }

        protected override void OnAppearing()
        {
            _viewModel.BuscarInternCollectionAsync();
        }

        private void LvIntern_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (e.ItemData != null)
            {
                var intern = (Intern)e.ItemData;
                _viewModel.ItemTapped(intern);
            }
        }

        SearchBar searchBar = null;
        private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            searchBar = (sender as SearchBar);
            if (lvIntern.DataSource != null)
            {
                this.lvIntern.DataSource.Filter = FilterListView;
                this.lvIntern.DataSource.RefreshFilter();
            }
        }
        private bool FilterListView(object obj)
        {
            if (searchBar == null || searchBar.Text == null)
                return true;

            var responsible = obj as Intern;
            if (responsible.NameCompleto.ToLower().Contains(searchBar.Text.ToLower()) || responsible.NameCompleto.ToLower().Contains(searchBar.Text.ToLower()))
                return true;
            else
                return false;
        }
    }
}
