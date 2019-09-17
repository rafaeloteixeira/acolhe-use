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
    public class HorarioAcaoRepository : Repository<Atendimento>, IHorarioAcaoRepository
    {
        public HorarioAcaoRepository()
            : base(new FirebaseConfigurations.FirebaseAccess())
        {
        }
        public async Task<IEnumerable<Atendimento>> GetAtendimentosByIdAcaoAsync(string idAcao)
        {
            var query = await CrossCloudFirestore.Current
                                               .Instance
                                               .GetCollection(collectionName)
                                               .GetDocument(idAcao)
                                               .GetCollection("horarios")
                                               .GetDocumentsAsync();

            var yourModels = query.ToObjects<Atendimento>();
            return yourModels;
        }


        public async Task<Atendimento> GetAtendimentoByIdAcaoEventIdAsync(string idAcao, string eventId)
        {
            var document = await CrossCloudFirestore.Current
                                               .Instance
                                               .GetCollection(collectionName)
                                               .GetDocument(idAcao)
                                               .GetCollection("horarios")
                                               .GetDocument(eventId)
                                               .GetDocumentAsync();

            var yourModel = document.ToObject<Atendimento>();
            return yourModel;
        }

        public async Task AddAtendimentoByIdAcaoAsync(string idAcao, Atendimento entity)
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

            await CrossCloudFirestore.Current
                                    .Instance
                                    .GetCollection(collectionName)
                                    .GetDocument(idAcao)
                                    .GetCollection("horarios")
                                    .GetDocument(eventId)
                                    .DeleteDocumentAsync();

        }
    }
}
