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
        /// Adiciona uma entitade ao repositorio
        /// </summary>
        /// <param name="entity">Entidade que se deseja associar ao repositorio</param>
        /// <param name="isNew">Indica se a entidade é nova ou é um valor ja existente</param>
        void Add(TEntity entity, bool isNew);

        /// <summary>
        /// Remove uma nova instância de TEntity
        /// </summary>
        /// <param name="entity">Entidade que se deseja apagar do repositorio</param>
        /// <returns></returns>
        void Delete(TEntity entity);

        /// <summary>
        /// Obtem uma coleção de todos TEntity no repositório
        /// </summary>
        /// <returns></returns>
        ICollection<TEntity> GetAll();

        /// <summary>
        /// Obtem um TEntity pelo seu identificador
        /// </summary>
        /// <param name="id">Chave primaria da entidade que se deseja obter do repositorio</param>
        /// <returns></returns>
        TEntity GetByID(TId id, params string[] includeElements);

        /// <summary>
        /// Obtem um IQueryable de TEntity
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> AsQueryable();

        /// <summary>
        /// Obtem um IQueryable de TEntity
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> AsQueryable<TEntity>(IEnumerable<System.Linq.Expressions.Expression<Func<TEntity, object>>> funcs)
            where TEntity : class;
    }
}
