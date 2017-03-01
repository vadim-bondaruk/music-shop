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
    public interface IRepository : IUnitOfWork, IDisposable
    {
        /// <summary>
        /// Set deleted
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemForUpdate"></param>
        void SetDeleted<T>(T itemForUpdate) where T : class, new();

        /// <summary>
        /// Set added
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemForUpdate"></param>
        void SetAdded<T>(T itemForUpdate) where T : class, new();

        /// <summary>
        /// Set modified
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemForUpdate"></param>
        void SetModified<T>(T itemForUpdate) where T : class, new();

        /// <summary>
        /// Create new entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Create<T>() where T : class, new();

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
        T GetFirstOrNew<T>(out bool isNew) where T : class, new();

        /// <summary>
        /// Get first or new
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="match"></param>
        /// <param name="isNew"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        T GetFirstOrNew<T>(Expression<Func<T, bool>> match, out bool isNew, params Expression<Func<T, object>>[] includes) where T : class, new();

        /// <summary>
        /// Get first or default
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="match"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        T GetFirstOrDefault<T>(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includes) where T : class, new();

        /// <summary>
        /// Get first
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="includes"></param>
        /// <returns></returns>
        T GetFirst<T>(params Expression<Func<T, object>>[] includes) where T : class, new();

        /// <summary>
        /// Get first
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="match"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        T GetFirst<T>(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includes) where T : class, new();

        /// <summary>
        /// Add or update
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        void AddOrUpdate<T>(IEnumerable<T> items) where T : class, new();

        /// <summary>
        /// Add entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        void AddEntity<T>(T entity) where T : class, new();

        /// <summary>
        /// Take all
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="includes"></param>
        /// <returns></returns>
        IEnumerable<T> TakeAll<T>(params Expression<Func<T, object>>[] includes) where T : class, new();

        /// <summary>
        /// Take all async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> TakeAllAsync<T>(params Expression<Func<T, object>>[] includes) where T : class, new();

        /// <summary>
        /// Where
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="match"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        IEnumerable<T> Where<T>(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includes) where T : class, new();

        /// <summary>
        /// Where async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="match"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<ICollection<T>> WhereAsync<T>(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includes) where T : class, new();

        /// <summary>
        /// Find
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        T Find<T>(object[] parameters) where T : class, new();

        /// <summary>
        /// Get first or default async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="match"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<T> GetFirstOrDefaultAsync<T>(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includes) where T : class, new();

        /// <summary>
        /// Get first async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<T> GetFirstAsync<T>(params Expression<Func<T, object>>[] includes) where T : class, new();

        /// <summary>
        /// Get frist async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="match"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<T> GetFirstAsync<T>(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includes) where T : class, new();

        /// <summary>
        /// Paging
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <param name="match"></param>
        /// <param name="orderBy"></param>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> Paging<T, T1>(Expression<Func<T, bool>> match, Expression<Func<T, T1>> orderBy, int? startIndex, int? pageSize, params Expression<Func<T, object>>[] includes) where T : class, new();

        /// <summary>
        /// Does entity exist
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool IsEntityExist<T>(T entity) where T : class, new();
    }
}