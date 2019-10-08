using ifsp.acolheuse.mobile.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ifsp.acolheuse.mobile.Core.Repositories
{
    public interface IInternRepository : IRepository<Intern>
    {
        Task<IEnumerable<Intern>> GetInternsByResponsibleIdAsync(string idResponsible);
    }
}
