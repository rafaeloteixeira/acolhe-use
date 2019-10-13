using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using ifsp.acolheuse.mobile.Core.Settings;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ifsp.acolheuse.mobile.ViewModels
{
    public class MessagesPageViewModel : ViewModelBase
    {
        #region properties
        private IEnumerable<Message> messageCollection;
        public IEnumerable<Message> MessageCollection
        {
            get { return messageCollection; }
            set { messageCollection = value; RaisePropertyChanged(); }
        }
        #endregion

        private INavigationService navigationService;
        private IMessageRepository messageRepository;

        public MessagesPageViewModel(INavigationService navigationService, IMessageRepository messageRepository) :
            base(navigationService)
        {
            this.navigationService = navigationService;
            this.messageRepository = messageRepository;
        }
        private async void GetMessages()
        {
            MessageCollection = await messageRepository.GetByUserIdAsync(Settings.UserId);
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            ObservableCollection<Message> messages = new ObservableCollection<Message> (MessageCollection.Where(x => x.Read == false));
            for (int i = 0; i < messages.Count(); i++)
            {
                messages[i].Read = true;
                messageRepository.UpdateAsync(messages[i], messages[i].Id);
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            var navigationMode = parameters.GetNavigationMode();
            if (navigationMode != NavigationMode.Back)
            {
                GetMessages();
            }
        }
    }
}
