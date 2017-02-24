// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Repository.cs" company="PVT.Q1.2017">
//   PVT.Q1.2017
// </copyright>
// <summary>
//   The models repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Shop.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    using Shop.Infrastructure.Models;
    using Shop.Infrastructure.Repositories;

    /// <summary>
    ///     The models repository.
    /// </summary>
    public class Repository<TEntity> : IRepository<TEntity>, IDisposable
        where TEntity : BaseEntity, new()
    {
        /// <summary>
        ///     The Db context.
        /// </summary>
        private readonly DbContext _dbContext;

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="Shop.DAL.Repositories.Repository`1" /> class.
        /// </summary>
        /// <param name="dbContext">The Db context.</param>
        /// <exception cref="System.ArgumentNullException">
        ///     When <paramref name="dbContext" /> is null.
        /// </exception>
        public Repository(DbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            this._dbContext = dbContext;
        }

        /// <summary>
        ///     Adds or updates the specified <paramref name="model" /> .
        /// </summary>
        /// <param name="model">The model to add or to update.</param>
        public void AddOrUpdate(TEntity model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var currentTable = this._dbContext.Set<TEntity>();

            if (model.Id > 0)
            {
                var originalTrack = currentTable.Find(model.Id);

                // if the model exists in Db
                if (originalTrack != null)
                {
                    var entry = this._dbContext.Entry(originalTrack);
                    entry.CurrentValues.SetValues(model);
                    entry.State = EntityState.Modified;
                    return;
                }
            }

            // if it is a new model
            currentTable.Add(model);
        }

        /// <summary>
        ///     Deletes a model with the specified <paramref name="id" /> .
        /// </summary>
        /// <param name="id">The model key.</param>
        public void Delete(int id)
        {
            var currentTable = this._dbContext.Set<TEntity>();
            var model = currentTable.Find(id);
            if (model != null)
            {
                currentTable.Remove(model);
            }
        }

        /// <summary>
        ///     Deletes the <paramref name="model" /> from the repository.
        /// </summary>
        /// <param name="model">The model to remove.</param>
        public void Delete(TEntity model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var currentTable = this._dbContext.Set<TEntity>();
            currentTable.Remove(model);
        }

        /// <summary>
        ///     Disposes resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Returns all models from the repository.
        /// </summary>
        /// <returns>
        ///     All models from the repository.
        /// </returns>
        public ICollection<TEntity> GetAll()
        {
            var currentTable = this._dbContext.Set<TEntity>();
            return currentTable.ToList();
        }

        /// <summary>
        ///     Tries to find models from the repository using the specified
        ///     <paramref name="filter" /> .
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>
        ///     Entities which corespond to <paramref name="filter" /> .
        /// </returns>
        public ICollection<TEntity> GetAll(Expression<Func<TEntity, bool>> filter)
        {
            var currentTable = this._dbContext.Set<TEntity>();
            return currentTable.Where(filter).ToList();
        }

        /// <summary>
        ///     Tries to find a model by the specified <paramref name="id" /> .
        /// </summary>
        /// <param name="id">The model key.</param>
        /// <returns>
        ///     A model with the specified <paramref name="id" /> or
        ///     <see langword="null" /> in case if there are now models with such
        ///     <paramref name="id" /> .
        /// </returns>
        public TEntity GetById(int id)
        {
            var currentTable = this._dbContext.Set<TEntity>();
            return currentTable.Find(id);
        }

        /// <summary>
        ///     Disposes resources in case if <paramref name="disposing" /> is
        ///     <b>
        ///         <see langword="true" />
        ///     </b>
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._dbContext?.Dispose();
            }
        }
    }
}