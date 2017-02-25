namespace Shop.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using Infrastructure.Models;
    using Infrastructure.Repositories;

    /// <summary>
    /// The models repository.
    /// </summary>
    public class Repository<TEntity> : IDisposableRepository<TEntity> where TEntity : BaseEntity, new()
    {
        #region Fields

        /// <summary>
        /// The Db context.
        /// </summary>
        private readonly DbContext _dbContext;

        /// <summary>
        /// Indicates whether the repository state was changed.
        /// </summary>
        private bool _stateChanged;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
        /// </summary>
        /// <param name="dbContext">
        /// The Db context.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="dbContext"/> is null.
        /// </exception>
        public Repository(DbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            this._dbContext = dbContext;
        }

        #endregion //Constructors

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
            IQueryable<TEntity> currentTable = this._dbContext.Set<TEntity>();
            return currentTable.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Returns all models from the repository.
        /// </summary>
        /// <returns>
        /// All models from the repository.
        /// </returns>
        public ICollection<TEntity> GetAll()
        {
            IQueryable<TEntity> currentTable = this._dbContext.Set<TEntity>();
            return currentTable.ToList();
        }

        /// <summary>
        /// Tries to find models from the repository using the specified <paramref name="filter"/>.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>Entities which corespond to <paramref name="filter"/>.</returns>
        public ICollection<TEntity> GetAll(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> currentTable = this._dbContext.Set<TEntity>();
            return currentTable.Where(filter).ToList();
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

            // if the model exists in Db then we have to update it
            var originalTrack = this.GetById(model.Id);
            if (originalTrack != null)
            {
                var entry = this._dbContext.Entry(originalTrack);
                entry.CurrentValues.SetValues(model);
                this._stateChanged = true;
            }
            else
            {
                // if it is a new model then we have to insert it
                this._dbContext.Set<TEntity>().Add(model);
                this._stateChanged = true;
            }
        }

        /// <summary>
        /// Deletes a model with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The model key.</param>
        public void Delete(int id)
        {
            var model = this.GetById(id);
            if (model != null)
            {
                Delete(model);
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
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            this._dbContext.Set<TEntity>().Remove(model);
            this._stateChanged = true;
        }

        #endregion //IRepository<TEntity> Members

        #region IDisposableRepository Members

        /// <summary>
        /// Saves all changes.
        /// </summary>
        public void SaveChanges()
        {
            if (this._stateChanged)
            {
                this._dbContext.SaveChanges();
                this._stateChanged = false;
            }
        }

        #endregion //IDisposableRepository Members

        #region IDisposable Pattern

        /// <summary>
        /// Disposes resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes resources in case if <paramref name="disposing"/> is <b>true</b>
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            // to ensure that the changes are not lost
            this.SaveChanges();

            if (disposing)
            {
                this._dbContext?.Dispose();
            }
        }

        #endregion //IDisposable Pattern
    }
}