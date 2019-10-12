using ifsp.acolheuse.mobile.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ifsp.acolheuse.mobile.Core.Repositories
{
    public interface IScheduleActionRepository : IRepository<ScheduleAction>
    {
        Task<IEnumerable<ScheduleAction>> GetAppointmentsByIdActionAsync(string idAction);
        Task<ScheduleAction> GetAppointmentByIdActionEventIdAsync(string idAction, string eventId);
        Task AddAppointmentByIdActionAsync(string idAction, ScheduleAction entity);
        Task DeleteAppointmentByIdActionEventIdAsync(string idAction, string eventId);
    }
}
