using ifsp.acolheuse.mobile.ViewModels.Responsavel;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Responsavel
{
    public partial class InclusaoEstagiariosAtendimentoPage : ContentPage
    {
        private readonly InclusaoEstagiariosAtendimentoPageViewModel _viewModel;

        public InclusaoEstagiariosAtendimentoPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as InclusaoEstagiariosAtendimentoPageViewModel;
        }
   
        protected override void OnAppearing()
        {
        }
    }
}