namespace Shop.Infrastructure.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Models;

    /// <summary>
    /// Common Repository interface.
    /// </summary>
    /// <typeparam name="TEntity">A model.</typeparam>
    public interface IRepository<TEntity> : IDisposable where TEntity : BaseEntity, new()
    {
        /// <summary>
        /// Tries to find an entity by the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The key.</param>
        /// <param name="includes">The additional include if needed.</param>
        /// <returns>
        /// An entity with the specified <paramref name="id"/> or null in case if there are now enity with such <paramref name="id"/>
        /// </returns>
        TEntity GetById(int id, params Expression<Func<TEntity, BaseEntity>>[] includes);

        /// <summary>
        /// Returns all entities from the repository.
        /// </summary>
        /// <param name="includes">The additional include if needed.</param>
        /// <returns>
        /// All entities from the repository.
        /// </returns>
        ICollection<TEntity> GetAll(params Expression<Func<TEntity, BaseEntity>>[] includes);

        /// <summary>
        /// Tries to find entities from the repository using the specified <paramref name="filter"/>.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includes">The additional include if needed.</param>
        /// <returns>Entities which corespond to </returns>
        ICollection<TEntity> GetAll(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, BaseEntity>>[] includes);

        /// <summary>
        /// Adds or updates the specified <paramref name="model"/>.
        /// </summary>
        /// <param name="model">A model to add or update.</param>
        void AddOrUpdate(TEntity model);

        /// <summary>
        /// Adds or updates the specified array of <paramref name="models"/>.
        /// </summary>
        /// <param name="models">Models to add or update.</param>
        void AddOrUpdate(TEntity[] models);

        /// <summary>
        /// Deletes a model with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The model key.</param>
        void Delete(int id);

        /// <summary>
        /// Deletes the <paramref name="model"/> from the repository.
        /// </summary>
        /// <param name="model">The model to delete.</param>
        void Delete(TEntity model);

        /// <summary>
        /// Saves all changes.
        /// </summary>
        void SaveChanges();
    }
}