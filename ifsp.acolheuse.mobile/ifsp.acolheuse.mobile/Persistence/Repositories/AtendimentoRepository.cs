using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ifsp.acolheuse.mobile.Persistence.Repositories
{
    public class AtendimentoRepository : Repository<Atendimento>, IAtendimentoRepository
    {
        public AtendimentoRepository()
            : base(new FirebaseConfigurations.FirebaseAccess())
        {
        }

        public async Task<Atendimento> GetAtendimentoByEventIdAcaoIdAsync(string eventId, string acaoId)
        {
            var query = await CrossCloudFirestore.Current
                                        .Instance
                                        .GetCollection(collectionName)
                                        .GetDocumentsAsync();

            var yourModel = query.ToObjects<Atendimento>().FirstOrDefault(x => x.EventId == eventId && x.IdAcao == acaoId);
            return yourModel;
        }

    }
}
