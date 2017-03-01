namespace Shop.Infrastructure.Core
{
    using System.Threading.Tasks;

    /// <summary>
    /// Unit of work
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Save changes
        /// </summary>
        /// <returns></returns>
        bool SaveChanges();

        /// <summary>
        /// Save changes async
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveChangesAsync();
    }
}
