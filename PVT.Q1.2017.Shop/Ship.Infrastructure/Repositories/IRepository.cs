using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Ship.Infrastructure.Repositories
{
    public interface IRepository<TModel, TKey> where TModel : class
    {
        /// <summary>
        /// Returns all models from the current repository.
        /// </summary>
        /// <returns>All models from the current repository.</returns>
        IEnumerable<TModel> GetAll();

        /// <summary>
        /// Returns all models from the current repository which are satisfy the specified <paramref name="filter"/>.
        /// </summary>
        /// <returns>All models from the current repository which are satisfy the specified <paramref name="filter"/>.</returns>
        IEnumerable<TModel> GetAll(Expression filter);

        /// <summary>
        /// Tries to find a model by the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="key">An unique model identifier.</param>
        /// <returns>A model from the repository or null in case if a model with such <paramref name="key"/> doesn't exist.</returns>
        TModel GetById(TKey key);

        /// <summary>
        /// Adds a new <paramref name="model"/> into the current repository.
        /// </summary>
        /// <param name="model">A model to add.</param>
        void Add(Action<TModel> model);

        /// <summary>
        /// Update the specified <paramref name="model"/>.
        /// </summary>
        /// <param name="model">A model to update.</param>
        void Update(Action<TModel> model);

        /// <summary>
        /// Deletes the specified <paramref name="model"/> from the current repository.
        /// </summary>
        /// <param name="model">A model to delete.</param>
        void Delete(Action<TModel> model);

        /// <summary>
        /// Deletes a model by the specified <paramref name="key"/> from the current repository.
        /// </summary>
        /// <param name="key">A model key to indentify a model in the current repository.</param>
        void Delete(Action<TKey> key);
    }
}