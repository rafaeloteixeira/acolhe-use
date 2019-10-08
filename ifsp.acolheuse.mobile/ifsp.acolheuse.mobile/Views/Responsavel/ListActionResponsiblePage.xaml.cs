using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.ViewModels;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views
{
    public partial class ListActionResponsiblePage : ContentPage
    {
        private readonly ViewModels.ListActionResponsiblePageViewModel _viewModel;
        public ListActionResponsiblePage()
        {
            InitializeComponent();
            _viewModel = BindingContext as ViewModels.ListActionResponsiblePageViewModel;
        }
        protected override void OnAppearing()
        {
            
        }

        private  void LvAction_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (e.ItemData != null)
            {
                var item = (ActionModel)e.ItemData;
                _viewModel.NavigateToAction(item);
            }
        }

        private void SfComboBox_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            _viewModel.BuscarActionCollectionAsync();
        }
    }
}