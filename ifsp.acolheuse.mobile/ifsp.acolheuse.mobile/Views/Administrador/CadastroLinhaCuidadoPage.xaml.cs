using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.ViewModels;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Administrador
{
    public partial class CadastroLinhaCuidadoPage : ContentPage
    {
        private readonly CadastroLinhaCuidadoPageViewModel _viewModel;
        public CadastroLinhaCuidadoPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as CadastroLinhaCuidadoPageViewModel;
            _viewModel.GetLinhaAsync();
        }

        private void LvAcoes_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var item = (Acao)e.ItemData;
            _viewModel.OpenCadastroAcao(item);
        }
    }
}
