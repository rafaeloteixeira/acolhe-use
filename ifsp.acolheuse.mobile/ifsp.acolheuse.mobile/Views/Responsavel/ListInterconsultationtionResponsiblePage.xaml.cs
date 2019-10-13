using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Services;
using ifsp.acolheuse.mobile.ViewModels;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views
{
    public partial class ListInterconsultationtionResponsiblePage : ContentPage
    {
        private readonly ListInterconsultationtionResponsiblePageViewModel _viewModel;
        public ListInterconsultationtionResponsiblePage()
        {
            InitializeComponent();
            _viewModel = BindingContext as ListInterconsultationtionResponsiblePageViewModel;
        }

        protected override void OnAppearing()
        {
        }

        private void LvPatient_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (e.ItemData != null)
            {
                var item = (Patient)e.ItemData;
                _viewModel.PromoverAppointment(item);
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

            var responsible = obj as Patient;
            if (responsible.NameCompleto.ToLower().Contains(searchBar.Text.ToLower()) || responsible.NameCompleto.ToLower().Contains(searchBar.Text.ToLower()))
                return true;
            else
                return false;
        }
    }
}
