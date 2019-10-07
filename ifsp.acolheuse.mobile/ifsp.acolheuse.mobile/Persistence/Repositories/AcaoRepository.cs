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
    public class AcaoRepository : Repository<Acao>, IAcaoRepository
    {
        public AcaoRepository()
            : base(new FirebaseConfigurations.FirebaseAccess())
        {
        }

        public async Task<IEnumerable<Acao>> GetAllByIdLinhaAsync(string idLinha)
        {
            var query = await CrossCloudFirestore.Current
                                               .Instance
                                               .GetCollection(collectionName)
                                               .WhereEqualsTo("IdLinha", idLinha)
                                               .GetDocumentsAsync();



            var yourModels = query.ToObjects<Acao>();
            return yourModels;
        }

        public async Task<IEnumerable<Acao>> GetAllByServidorId(string servidorId)
        {
            var query = await CrossCloudFirestore.Current
                                                  .Instance
                                                  .GetCollection(collectionName)
                                                  .GetDocumentsAsync();

            var yourModels = query.ToObjects<Acao>().Where(x => x.ResponsavelCollection != null && x.ResponsavelCollection.FirstOrDefault(m => m.Id == servidorId) != null);
            return yourModels;
        }

        public async Task<Acao> GetByGuidAsync(string guidAcao)
        {
            var query = await CrossCloudFirestore.Current
                                   .Instance
                                   .GetCollection(collectionName)
                                   .WhereEqualsTo("GuidAcao", guidAcao)
                                   .GetDocumentsAsync();

            var yourModels = query.ToObjects<Acao>();
            return yourModels.FirstOrDefault();
        }
    }
}
