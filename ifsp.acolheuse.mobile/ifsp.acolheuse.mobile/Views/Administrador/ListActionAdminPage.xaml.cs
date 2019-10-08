using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Settings;
using ifsp.acolheuse.mobile.ViewModels;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views.Administrador
{
    public partial class ListActionAdminPage : ContentPage
    {
        private readonly ViewModels.Administrador.ListActionAdminPageViewModel _viewModel;
        public ListActionAdminPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as ViewModels.Administrador.ListActionAdminPageViewModel;
            if (Settings.Type == "acolhimento")
                ToolbarItems.Clear();
        }

        protected override void OnAppearing()
        {
            _viewModel.BuscarLinesCollectionAsync();
        }

        private void LvAction_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
          
            if (Settings.Type != "acolhimento")
            {
                if (e.ItemData != null)
                {
                    var item = (ActionModel)e.ItemData;
                    _viewModel.ItemTapped(item);
                }
            }
        }

        private void SfComboBox_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            _viewModel.BuscarActionCollectionAsync();
        }
    }
}
