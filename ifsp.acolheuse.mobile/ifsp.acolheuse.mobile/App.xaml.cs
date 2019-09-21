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
using ifsp.acolheuse.mobile.Views.Responsavel;
using ifsp.acolheuse.mobile.Views.Estagio;
using ifsp.acolheuse.mobile.Views.Menu;

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

            await NavigationService.NavigateAsync("NavigationPage/MenuPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<ViewA, ViewAViewModel>();
            containerRegistry.RegisterForNavigation<ViewB, ViewBViewModel>();
            containerRegistry.RegisterForNavigation<PrismContentPage1, PrismContentPage1ViewModel>();

            containerRegistry.Register<IAcaoRepository, AcaoRepository>();
            containerRegistry.Register<ILinhaRepository, LinhaRepository>();

            containerRegistry.Register<IHorarioAcaoRepository, HorarioAcaoRepository>();
            containerRegistry.Register<IEstagiarioRepository, EstagiarioRepository>();
            containerRegistry.Register<IPacienteRepository, PacienteRepository>();
            containerRegistry.Register<IServidorRepository, ServidorRepository>();
            containerRegistry.Register<IUserRepository, UserRepository>();

            #region administrador
            containerRegistry.RegisterForNavigation<CadastroAcaoPage, CadastroAcaoPageViewModel>();
            containerRegistry.RegisterForNavigation<CadastroLinhaCuidadoPage, CadastroLinhaCuidadoPageViewModel>();
            containerRegistry.RegisterForNavigation<EdicaoListaEstagiariosPage, EdicaoListaEstagiariosPageViewModel>();
            containerRegistry.RegisterForNavigation<EdicaoListaResponsaveisPage, EdicaoListaResponsaveisPageViewModel>();
            containerRegistry.RegisterForNavigation<HorarioAcaoPage, HorarioAcaoPageViewModel>();
            containerRegistry.RegisterForNavigation<ListaAcoesPage, ListaAcoesPageViewModel>();
            containerRegistry.RegisterForNavigation<ListaEstagiariosPage, ListaEstagiariosPageViewModel>();
            containerRegistry.RegisterForNavigation<ListaLinhasPage, ListaLinhasPageViewModel>();
            containerRegistry.RegisterForNavigation<ListaServidoresPage, ListaServidoresPageViewModel>();
            containerRegistry.RegisterForNavigation<ListaUsuariosPage, ListaUsuariosPageViewModel>();
            #endregion

            containerRegistry.RegisterForNavigation<CadastroPacientePage, CadastroPacientePageViewModel>();
            containerRegistry.RegisterForNavigation<CadastroServidorPage, CadastroServidorPageViewModel>();
            containerRegistry.RegisterForNavigation<CadastroEstagiarioPage, CadastroEstagiarioPageViewModel>();
            containerRegistry.RegisterForNavigation<IncluirAcaoPage, IncluirAcaoPageViewModel>();
            containerRegistry.RegisterForNavigation<MenuPage, MenuPageViewModel>();
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
