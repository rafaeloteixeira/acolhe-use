using ifsp.acolheuse.mobile.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ifsp.acolheuse.mobile.Core.Repositories
{
    public interface IHorarioAcaoRepository
    {
        Task<IEnumerable<HorarioAcao>> GetAtendimentosByIdAcaoAsync(string idAcao);
        Task<HorarioAcao> GetAtendimentoByIdAcaoEventIdAsync(string idAcao, string eventId);
        Task AddAtendimentoByIdAcaoAsync(string idAcao, HorarioAcao entity);
        Task DeleteAtendimentoByIdAcaoEventIdAsync(string idAcao, string eventId);
    }
}
