using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.ViewModels.Responsavel;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Responsavel
{
    public partial class InclusaoProfessorInterconsultaPage : ContentPage
    {
        private readonly InclusaoProfessorInterconsultaPageViewModel _viewModel;

        public InclusaoProfessorInterconsultaPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as InclusaoProfessorInterconsultaPageViewModel;
        }

        protected override void OnAppearing()
        {
            _viewModel.BuscarAcoesCollectionAsync();
        }

        private void LvAcoes_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (e.ItemData != null)
            {
                var acao = (Acao)e.ItemData;
                _viewModel.EnviarInterconsultaAsync(acao);
            }
        }
    }
}