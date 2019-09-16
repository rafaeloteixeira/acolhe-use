using ifsp.acolheuse.mobile.Core.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ifsp.acolheuse.mobile.Persistence.FirebaseConfigurations
{
    public class FirebaseAccess
    {
        private const string firebaseApiKey = "AIzaSyA2PIWZJx0qqAFP9rPrddEkVAWnrfR5MPE";
        private const string firebaseAppUri = "https://acolhe-use.firebaseio.com/";
        private FirebaseAccess firebaseClient;

        public FirebaseAccess GetConnection()
        {
            if (firebaseClient == null)
                CreateConnection();

            return firebaseClient;
        }

        private void CreateConnection()
        {
            //firebaseClient = new FirebaseAccess(firebaseAppUri, new FirebaseOptions
            //{
            //    AuthTokenAsyncFactory = () => Task.FromResult(Settings.FirebaseAuthJson)
            //});
        }
    }
}
