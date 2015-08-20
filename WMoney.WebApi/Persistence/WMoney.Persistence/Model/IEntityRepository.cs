using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMoney.Persistence.Model
{
    /// <summary>
    /// Base intefarce for entities repositories
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public interface IEntityRepository<TEntity, TId> where TEntity : class
    {
        void Attach(TEntity entity);

        void Detach(TEntity entity);

        /// <summary>
        /// Add an entity to repository.
        /// </summary>
        /// <param name="entity">Entity to add to repository.</param>
        /// <param name="isNew">Indicates if the entity is new or already exists.</param>
        void Add(TEntity entity, bool isNew);

        /// <summary>
        /// Add an entity to repository asynchronously.
        /// </summary>
        /// <param name="entity">Entity to add to repository.</param>
        /// <param name="isNew">Indicates if the entity is new or already exists.</param>
        Task<TEntity> AddAsync(TEntity entity, bool isNew);

        /// <summary>
        /// Deletes an instance of TEntity.
        /// </summary>
        /// <param name="entity">Entity to delete from repository.</param>
        /// <returns></returns>
        void Delete(TEntity entity);

        /// <summary>
        /// Deletes an instance of TEntity asynchronously.
        /// </summary>
        /// <param name="entity">Entity to delete from repository.</param>
        /// <returns></returns>
        Task<int> DeleteAsync(TEntity entity);

        /// <summary>
        /// Obtains a collection of all TEntity on repository.
        /// </summary>
        /// <returns></returns>
        ICollection<TEntity> GetAll();

        /// <summary>
        /// Obtains a collection of all TEntity on repository.
        /// </summary>
        /// <returns></returns>
        Task<ICollection<TEntity>> GetAllAsync();

        /// <summary>
        /// Obtains a TEntity by its identifier.
        /// </summary>
        /// <param name="id">Identifier from entity to obtain from repository.</param>
        /// <returns></returns>
        TEntity GetByID(TId id, params string[] includeElements);

        /// <summary>
        /// Obtains a TEntity by its identifier.
        /// </summary>
        /// <param name="id">Identifier from entity to obtain from repository.</param>
        /// <returns></returns>
        Task<TEntity> GetByIDAsync(int id);

        /// <summary>
        /// Obtains an IQueryable of TEntity
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> AsQueryable();

        /// <summary>
        /// Obtains an IQueryable of TEntity
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> AsQueryable<TEntity>(IEnumerable<System.Linq.Expressions.Expression<Func<TEntity, object>>> funcs)
            where TEntity : class;

        void Save();
    }
}
