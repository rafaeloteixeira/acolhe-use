using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.ViewModels;
using Xamarin.Forms;

namespace ifsp.acolheuse.mobile.Views
{
    public partial class SelectionResponsibleInterconsultationtionPage : ContentPage
    {
        private readonly SelectionResponsibleInterconsultationtionPageViewModel _viewModel;

        public SelectionResponsibleInterconsultationtionPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as SelectionResponsibleInterconsultationtionPageViewModel;
        }

        protected override void OnAppearing()
        {
            _viewModel.BuscarActionCollectionAsync();
        }

        private void LvAction_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (e.ItemData != null)
            {
                var action = (ActionModel)e.ItemData;
                _viewModel.EnviarInterconsultationtionAsync(action);
            }
        }
    }
}