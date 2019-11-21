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
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        public PatientRepository()
            : base(new FirebaseConfigurations.FirebaseAccess())
        {
            collectionName = "Patient";
        }
        public async Task<IEnumerable<Patient>> GetAllByActionIdAsync(string actionId)
        {
            if (!String.IsNullOrEmpty(actionId))
            {

                var query = await CrossCloudFirestore.Current
                                       .Instance
                                       .GetCollection(collectionName)
                                       .GetDocumentsAsync();

                var yourModels = query.ToObjects<Patient>();
                return yourModels.Where(x => x.ActionCollection != null && x.ActionCollection.FirstOrDefault(y => y.Id == actionId) != null);
            }
            else
            {
                return null;
            }
        }
    }
}
