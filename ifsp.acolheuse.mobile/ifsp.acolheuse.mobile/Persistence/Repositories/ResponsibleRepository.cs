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
    public class ResponsibleRepository : Repository<Responsible>, IResponsibleRepository
    {
        public ResponsibleRepository()
            : base(new FirebaseConfigurations.FirebaseAccess())
        {
            collectionName = "Responsible";
        }

        public async Task<Responsible> GetByAccessTokenAsync(string accountId)
        {
            if (!String.IsNullOrEmpty(accountId))
            {

                var query = await CrossCloudFirestore.Current
                                       .Instance
                                       .GetCollection(collectionName)
                                       .WhereEqualsTo("AccessToken", accountId)
                                       .GetDocumentsAsync();

                var yourModels = query.ToObjects<Responsible>();
                return yourModels.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
    }
}
