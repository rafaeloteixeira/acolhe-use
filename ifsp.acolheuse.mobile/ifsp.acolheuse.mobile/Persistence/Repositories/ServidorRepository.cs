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
    public class ServidorRepository : Repository<Servidor>, IServidorRepository
    {
        public ServidorRepository()
            : base(new FirebaseConfigurations.FirebaseAccess())
        {
        }

        public async Task<Servidor> GetByAccountIdAsync(string accountId)
        {
            var query = await CrossCloudFirestore.Current
                                   .Instance
                                   .GetCollection(collectionName)
                                   .WhereEqualsTo("AccountId", accountId)
                                   .GetDocumentsAsync();

            var yourModels = query.ToObjects<Servidor>();
            return yourModels.FirstOrDefault();
        }
    }
}
