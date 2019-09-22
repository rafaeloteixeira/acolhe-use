using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.ViewModels;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Responsavel
{
    public partial class ListaAcoesResponsavelPage : ContentPage
    {
        private readonly ViewModels.Responsavel.ListaAcoesResponsavelPageViewModel _viewModel;
        public ListaAcoesResponsavelPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as ViewModels.Responsavel.ListaAcoesResponsavelPageViewModel;
        }
        protected override void OnAppearing()
        {
            _viewModel.BuscarLinhasCollectionAsync();
        }

        private  void LvAcoes_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (e.ItemData != null)
            {
                var item = (Acao)e.ItemData;
                _viewModel.NavigateToAcao(item);
            }
        }

        private void SfComboBox_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
        }
    }
}