using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.ViewModels;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Administrador
{
    public partial class ListPatientsAdminPage : ContentPage
    {
        private readonly ViewModels.Administrador.ListPatientsAdminPageViewModel _viewModel;
        public ListPatientsAdminPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as ViewModels.Administrador.ListPatientsAdminPageViewModel;
        }
        protected override void OnAppearing()
        {
            _viewModel.BuscarPatientsCollectionAsync();
        }
        private void LvPatient_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (e.ItemData != null)
            {
                var item = (Patient)e.ItemData;
                _viewModel.ItemTapped(item);
            }
        }

        SearchBar searchBar = null;
        private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            searchBar = (sender as SearchBar);
            if (lvPatient.DataSource != null)
            {
                this.lvPatient.DataSource.Filter = FilterListView;
                this.lvPatient.DataSource.RefreshFilter();
            }
        }
        private bool FilterListView(object obj)
        {
            if (searchBar == null || searchBar.Text == null)
                return true;

            var patient = obj as Patient;
            if (patient.NameCompleto.ToLower().Contains(searchBar.Text.ToLower()) || patient.NameCompleto.ToLower().Contains(searchBar.Text.ToLower()))
                return true;
            else
                return false;
        }
    }
}
