using System.Collections.Generic;

namespace Ship.Infrastructure.Repositories
{
    public interface IRepository<TEntity, in TKey> where TEntity : class
    {
        /// <summary>
        /// Returns all items from the current repository.
        /// </summary>
        /// <returns>All items from the current repository.</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Tries to find an item by the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="key">An unique item identifier.</param>
        /// <returns>An item from the repository or null in case if an item with such <paramref name="key"/> doesn't exist.</returns>
        TEntity Find(TKey key);

        /// <summary>
        /// Adds a new <paramref name="item"/> into the current repository.
        /// </summary>
        /// <param name="item">The item to add.</param>
        void Add(TEntity item);

        /// <summary>
        /// Update the specified <paramref name="item"/>.
        /// </summary>
        /// <param name="item">The item to update.</param>
        void Update(TEntity item);

        /// <summary>
        /// Deletes the <paramref name="item"/> from the current repository.
        /// </summary>
        /// <param name="item">The item to delete.</param>
        void Delete(TEntity item);
    }
}