using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.ViewModels;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views
{
    public partial class ListInternsResponsiblePage : ContentPage
    {
        private ListInternsResponsiblePageViewModel _viewModel;
        public ListInternsResponsiblePage()
        {
            InitializeComponent();
            _viewModel = BindingContext as ListInternsResponsiblePageViewModel;
        }



        private void LvIntern_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (e.ItemData != null)
            {
                var intern = (Intern)e.ItemData;
                _viewModel.NavigateToInternResponsible(intern);
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