using ifsp.acolheuse.mobile.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ifsp.acolheuse.mobile.Core.Repositories
{
    public interface IHorarioAcaoRepository
    {
        Task<IEnumerable<Atendimento>> GetAtendimentosByIdAcaoAsync(string idAcao);
        Task<Atendimento> GetAtendimentoByIdAcaoEventIdAsync(string idAcao, string eventId);
        Task AddAtendimentoByIdAcaoAsync(string idAcao, Atendimento entity);
        Task DeleteAtendimentoByIdAcaoEventIdAsync(string idAcao, string eventId);
    }
}
