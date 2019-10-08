using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace ifsp.acolheuse.mobile.Persistence.Repositories
{
    public class ScheduleActionRepository : Repository<ScheduleAction>, IScheduleActionRepository
    {
        public ScheduleActionRepository()
            : base(new FirebaseConfigurations.FirebaseAccess())
        {
            collectionName = "Schedule";
        }
        public async Task<IEnumerable<ScheduleAction>> GetAppointmentsByIdActionAsync(string idAction)
        {
            if (!String.IsNullOrEmpty(idAction))
            {
                var query = await CrossCloudFirestore.Current
                                       .Instance
                                       .GetCollection(collectionName)
                                       .GetDocument(idAction)
                                       .GetCollection("schedules")
                                       .GetDocumentsAsync();

                var yourModels = query.ToObjects<ScheduleAction>();
                return yourModels;
            }
            else
            {
                return null;
            }
        }


        public async Task<ScheduleAction> GetAppointmentByIdActionEventIdAsync(string idAction, string eventId)
        {
            if (!String.IsNullOrEmpty(idAction) && !String.IsNullOrEmpty(eventId))
            {

                var query = await CrossCloudFirestore.Current
                                                   .Instance
                                                   .GetCollection(collectionName)
                                                   .GetDocument(idAction)
                                                   .GetCollection("schedules")
                                                   .WhereEqualsTo("EventId", eventId)
                                                   .GetDocumentsAsync();

                var yourModels = query.ToObjects<ScheduleAction>();
                return yourModels.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public async Task AddAppointmentByIdActionAsync(string idAction, ScheduleAction entity)
        {
            if (!String.IsNullOrEmpty(idAction) && entity != null)
            {
                await CrossCloudFirestore.Current
                        .Instance
                        .GetCollection(collectionName)
                        .GetDocument(idAction)
                        .GetCollection("schedules")
                        .AddDocumentAsync(entity);
            }
        }

        public async Task DeleteAppointmentByIdActionEventIdAsync(string idAction, string eventId)
        {
            if (!String.IsNullOrEmpty(idAction) && !String.IsNullOrEmpty(eventId))
            {
                var item = await GetAppointmentByIdActionEventIdAsync(idAction, eventId);
                await CrossCloudFirestore.Current
                                        .Instance
                                        .GetCollection(collectionName)
                                        .GetDocument(idAction)
                                        .GetCollection("schedules")
                                        .GetDocument(item.Id)
                                        .DeleteDocumentAsync();
            }
        }
    }
}
