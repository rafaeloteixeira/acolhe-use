﻿using ifsp.acolheuse.mobile.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ifsp.acolheuse.mobile.Core.Repositories
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        Task<Appointment> GetAppointmentByEventIdActionIdAsync(string eventId, string idAction);
        Task<IEnumerable<Appointment>> GetAllByResponsibleIdPatientIdConsultationId(string responsibleId, string patientId, int consultationType);
        Task<IEnumerable<Appointment>> GetAllByInternId(string internId);
    }
}
