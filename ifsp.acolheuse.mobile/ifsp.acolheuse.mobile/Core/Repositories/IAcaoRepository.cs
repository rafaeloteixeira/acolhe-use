using ifsp.acolheuse.mobile.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ifsp.acolheuse.mobile.Core.Repositories
{
    public interface IAcaoRepository : IRepository<Acao>
    {
        Task<IEnumerable<Acao>> GetAllByIdLinhaAsync(string idLinha);
    }
}
