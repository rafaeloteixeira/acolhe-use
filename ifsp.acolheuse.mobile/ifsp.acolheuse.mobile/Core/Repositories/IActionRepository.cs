using ifsp.acolheuse.mobile.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ifsp.acolheuse.mobile.Core.Repositories
{
    public interface IActionRepository : IRepository<ActionModel>
    {
        Task<IEnumerable<ActionModel>> GetAllByIdLineAsync(string idLine);
        Task<ActionModel> GetByGuidAsync(string guidAction);
        Task<IEnumerable<ActionModel>> GetAllByResponsibleId(string responsibleId);
    }
}
