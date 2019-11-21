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
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository()
            : base(new FirebaseConfigurations.FirebaseAccess())
        {
            collectionName = "Appointment";
        }

        public async Task<IEnumerable<Appointment>> GetAllByResponsibleId(string responsibleId)
        {
            if (!String.IsNullOrEmpty(responsibleId))
            {
                var query = await CrossCloudFirestore.Current
                         .Instance
                         .GetCollection(collectionName)
                         .GetDocumentsAsync();

                var yourModels = query.ToObjects<Appointment>().Where
                (
                    x => x.IdResponsible == responsibleId
                );

                return yourModels;
            }
            else
            {
                return null;
            }
        }

        public async Task<Appointment> GetAppointmentByEventIdActionIdAsync(string eventId, string actionId, string patientId)
        {
            if (!String.IsNullOrEmpty(eventId) && !String.IsNullOrEmpty(actionId))
            {
                var query = await CrossCloudFirestore.Current
                                             .Instance
                                             .GetCollection(collectionName)
                                             .GetDocumentsAsync();

                var yourModel = query.ToObjects<Appointment>().FirstOrDefault(x => x.EventId == eventId && x.IdAction == actionId && x.Patient.Id == patientId);
                return yourModel;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Appointment>> GetAllByInternId(string internId)
        {
            if (!String.IsNullOrEmpty(internId))
            {
                var query = await CrossCloudFirestore.Current
                           .Instance
                           .GetCollection(collectionName)
                           .GetDocumentsAsync();

                var yourModels = query.ToObjects<Appointment>().Where
                (
                x => x.InternsIdCollection != null
                && x.InternsIdCollection.Contains(internId)
                );

                return yourModels;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Appointment>> GetAllByInternIdStartTime(string internId, DateTime startTime)
        {
            if (!String.IsNullOrEmpty(internId))
            {
                var query = await CrossCloudFirestore.Current
                           .Instance
                           .GetCollection(collectionName)
                           .GetDocumentsAsync();

                var yourModels = query.ToObjects<Appointment>().Where(
                x => x.InternsIdCollection != null &&
                x.InternsIdCollection.Contains(internId) &&
                ((x.StartTime.ToString("dd/MM/yyyy") == startTime.ToString("dd/MM/yyyy") && x.ConsultationType == Appointment._ORIENTACAO)
                ||
                (x.StartTime.DayOfWeek == startTime.DayOfWeek &&
                (x.ConsultationType == Appointment._GRUPO || x.ConsultationType == Appointment._INDIVIDUAL)))
                );

              

                return yourModels;
            }
            else
            {
                return null;
            }
        }
    }
}
