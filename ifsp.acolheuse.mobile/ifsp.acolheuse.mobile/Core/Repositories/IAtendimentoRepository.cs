using ifsp.acolheuse.mobile.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ifsp.acolheuse.mobile.Core.Repositories
{
    public interface IAtendimentoRepository : IRepository<Atendimento>
    {
        Task<Atendimento> GetAtendimentoByEventIdAcaoIdAsync(string eventId, string idAcao);
        Task<IEnumerable<Atendimento>> GetAllByServidorIdPacienteIdConsultaId(string servidorId, string pacienteId, int tipoConsulta);
    }
}
