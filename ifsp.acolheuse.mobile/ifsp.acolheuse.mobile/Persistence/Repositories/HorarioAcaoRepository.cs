using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace ifsp.acolheuse.mobile.Persistence.Repositories
{
    public class HorarioAcaoRepository : Repository<HorarioAcao>, IHorarioAcaoRepository
    {
        public HorarioAcaoRepository()
            : base(new FirebaseConfigurations.FirebaseAccess())
        {
        }
        public async Task<IEnumerable<HorarioAcao>> GetAtendimentosByIdAcaoAsync(string idAcao)
        {
            var query = await CrossCloudFirestore.Current
                                               .Instance
                                               .GetCollection(collectionName)
                                               .GetDocument(idAcao)
                                               .GetCollection("horarios")
                                               .GetDocumentsAsync();

            var yourModels = query.ToObjects<HorarioAcao>();
            return yourModels;
        }


        public async Task<HorarioAcao> GetAtendimentoByIdAcaoEventIdAsync(string idAcao, string eventId)
        {
            var query = await CrossCloudFirestore.Current
                                               .Instance
                                               .GetCollection(collectionName)
                                               .GetDocument(idAcao)
                                               .GetCollection("horarios")
                                               .WhereEqualsTo("EventId", eventId)
                                               .GetDocumentsAsync();

            var yourModels = query.ToObjects<HorarioAcao>();
            return yourModels.FirstOrDefault();
        }

        public async Task AddAtendimentoByIdAcaoAsync(string idAcao, HorarioAcao entity)
        {

            await CrossCloudFirestore.Current
                                    .Instance
                                    .GetCollection(collectionName)
                                    .GetDocument(idAcao)
                                    .GetCollection("horarios")
                                    .AddDocumentAsync(entity);

        }
        public async Task DeleteAtendimentoByIdAcaoEventIdAsync(string idAcao, string eventId)
        {
            var item = await GetAtendimentoByIdAcaoEventIdAsync(idAcao, eventId);
            await CrossCloudFirestore.Current
                                    .Instance
                                    .GetCollection(collectionName)
                                    .GetDocument(idAcao)
                                    .GetCollection("horarios")
                                    .GetDocument(item.Id)
                                    .DeleteDocumentAsync();
        }
    }
}
