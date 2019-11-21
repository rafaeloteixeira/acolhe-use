using ifsp.acolheuse.mobile.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ifsp.acolheuse.mobile.Core.Repositories
{
    public interface ICheckOutRepository : IRepository<CheckOut>
    {
        Task<CheckOut> GetByAppointmentIdDateAsync(string appointmentId, DateTime day);
    }
}
