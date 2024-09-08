using LAF.MySQLDatastore;
using LAF.Models.Interfaces.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LAF
{
    namespace Models.Abstract
    {
        /*
         * This is an interesting idea for a generic respository that can potentially deal with any entity type. However, I decided not to use it 
         * because it requires passing entity types from the client. My idea is to completely isolate the database from the client, 
         * so this isn't a good implementation.
         * There might be a good way of making use of it if I had more time ...
        */
        public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
        {
            internal MySQLDbContext context;
            internal DbSet<TEntity> dbSet;

            public GenericRepository(MySQLDbContext context)
            {
                this.context = context;
                this.dbSet = context.Set<TEntity>();
            }

            public virtual IEnumerable<TEntity> Get(
                Expression<Func<TEntity, bool>> filter = null,
                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                string includeProperties = "")
            {
                IQueryable<TEntity> query = dbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (orderBy != null)
                {
                    return orderBy(query).ToList();
                }
                else
                {
                    return query.ToList();
                }
            }

            public virtual TEntity GetByID(object id)
            {
                return dbSet.Find(id);
            }

            public virtual void Insert(TEntity entity)
            {
                dbSet.Add(entity);
            }

            public virtual void Delete(object id)
            {
                TEntity entityToDelete = dbSet.Find(id);
                Delete(entityToDelete);
            }

            public virtual void Delete(TEntity entityToDelete)
            {
                if (context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    dbSet.Attach(entityToDelete);
                }
                dbSet.Remove(entityToDelete);
            }

            public virtual void Update(TEntity entityToUpdate)
            {
                dbSet.Attach(entityToUpdate);
                context.Entry(entityToUpdate).State = EntityState.Modified;
            }

            public Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, bool tracked = true)
            {
                throw new NotImplementedException();
            }

            public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null, bool tracked = true)
            {
                throw new NotImplementedException();
            }

            public Task CreateAsync(TEntity entity)
            {
                throw new NotImplementedException();
            }

            public Task RemoveAsync(TEntity entity)
            {
                throw new NotImplementedException();
            }

            public Task SaveAsync()
            {
                throw new NotImplementedException();
            }
        }
    }
}
