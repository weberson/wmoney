using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMoney.Persistence.Model;

namespace WMoney.Persistence.EntityFramework.Repositories
{
    /// <summary>
    /// Base class to implement the entity repositories with EF
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public abstract class RepositoryBase<TEntity, TId> : IEntityRepository<TEntity, TId> where TEntity : class
    {
        #region Constructor

        /// <summary>
        /// Initializes the repository
        /// </summary>
        /// <param name="context">Current context</param>
        public RepositoryBase(WMoneyContext context)
        {
            Context = context;
        }

        #endregion

        public WMoneyContext Context { get; private set; }

        #region IEntityRepository<TEntity,TId> Members

        public void Attach(TEntity entity)
        {
            var objectContext = (Context as IObjectContextAdapter).ObjectContext;
            Context.Set<TEntity>().Attach(entity);
        }

        public void Detach(TEntity entity)
        {
            var objectContext = (Context as IObjectContextAdapter).ObjectContext;
            objectContext.Detach(entity);
        }

        /// <summary>
        /// Add an entity to the repository
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Add(TEntity entity, bool isNew)
        {
            if (Context.Entry<TEntity>(entity).State == EntityState.Detached)
            {
                Context.Set<TEntity>().Attach(entity);
            }

            if (isNew)
            {
                Context.Entry<TEntity>(entity).State = EntityState.Added;
            }
            else
            {
                Context.Entry<TEntity>(entity).State = EntityState.Modified;
            }
        }

        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        /// <summary>
        /// Returns all entities from repository
        /// </summary>
        /// <returns></returns>
        public virtual ICollection<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        /// <summary>
        /// Obtains an entity by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract TEntity GetByID(TId id, params string[] includeElements);

        /// <summary>
        /// Returns an IQueryable typed by entity
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> AsQueryable()
        {
            return Context.Set<TEntity>();
        }

        /// <summary>
        /// Returns an IQueryable typed by entity, including the passed properties
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> AsQueryable<TEntity>(IEnumerable<System.Linq.Expressions.Expression<Func<TEntity, object>>> funcs)
            where TEntity : class
        {
            var result = Context.Set<TEntity>().AsQueryable();

            foreach (var func in funcs)
            {
                result = result.Include(func);
            }

            return result;
        }

        #endregion
    }
}
