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
using ifsp.acolheuse.mobile.Core.Settings;

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

            Settings.UserId = "MUpZRnWd2vMfPCBt3c0N";
            await NavigationService.NavigateAsync("NavigationPage/MenuResponsavelPage");

            //await NavigationService.NavigateAsync("NavigationPage/MenuAdminPage");

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

            containerRegistry.RegisterForNavigation<NavigationPage>();

            #region menu
            containerRegistry.RegisterForNavigation<MenuAdminPage, MenuAdminPageViewModel>();
            containerRegistry.RegisterForNavigation<MenuResponsavelPage, MenuResponsavelPageViewModel>();
            #endregion

            #region repository
            containerRegistry.Register<IAcaoRepository, AcaoRepository>();
            containerRegistry.Register<ILinhaRepository, LinhaRepository>();
            containerRegistry.Register<IHorarioAcaoRepository, HorarioAcaoRepository>();
            containerRegistry.Register<IEstagiarioRepository, EstagiarioRepository>();
            containerRegistry.Register<IPacienteRepository, PacienteRepository>();
            containerRegistry.Register<IServidorRepository, ServidorRepository>();
            containerRegistry.Register<IUserRepository, UserRepository>();
            containerRegistry.Register<IAtendimentoRepository, AtendimentoRepository>();
            #endregion

            #region administrador
            containerRegistry.RegisterForNavigation<CadastroAcaoPage, ViewModels.Administrador.CadastroAcaoPageViewModel>();
            containerRegistry.RegisterForNavigation<CadastroLinhaCuidadoPage, ViewModels.Administrador.CadastroLinhaCuidadoPageViewModel>();
            containerRegistry.RegisterForNavigation<EdicaoListaEstagiariosPage, ViewModels.Administrador.EdicaoListaEstagiariosPageViewModel>();
            containerRegistry.RegisterForNavigation<EdicaoListaResponsaveisPage, ViewModels.Administrador.EdicaoListaResponsaveisPageViewModel>();
            containerRegistry.RegisterForNavigation<HorarioAcaoPage, ViewModels.Administrador.HorarioAcaoPageViewModel>();

            containerRegistry.RegisterForNavigation<ListaAcoesAdminPage, ViewModels.Administrador.ListaAcoesAdminPageViewModel>();
            containerRegistry.RegisterForNavigation<ListaEstagiariosAdminPage, ViewModels.Administrador.ListaEstagiariosAdminPageViewModel>();
            containerRegistry.RegisterForNavigation<ListaUsuariosAdminPage, ViewModels.Administrador.ListaUsuariosAdminPageViewModel>();
            containerRegistry.RegisterForNavigation<ListaServidoresAdminPage, ViewModels.Administrador.ListaServidoresAdminPageViewModel>();
            containerRegistry.RegisterForNavigation<ListaLinhasAdminPage, ViewModels.Administrador.ListaLinhasAdminPageViewModel>();


            #endregion

            #region acolhimento
            containerRegistry.RegisterForNavigation<CadastroPacientePage, ViewModels.Acolhimento.CadastroPacientePageViewModel>();
            containerRegistry.RegisterForNavigation<InclusaoAcaoPage, ViewModels.Acolhimento.InclusaoAcaoPageViewModel>();
            #endregion


            containerRegistry.RegisterForNavigation<CadastroEstagiarioPage, ViewModels.Estagio.CadastroEstagiarioPageViewModel>();

            #region responsavel
            containerRegistry.RegisterForNavigation<CadastroServidorPage, ViewModels.Responsavel.CadastroServidorPageViewModel>();
            containerRegistry.RegisterForNavigation<AcaoServidorPage, ViewModels.Responsavel.AcaoServidorPageViewModel>();
            containerRegistry.RegisterForNavigation<AgendaAtendimentoPage, ViewModels.Responsavel.AgendaAtendimentoPageViewModel>();
            containerRegistry.RegisterForNavigation<AgendaServidorPage, ViewModels.Responsavel.AgendaServidorPageViewModel>();
            containerRegistry.RegisterForNavigation<AtendimentoPage, ViewModels.Responsavel.AtendimentoPageViewModel>();
            containerRegistry.RegisterForNavigation<DetalhesAgendamentoPage, ViewModels.Responsavel.DetalhesAgendamentoPageViewModel>();
            containerRegistry.RegisterForNavigation<EstagiarioServidorPage, ViewModels.Responsavel.EstagiarioServidorPageViewModel>();
            containerRegistry.RegisterForNavigation<InclusaoEstagiariosAtendimentoPage, ViewModels.Responsavel.InclusaoEstagiariosAtendimentoPageViewModel>();
            containerRegistry.RegisterForNavigation<InclusaoProfessorInterconsultaPage, ViewModels.Responsavel.InclusaoProfessorInterconsultaPageViewModel>();
            containerRegistry.RegisterForNavigation<ListaAtendimentoResponsavelPage, ViewModels.Responsavel.ListaAtendimentoResponsavelPageViewModel>();
            containerRegistry.RegisterForNavigation<ListaEsperaResponsavelPage, ViewModels.Responsavel.ListaEsperaResponsavelPageViewModel>();

            containerRegistry.RegisterForNavigation<ListaAcoesResponsavelPage, ViewModels.Responsavel.ListaAcoesResponsavelPageViewModel>();
            containerRegistry.RegisterForNavigation<ListaEstagiariosResponsavelPage, ViewModels.Responsavel.ListaEstagiariosResponsavelPageViewModel>();
            containerRegistry.RegisterForNavigation<ListaUsuariosResponsavelPage, ViewModels.Responsavel.ListaUsuariosResponsavelPageViewModel>();
            containerRegistry.RegisterForNavigation<ListaUsuariosAltaResponsavelPage, ViewModels.Responsavel.ListaUsuariosAltaResponsavelPageViewModel>();
            containerRegistry.RegisterForNavigation<UsuarioServidorPage, ViewModels.Responsavel.UsuarioServidorPageViewModel>();
            #endregion
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
