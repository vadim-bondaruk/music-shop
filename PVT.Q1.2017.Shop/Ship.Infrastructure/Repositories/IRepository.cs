namespace Shop.Infrastructure.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Core;

    /// <summary>
    /// Common Repository interface.
    /// </summary>
    /// <typeparam name="TEntity">A model.</typeparam>
    public interface IRepository<T> : IUnitOfWork, IDisposable where T : class, new()
    {
        /// <summary>
        /// Set deleted
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemForUpdate"></param>
        void SetDeleted(T itemForUpdate);

        /// <summary>
        /// Set added
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemForUpdate"></param>
        void SetAdded(T itemForUpdate);

        /// <summary>
        /// Set modified
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemForUpdate"></param>
        void SetModified(T itemForUpdate);

        /// <summary>
        /// Create new entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Create();

        /// <summary>
        /// Create new entity
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        object Create(Type target);

        /// <summary>
        /// Get first or new
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="isNew"></param>
        /// <returns></returns>
        T GetFirstOrNew(out bool isNew);

        /// <summary>
        /// Get first or new
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="match"></param>
        /// <param name="isNew"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        T GetFirstOrNew(Expression<Func<T, bool>> match, out bool isNew, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Get first or default
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="match"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        T GetFirstOrDefault(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Get first
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="includes"></param>
        /// <returns></returns>
        T GetFirst(params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Get first
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="match"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        T GetFirst(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Add or update
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        void AddOrUpdate(IEnumerable<T> items);

        /// <summary>
        /// Add entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        void AddEntity(T entity);

        /// <summary>
        /// Take all
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="includes"></param>
        /// <returns></returns>
        IEnumerable<T> TakeAll(params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Take all async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> TakeAllAsync(params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Where
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="match"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        IEnumerable<T> Where(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Where async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="match"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<ICollection<T>> WhereAsync(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Find
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        T Find(object[] parameters);

        /// <summary>
        /// Get first or default async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="match"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Get first async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<T> GetFirstAsync(params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Get frist async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="match"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<T> GetFirstAsync(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Paging
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="match"></param>
        /// <param name="orderBy"></param>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> Paging<T1>(Expression<Func<T, bool>> match, Expression<Func<T, T1>> orderBy, int? startIndex, int? pageSize, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Does entity exist
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool IsEntityExist(T entity);
    }
}