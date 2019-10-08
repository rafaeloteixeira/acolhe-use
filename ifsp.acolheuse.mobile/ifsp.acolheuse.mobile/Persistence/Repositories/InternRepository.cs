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
    public class InternRepository : Repository<Intern>, IInternRepository
    {
        public InternRepository()
            : base(new FirebaseConfigurations.FirebaseAccess())
        {
            collectionName = "Intern";
        }

        public async Task<IEnumerable<Intern>> GetInternsByResponsibleIdAsync(string idResponsible)
        {
            if (!String.IsNullOrEmpty(idResponsible))
            {
                var query = await CrossCloudFirestore.Current
                                   .Instance
                                   .GetCollection(collectionName)
                                    .WhereEqualsTo("IdResponsible", idResponsible)
                                   .GetDocumentsAsync();

                var yourModels = query.ToObjects<Intern>();
                return yourModels;
            }
            else
            {
                return null;
            }
        }
        public async Task<Intern> GetByAccessTokenAsync(string accountId)
        {
            if (!String.IsNullOrEmpty(accountId))
            {

                var query = await CrossCloudFirestore.Current
                                       .Instance
                                       .GetCollection(collectionName)
                                       .WhereEqualsTo("AccessToken", accountId)
                                       .GetDocumentsAsync();

                var yourModels = query.ToObjects<Intern>();
                return yourModels.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
    }
}
