using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Administrador
{
    public partial class ListaUsuariosPage : ContentPage
    {
        public ListaUsuariosPage()
        {
            InitializeComponent();
        }
        private readonly ViewModels.Administrador.ListarPacientesViewModel _viewModel;
        public ListarPacientesView()
        {
            InitializeComponent();
            _viewModel = new ViewModels.Administrador.ListarPacientesViewModel();
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            _viewModel.BuscarPacientesCollectionAsync();
        }

        private async void LvPaciente_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (e.ItemData != null)
            {
                var item = (PacienteModel)e.ItemData;
                await NavigationService.Instance.PushAsync(new Views.Acolhimento.PacienteAcolhimentoView(item.Id));
            }
        }
    }
}
