using ifsp.acolheuse.mobile.Core.Domain;
using ifsp.acolheuse.mobile.Core.Repositories;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
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
        public async Task RemoveByServidorAsync(string id)
        {
            await CrossCloudFirestore.Current
                         .Instance
                         .GetCollection(collectionName)
                         .GetDocument(id)
                         .DeleteDocumentAsync();
        }
    }
}
