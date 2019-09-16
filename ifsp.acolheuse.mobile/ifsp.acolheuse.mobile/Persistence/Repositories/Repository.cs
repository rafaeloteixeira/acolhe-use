using ifsp.acolheuse.mobile.Core.Repositories;
using ifsp.acolheuse.mobile.Persistence.FirebaseConfigurations;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Plugin.CloudFirestore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ifsp.acolheuse.mobile.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly FirebaseAccess Context;
        protected Type typeParameterType;

        public Repository(FirebaseAccess context)
        {
            Context = context;
            typeParameterType = typeof(TEntity);
        }

        public async Task<TEntity> GetAsync(string id)
        {
            var document = await CrossCloudFirestore.Current
                                                    .Instance
                                                    .GetCollection(typeParameterType.Name)
                                                    .GetDocument(id)
                                                    .GetDocumentAsync();

            var yourModel = document.ToObject<TEntity>();
            return yourModel;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var query = await CrossCloudFirestore.Current
                                               .Instance
                                               .GetCollection(typeParameterType.Name)
                                               .GetDocumentsAsync();

            var yourModels = query.ToObjects<TEntity>();
            return yourModels;
        }

        public async Task AddAsync(TEntity entity)
        {
            await CrossCloudFirestore.Current.Instance
                                               .GetCollection(typeParameterType.Name)
                                               .AddDocumentAsync(entity);
        }

        public async Task RemoveAsync(string id)
        {
            await CrossCloudFirestore.Current
                         .Instance
                         .GetCollection(typeParameterType.Name)
                         .GetDocument(id)
                         .DeleteDocumentAsync();
        }
    }
}
