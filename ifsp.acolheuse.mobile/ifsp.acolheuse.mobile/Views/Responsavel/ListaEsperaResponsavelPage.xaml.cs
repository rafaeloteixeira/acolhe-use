using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Services;
using ifsp.acolheuse.mobile.ViewModels.Responsavel;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Responsavel
{
    public partial class ListaEsperaResponsavelPage : ContentPage
    {
        private readonly ListaEsperaResponsavelPageViewModel _viewModel;
        public ListaEsperaResponsavelPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as ListaEsperaResponsavelPageViewModel;
        }

        protected override void OnAppearing()
        {
        }

        private async void LvPaciente_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (e.ItemData != null)
            {
                var item = (Paciente)e.ItemData;
                if (await MessageService.Instance.ShowAsyncYesNo("Deseja promover este usuário ao atendimento?"))
                {
                    _viewModel.PromoverAtendimento(item);
                }
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