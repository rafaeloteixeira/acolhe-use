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
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository()
            : base(new FirebaseConfigurations.FirebaseAccess())
        {
            collectionName = "user";
        }

        public async Task<User> GetByAccessTokenAsync(string accessToken)
        {
            if (!String.IsNullOrEmpty(accessToken))
            {
                var query = await CrossCloudFirestore.Current
                      .Instance
                      .GetCollection(collectionName)
                      .WhereEqualsTo("AccessToken", accessToken)
                      .GetDocumentsAsync();

                var yourModels = query.ToObjects<User>();
                return yourModels.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
    }
}
