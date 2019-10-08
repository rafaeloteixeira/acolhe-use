using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Settings;
using ifsp.acolheuse.mobile.ViewModels;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Administrador
{
    public partial class ListLinesAdminPage : ContentPage
    {
        private readonly ViewModels.Administrador.ListLinesAdminPageViewModel _viewModel;
        public ListLinesAdminPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as ViewModels.Administrador.ListLinesAdminPageViewModel;

            if (Settings.Type == "acolhimento")
                ToolbarItems.Clear();
        }


        protected override void OnAppearing()
        {
            _viewModel.BuscarLinesCollectionAsync();
        }

        private void LvLines_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (Settings.Type != "acolhimento")
            {
                if (e.ItemData != null)
                {
                    var item = (Line)e.ItemData;
                    _viewModel.ItemTapped(item);
                }
            }
        }

        SearchBar searchBar = null;
        private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            searchBar = (sender as SearchBar);
            if (lvLines.DataSource != null)
            {
                this.lvLines.DataSource.Filter = FilterListView;
                this.lvLines.DataSource.RefreshFilter();
            }
        }
        private bool FilterListView(object obj)
        {
            if (searchBar == null || searchBar.Text == null)
                return true;

            var line = obj as Line;
            if (line.Name.ToLower().Contains(searchBar.Text.ToLower()) || line.Name.ToLower().Contains(searchBar.Text.ToLower()))
                return true;
            else
                return false;
        }
    }
}
