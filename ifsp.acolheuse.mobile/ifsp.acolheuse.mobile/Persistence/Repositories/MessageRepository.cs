using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ifsp.acolheuse.mobile.Persistence.Repositories
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository()
            : base(new FirebaseConfigurations.FirebaseAccess())
        {
            collectionName = "Message";
        }


        public async Task<IEnumerable<Message>> GetByUserIdAsync(string userId)
        {
            if (!String.IsNullOrEmpty(userId))
            {

                var query = await CrossCloudFirestore.Current
                                       .Instance
                                       .GetCollection(collectionName)
                                       .WhereEqualsTo("IdTo", userId)
                                       .GetDocumentsAsync();

                var yourModels = query.ToObjects<Message>();
                return yourModels;
            }
            else
            {
                return null;
            }
        }

        public async Task<int> GetCountNewMessagesAsync(string userId)
        {
            if (!String.IsNullOrEmpty(userId))
            {

                var query = await CrossCloudFirestore.Current
                                       .Instance
                                       .GetCollection(collectionName)
                                       .WhereEqualsTo("IdTo", userId)
                                       .WhereEqualsTo("Read", false)
                                       .GetDocumentsAsync();

                var yourModels = query.ToObjects<Message>();
                return yourModels.Count();
            }
            else
            {
                return 0;
            }
        }
    }
}
