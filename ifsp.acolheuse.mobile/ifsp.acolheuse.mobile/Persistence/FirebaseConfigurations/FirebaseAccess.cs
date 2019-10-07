using Firebase.Auth;
using ifsp.acolheuse.mobile.Core.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ifsp.acolheuse.mobile.Persistence.FirebaseConfigurations
{
    public class FirebaseAccess
    {
        private const string firebaseApiKey = "AIzaSyBmHgxxSE_Za1WwBKP8kf61YATAnOAjcek";
        //private const string firebaseAppUri = "https://acolhe-use.firebaseio.com/";
        private FirebaseAccess firebaseClient;

        public FirebaseAccess GetConnection()
        {
            if (firebaseClient == null)
                CreateConnection();

            return firebaseClient;
        }

        private void CreateConnection()
        {
            //firebaseClient = new FirebaseClient(FirebaseAppUri, new FirebaseOptions
            //{
            //    AuthTokenAsyncFactory = () => Task.FromResult(Settings.FirebaseAuthJson)
            //});
        }

        public async Task<string> LoginAsync(ifsp.acolheuse.mobile.Core.Domain.User usuario)
        {
            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(firebaseApiKey));
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(usuario.Email, usuario.Password);
                Settings.FirebaseAuthJson = auth.FirebaseToken;
                Settings.Email = usuario.Email;
                Settings.AccessToken = auth.User.LocalId;

                return null;
            }
            catch (FirebaseAuthException ex)
            {
                return ex.Reason.ToString();
            }
        }

        public async Task<string> CreateUserAsync(ifsp.acolheuse.mobile.Core.Domain.User usuario)
        {
            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(firebaseApiKey));
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(usuario.Email, usuario.Password);

                usuario.AcessToken = auth.User.LocalId;
                return null;
            }
            catch (FirebaseAuthException ex)
            {
                return ex.Reason.ToString();
            }
        }
    }
}
