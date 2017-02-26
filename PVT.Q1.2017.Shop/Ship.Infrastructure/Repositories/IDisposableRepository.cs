namespace Shop.Infrastructure.Repositories
{
    using System;
    using Models;

    /// <summary>
    /// Common Repository interface witch uses resources.
    /// </summary>
    /// <typeparam name="TEntity">
    /// A model.
    /// </typeparam>
    public interface IDisposableRepository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : BaseEntity, new()
    {
        /// <summary>
        /// Saves all changes.
        /// </summary>
        void SaveChanges();
    }
}