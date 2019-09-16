using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
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
                                                .WhereEqualsTo("Linha/Id", idLinha)
                                               .GetDocumentsAsync();

            var yourModels = query.ToObjects<Acao>();
            return yourModels;
        }
    }
}
