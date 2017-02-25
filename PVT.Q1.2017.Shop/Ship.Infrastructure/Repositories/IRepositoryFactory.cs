namespace Shop.Infrastructure.Repositories
{
    using Models;

    /// <summary>
    /// The repository factory
    /// </summary>
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Returns the repository for the specified model.
        /// </summary>
        /// <typeparam name="TEntity">
        /// A model.
        /// </typeparam>
        /// <returns>
        /// Returns the repository for the specified model.
        /// </returns>
        IDisposableRepository<TEntity> CreateRepository<TEntity>() where TEntity : BaseEntity, new();
    }
}