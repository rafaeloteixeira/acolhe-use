using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ifsp.acolheuse.mobile.Persistence.Repositories
{
    public class EstagiarioRepository : Repository<Estagiario>, IEstagiarioRepository
    {
        public EstagiarioRepository()
            : base(new FirebaseConfigurations.FirebaseAccess())
        {

        }

        public async Task<IEnumerable<Estagiario>> GetEstagiariosByResponsavelIdAsync(string idServidor)
        {
            var query = await CrossCloudFirestore.Current
                                               .Instance
                                               .GetCollection(collectionName)
                                                .WhereEqualsTo("ProfessorOrientador/UserId", idServidor)
                                               .GetDocumentsAsync();

            var yourModels = query.ToObjects<Estagiario>();
            return yourModels;
        }
    }
}
