using ifsp.acolheuse.mobile.ViewModels.Responsavel;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Responsavel
{
    public partial class DetalhesAgendamentoPage : ContentPage
    {
        private readonly DetalhesAgendamentoPageViewModel _viewModel;
        public DetalhesAgendamentoPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
        }
        private void LvEstagiarios_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private void LvResponsaveis_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}