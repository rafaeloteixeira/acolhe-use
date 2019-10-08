using Prism;
using Prism.Ioc;
using ifsp.acolheuse.mobile.ViewModels;
using ifsp.acolheuse.mobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Plugin.CloudFirestore;
using ifsp.acolheuse.mobile.Persistence.Repositories;
using ifsp.acolheuse.mobile.Core.Repositories;
using ifsp.acolheuse.mobile.Views.Administrador;
using ifsp.acolheuse.mobile.Views.Acolhimento;
using ifsp.acolheuse.mobile.Views.Estagio;
using ifsp.acolheuse.mobile.Views.Menu;
using ifsp.acolheuse.mobile.Core.Settings;
using ifsp.acolheuse.mobile.Views.AuthForms;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ifsp.acolheuse.mobile
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTU0MDM0QDMxMzcyZTMzMmUzMFBhRzFJOHIyenlJZE1ER2hROGVVTTdzV1JOMXJDdjlERnQwa1o3V29mejg9");
            await NavigationService.NavigateAsync("NavigationPage/LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();

            #region menu
            containerRegistry.RegisterForNavigation<MenuAdminPage, MenuAdminPageViewModel>();
            containerRegistry.RegisterForNavigation<MenuResponsiblePage, MenuResponsiblePageViewModel>();
            #endregion

            #region repository
            containerRegistry.Register<IActionRepository, ActionRepository>();
            containerRegistry.Register<ILineRepository, LineRepository>();
            containerRegistry.Register<IScheduleActionRepository, ScheduleActionRepository>();
            containerRegistry.Register<IInternRepository, InternRepository>();
            containerRegistry.Register<IPatientRepository, PatientRepository>();
            containerRegistry.Register<IResponsibleRepository, ResponsibleRepository>();
            containerRegistry.Register<IUserRepository, UserRepository>();
            containerRegistry.Register<IAppointmentRepository, AppointmentRepository>();
            #endregion

            #region administrador
            containerRegistry.RegisterForNavigation<RegisterActionPage, ViewModels.Administrador.RegisterActionPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterLinePage, ViewModels.Administrador.RegisterLinePageViewModel>();
            containerRegistry.RegisterForNavigation<EditListInternsPage, ViewModels.Administrador.EditListInternsPageViewModel>();
            containerRegistry.RegisterForNavigation<EditListResponsiblePage, ViewModels.Administrador.EditListResponsiblePageViewModel>();
            containerRegistry.RegisterForNavigation<ScheduleActionPage, ViewModels.Administrador.ScheduleActionPageViewModel>();

            containerRegistry.RegisterForNavigation<ListActionAdminPage, ViewModels.Administrador.ListActionAdminPageViewModel>();
            containerRegistry.RegisterForNavigation<ListInternsAdminPage, ViewModels.Administrador.ListInternsAdminPageViewModel>();
            containerRegistry.RegisterForNavigation<ListPatientsAdminPage, ViewModels.Administrador.ListPatientsAdminPageViewModel>();
            containerRegistry.RegisterForNavigation<ListResponsibleAdminPage, ViewModels.Administrador.ListResponsibleAdminPageViewModel>();
            containerRegistry.RegisterForNavigation<ListLinesAdminPage, ViewModels.Administrador.ListLinesAdminPageViewModel>();


            #endregion

            #region acolhimento
            containerRegistry.RegisterForNavigation<RegisterPatientPage, ViewModels.Acolhimento.RegisterPatientPageViewModel>();
            containerRegistry.RegisterForNavigation<SelectionActionPage, ViewModels.Acolhimento.SelectionActionPageViewModel>();
            #endregion


            containerRegistry.RegisterForNavigation<RegisterInternPage, ViewModels.Estagio.RegisterInternPageViewModel>();

            #region responsible
            containerRegistry.RegisterForNavigation<RegisterResponsiblePage, ViewModels.RegisterResponsiblePageViewModel>();
            containerRegistry.RegisterForNavigation<ActionResponsiblePage, ViewModels.ActionResponsiblePageViewModel>();
            containerRegistry.RegisterForNavigation<ScheduleAppointmentPage, ViewModels.ScheduleAppointmentPageViewModel>();
            containerRegistry.RegisterForNavigation<ScheduleResponsiblePage, ViewModels.ScheduleResponsiblePageViewModel>();
            containerRegistry.RegisterForNavigation<AppointmentPage, ViewModels.AppointmentPageViewModel>();
            containerRegistry.RegisterForNavigation<DetailsschedulePage, ViewModels.DetailsschedulePageViewModel>();
            containerRegistry.RegisterForNavigation<InternResponsiblePage, ViewModels.InternResponsiblePageViewModel>();
            containerRegistry.RegisterForNavigation<SelectionInternsAppointmentPage, ViewModels.SelectionInternsAppointmentPageViewModel>();
            containerRegistry.RegisterForNavigation<SelectionResponsibleInterconsultationtionPage, ViewModels.SelectionResponsibleInterconsultationtionPageViewModel>();
            containerRegistry.RegisterForNavigation<ListAppointmentResponsiblePage, ViewModels.ListAppointmentResponsiblePageViewModel>();
            containerRegistry.RegisterForNavigation<ListWaitingResponsiblePage, ViewModels.ListWaitingResponsiblePageViewModel>();
            containerRegistry.RegisterForNavigation<ListInterconsultationtionResponsiblePage, ViewModels.ListInterconsultationtionResponsiblePageViewModel>();

            containerRegistry.RegisterForNavigation<ListActionResponsiblePage, ViewModels.ListActionResponsiblePageViewModel>();
            containerRegistry.RegisterForNavigation<ListInternsResponsiblePage, ViewModels.ListInternsResponsiblePageViewModel>();
            containerRegistry.RegisterForNavigation<ListPatientsResponsiblePage, ViewModels.ListPatientsResponsiblePageViewModel>();
            containerRegistry.RegisterForNavigation<ListPatientsReleaseResponsiblePage, ViewModels.ListPatientsReleaseResponsiblePageViewModel>();
            containerRegistry.RegisterForNavigation<PatientResponsiblePage, ViewModels.PatientResponsiblePageViewModel>();
            #endregion
            containerRegistry.RegisterForNavigation<ScheduleInternPage, ScheduleInternPageViewModel>();
            containerRegistry.RegisterForNavigation<InternAttendancePage, InternAttendancePageViewModel>();
            containerRegistry.RegisterForNavigation<MenuHostPage, MenuHostPageViewModel>();
            containerRegistry.RegisterForNavigation<MenuInternPage, MenuInternPageViewModel>();
            containerRegistry.RegisterForNavigation<ListResponsibleHostPage, ListResponsibleHostPageViewModel>();
            containerRegistry.RegisterForNavigation<ListLinesHostPage, ListLinesHostPageViewModel>();
            containerRegistry.RegisterForNavigation<ListActionHostPage, ListActionHostPageViewModel>();
            containerRegistry.RegisterForNavigation<ListInternsHostPage, ListInternsHostPageViewModel>();
            containerRegistry.RegisterForNavigation<ListPatientsHostPage, ListPatientsHostPageViewModel>();
        }

        protected override void OnStart()
        {
            AppCenter.Start("android=0171744e-f6ec-4917-a2f4-3d9c15c1ecbf;" +
                              "uwp={Your UWP App secret here};" +
                              "ios={Your iOS App secret here}",
                              typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
