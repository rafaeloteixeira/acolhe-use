using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.ViewModels;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Administrador
{
    public partial class RegisterLinePage : ContentPage
    {
        private readonly ViewModels.Administrador.RegisterLinePageViewModel _viewModel;
        public RegisterLinePage()
        {
            InitializeComponent();
            _viewModel = BindingContext as ViewModels.Administrador.RegisterLinePageViewModel;
            _viewModel.GetLineAsync();
        }

        private void LvAction_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var item = (ActionModel)e.ItemData;
            _viewModel.OpenRegisterAction(item);
        }
    }
}
