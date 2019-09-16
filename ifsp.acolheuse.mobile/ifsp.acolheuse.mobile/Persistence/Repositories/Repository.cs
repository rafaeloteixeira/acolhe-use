using ifsp.acolheuse.mobile.Core.Repositories;
using ifsp.acolheuse.mobile.Persistence.FirebaseConfigurations;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Plugin.CloudFirestore;

namespace ifsp.acolheuse.mobile.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly FirebaseAccess Context;

        public Repository(FirebaseAccess context)
        {
            Context = context;
        }

        public async Task<TEntity> GetAsync(string id)
        {
            return null;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var query = await CrossCloudFirestore.Current
                                               .Instance
                                               .GetCollection("acao")
                                               .GetDocumentsAsync();

            var yourModels = query.ToObjects<TEntity>();
            return yourModels;
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return null;
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return null;
        }

        public void Add(TEntity entity)
        {
            CrossCloudFirestore.Current
                                     .Instance
                                     .GetCollection("acao")
                                     .AddDocument(entity, (error) =>
                                     {
                                         if (error != null)
                                         {
                                             System.Diagnostics.Debug.WriteLine("Firebase erro: " + error);
                                         }
                                     });
        }

        public async void AddAsync(TEntity entity)
        {
            await CrossCloudFirestore.Current
                                      .Instance
                                      .GetCollection("acao")
                                      .AddDocumentAsync(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {

        }

        public void Remove(TEntity entity)
        {

        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
        }
    }
}
