namespace Shop.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using Context;
    using Infrastructure.Models;
    using Infrastructure.Repositories;

    /// <summary>
    /// The models repository.
    /// </summary>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        #region IRepository<TEntity> Members

        /// <summary>
        /// Tries to find a model by the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">
        /// The model key.
        /// </param>
        /// <returns>
        /// A model with the specified <paramref name="id"/> or null in case if there are now models with such <paramref name="id"/>.
        /// </returns>
        public TEntity GetById(int id)
        {
            using (var dbContext = new ShopContext())
            {
                var currentTable = dbContext.Set<TEntity>();
                return currentTable.Find(id);
            }
        }

        /// <summary>
        /// Returns all models from the repository.
        /// </summary>
        /// <returns>
        /// All models from the repository.
        /// </returns>
        public ICollection<TEntity> GetAll()
        {
            using (var dbContext = new ShopContext())
            {
                var currentTable = dbContext.Set<TEntity>();
                return currentTable.ToList();
            }
        }

        /// <summary>
        /// Tries to find models from the repository using the specified <paramref name="filter"/>.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>Entities which corespond to <paramref name="filter"/>.</returns>
        public ICollection<TEntity> GetAll(Expression<Func<TEntity, bool>> filter)
        {
            using (var dbContext = new ShopContext())
            {
                var currentTable = dbContext.Set<TEntity>();
                return currentTable.Where(filter).ToList();
            }
        }

        /// <summary>
        /// Adds or updates the specified <paramref name="model"/>.
        /// </summary>
        /// <param name="model">
        /// The model to add or to update.
        /// </param>
        public void AddOrUpdate(TEntity model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            using (var dbContext = new ShopContext())
            {
                var currentTable = dbContext.Set<TEntity>();

                if (model.Id > 0)
                {
                    var originalTrack = currentTable.Find(model.Id);

                    // if the model exists in Db
                    if (originalTrack != null)
                    {
                        var entry = dbContext.Entry(originalTrack);
                        entry.CurrentValues.SetValues(model);
                        entry.State = EntityState.Modified;
                        dbContext.SaveChanges();
                        return;
                    }
                }

                // if it is a new model
                currentTable.Add(model);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes a model with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The model key.</param>
        public void Delete(int id)
        {
            using (var dbContext = new ShopContext())
            {
                var currentTable = dbContext.Set<TEntity>();
                var model = currentTable.Find(id);
                if (model != null)
                {
                    currentTable.Remove(model);
                    dbContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Deletes the <paramref name="model"/> from the repository.
        /// </summary>
        /// <param name="model">
        /// The model to remove.
        /// </param>
        public void Delete(TEntity model)
        {
            using (var dbContext = new ShopContext())
            {
                if (model == null)
                {
                    throw new ArgumentNullException(nameof(model));
                }

                var currentTable = dbContext.Set<TEntity>();
                currentTable.Remove(model);
                dbContext.SaveChanges();
            }
        }

        #endregion //IRepository<TEntity> Members
    }
}