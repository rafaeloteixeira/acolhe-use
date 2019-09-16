using Android.App;
using Android.Content.PM;
using Android.OS;
using Firebase;
using Prism;
using Prism.Ioc;

namespace ifsp.acolheuse.mobile.Droid
{
    [Activity(Label = "ifsp.acolheuse.mobile", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            //var options = new FirebaseOptions.Builder()
            //.SetApplicationId("1:466091313479:android:418a07fe33923f3acca462")
            //.SetApiKey("AIzaSyBmHgxxSE_Za1WwBKP8kf61YATAnOAjcek")
            //.SetProjectId("acolhe-use")
            ////.SetDatabaseUrl("Firebase-Database-Url")
            //.Build();

            //var firebaseApp = FirebaseApp.InitializeApp(this);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}

