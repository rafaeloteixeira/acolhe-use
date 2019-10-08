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
    public class ActionRepository : Repository<ActionModel>, IActionRepository
    {
        public ActionRepository()
            : base(new FirebaseConfigurations.FirebaseAccess())
        {
            collectionName = "Action";
        }

        public async Task<IEnumerable<ActionModel>> GetAllByIdLineAsync(string idLine)
        {
            if (!String.IsNullOrEmpty(idLine))
            {
                var query = await CrossCloudFirestore.Current
                                        .Instance
                                        .GetCollection(collectionName)
                                        .WhereEqualsTo("IdLine", idLine)
                                        .GetDocumentsAsync();



                var yourModels = query.ToObjects<ActionModel>();
                return yourModels;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<ActionModel>> GetAllByResponsibleId(string responsibleId)
        {
            if (!String.IsNullOrEmpty(responsibleId))
            {
                var query = await CrossCloudFirestore.Current
                                      .Instance
                                      .GetCollection(collectionName)
                                      .GetDocumentsAsync();

                var yourModels = query.ToObjects<ActionModel>().Where(x => x.ResponsibleCollection != null && x.ResponsibleCollection.FirstOrDefault(m => m.Id == responsibleId) != null);
                return yourModels;
            }
            else
            {
                return null;
            }
        }

        public async Task<ActionModel> GetByGuidAsync(string guidAction)
        {
            if (!String.IsNullOrEmpty(guidAction))
            {
                var query = await CrossCloudFirestore.Current
                                       .Instance
                                       .GetCollection(collectionName)
                                       .WhereEqualsTo("GuidAction", guidAction)
                                       .GetDocumentsAsync();

                var yourModels = query.ToObjects<ActionModel>();
                return yourModels.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
    }
}
