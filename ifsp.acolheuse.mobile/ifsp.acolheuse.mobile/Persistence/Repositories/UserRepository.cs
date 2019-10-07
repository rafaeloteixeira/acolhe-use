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
        }

        public async Task<User> GetByLocalIdAsync(string acessToken)
        {
            var query = await CrossCloudFirestore.Current
                            .Instance
                            .GetCollection(collectionName)
                            .WhereEqualsTo("AcessToken", acessToken)
                            .GetDocumentsAsync();

            var yourModels = query.ToObjects<User>();
            return yourModels.FirstOrDefault();
        }
    }
}
