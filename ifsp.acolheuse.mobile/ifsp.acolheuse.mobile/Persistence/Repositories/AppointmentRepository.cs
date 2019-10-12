﻿using ifsp.acolheuse.mobile.Core.Domain;
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

        public async Task<IEnumerable<Appointment>> GetAllByResponsibleIdPatientId(string responsibleId, string patientId)
        {
            if (!String.IsNullOrEmpty(responsibleId) && !String.IsNullOrEmpty(patientId))
            {
                var query = await CrossCloudFirestore.Current
                         .Instance
                         .GetCollection(collectionName)
                         .GetDocumentsAsync();

                var yourModels = query.ToObjects<Appointment>().Where
                (
                    x => x.IdResponsible == responsibleId
                    && x.Patient.Id == patientId
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
    }
}
