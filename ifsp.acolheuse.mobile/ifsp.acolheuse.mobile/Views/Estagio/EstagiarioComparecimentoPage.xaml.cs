using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.ViewModels;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Estagio
{
    public partial class EstagiarioComparecimentoPage : ContentPage
    {
        private readonly EstagiarioComparecimentoPageViewModel _viewModel;
        public EstagiarioComparecimentoPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as EstagiarioComparecimentoPageViewModel;
        }
        protected override void OnAppearing()
        {
            _viewModel.BuscarAtendimentosAsync();
        }
        private void ListTTasks_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (e.ItemData != null)
            {
                var atendimento = (Atendimento)e.ItemData;
                popupLayout.Show();
            }
        }
    }
}
