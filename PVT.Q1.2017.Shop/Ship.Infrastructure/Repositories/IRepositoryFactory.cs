namespace Shop.Infrastructure.Repositories
{
    using Models;

    /// <summary>
    /// The repository factory
    /// </summary>
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Returns the instance of the specified repository for the specified model type.
        /// </summary>
        /// <typeparam name="TEntity">
        /// A model type.
        /// </typeparam>
        /// <typeparam name="TRepository">A model repository type.</typeparam>
        /// <returns>
        /// A repository for the specified model.
        /// </returns>
        TRepository CreateRepository<TRepository>();
    }
}