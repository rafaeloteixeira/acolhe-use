using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ifsp.acolheuse.mobile.Services
{
    public class MessageService
    {
        private static MessageService instance;

        private MessageService() { }

        public static MessageService Instance
        {
            get
            {
                if (instance == null)
                    lock (typeof(MessageService))
                        if (instance == null) instance = new MessageService();

                return instance;
            }
        }

        public async Task ShowAsync(string message)
        {
            await App.Current.MainPage.DisplayAlert("USE App", message, "ok");
        }
        public async Task<bool> ShowAsyncYesNo(string message)
        {
            if (await App.Current.MainPage.DisplayAlert("USE App", message, "Sim", "Não"))
                return true;

            return false;
        }
    }
}
