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
        protected string collectionName;

        public Repository(FirebaseAccess context)
        {
            Context = context;
            collectionName = typeof(TEntity).Name.ToLower();
        }

        public async Task<TEntity> GetAsync(string id)
        {
            try
            {
                var document = await CrossCloudFirestore.Current
                                           .Instance
                                           .GetCollection(collectionName)
                                           .GetDocument(id)
                                           .GetDocumentAsync();

                var yourModel = document.ToObject<TEntity>();
                return yourModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var query = await CrossCloudFirestore.Current
                                               .Instance
                                               .GetCollection(collectionName)
                                               .GetDocumentsAsync();

            var yourModels = query.ToObjects<TEntity>();
            return yourModels;
        }

        public async Task AddAsync(TEntity entity)
        {
            await CrossCloudFirestore.Current.Instance
                                               .GetCollection(collectionName)
                                               .AddDocumentAsync(entity);
        }

        public async Task UpdateAsync(TEntity entity, string id)
        {
            await CrossCloudFirestore.Current.Instance
                                                 .GetCollection(collectionName)
                                                 .GetDocument(id)
                                                 .UpdateDataAsync(entity);
        }

        public async Task AddOrUpdateAsync(TEntity entity, string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                await UpdateAsync(entity, id);
            }
            else
            {
                await AddAsync(entity);
            }
        }

        public async Task RemoveAsync(string id)
        {
            await CrossCloudFirestore.Current
                         .Instance
                         .GetCollection(collectionName)
                         .GetDocument(id)
                         .DeleteDocumentAsync();
        }
    }
}
