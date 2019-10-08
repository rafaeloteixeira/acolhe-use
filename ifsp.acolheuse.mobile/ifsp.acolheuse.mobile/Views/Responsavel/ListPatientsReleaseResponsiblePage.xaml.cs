using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Services;
using ifsp.acolheuse.mobile.ViewModels;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views
{
    public partial class ListPatientsReleaseResponsiblePage : ContentPage
    {
        private readonly ListPatientsReleaseResponsiblePageViewModel _viewModel;
        public ListPatientsReleaseResponsiblePage()
        {
            InitializeComponent();
            _viewModel = BindingContext as ListPatientsReleaseResponsiblePageViewModel;
        }


        protected override void OnAppearing()
        {

        }

        private async void LvPatient_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (e.ItemData != null)
            {
                var item = (Patient)e.ItemData;
                if (await MessageService.Instance.ShowAsyncYesNo("Deseja enviar este usuário a lista de espera?"))
                {
                    _viewModel.PromoverListWaiting(item);
                }
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

            var pessoa = obj as Patient;
            if (pessoa.NameCompleto.ToLower().Contains(searchBar.Text.ToLower()) || pessoa.NameCompleto.ToLower().Contains(searchBar.Text.ToLower()))
                return true;
            else
                return false;
        }
    }
}