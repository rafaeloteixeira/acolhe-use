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
    public class CheckOutRepository : Repository<CheckOut>, ICheckOutRepository
    {
        public CheckOutRepository()
            : base(new FirebaseConfigurations.FirebaseAccess())
        {
            collectionName = "CheckOut";
        }

        public async Task<CheckOut> GetByAppointmentIdDateAsync(string appointmentId, DateTime day)
        {
            if (!String.IsNullOrEmpty(appointmentId))
            {
                var query = await CrossCloudFirestore.Current
                      .Instance
                      .GetCollection(collectionName)
                      .WhereEqualsTo("AppointmentId", appointmentId)
                      .GetDocumentsAsync();

                var yourModels = query.ToObjects<CheckOut>();
                return yourModels.FirstOrDefault(x=>x.Date.ToString("dd/MM/yyyy") == day.ToString("dd/MM/yyyy"));
            }
            else
            {
                return null;
            }
        }
    }
}
