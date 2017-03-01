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
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        #region Fields

        private bool _disposed;

        /// <summary>
        /// The Db context.
        /// </summary>
        private readonly DbContext _dbContext;

        /// <summary>
        /// The current Db table.
        /// </summary>
        private readonly IDbSet<TEntity> _currentDbSet;

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
            this._currentDbSet = this._dbContext.Set<TEntity>();
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
        public virtual TEntity GetById(int id)
        {
            IQueryable<TEntity> query = this._currentDbSet.Where(x => x.Id == id);
            return this.LoadAdditionalInfo(query).FirstOrDefault();
        }

        /// <summary>
        /// Returns all models from the repository.
        /// </summary>
        /// <returns>
        /// All models from the repository.
        /// </returns>
        public virtual ICollection<TEntity> GetAll()
        {
            IQueryable<TEntity> query = this._currentDbSet;
            return this.LoadAdditionalInfo(query).ToList();
        }

        /// <summary>
        /// Tries to find models from the repository using the specified <paramref name="filter"/>.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>Entities which corespond to <paramref name="filter"/>.</returns>
        public virtual ICollection<TEntity> GetAll(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = this._currentDbSet.Where(filter);
            return this.LoadAdditionalInfo(query).ToList();
        }

        /// <summary>
        /// Adds or updates the specified <paramref name="model"/>.
        /// </summary>
        /// <param name="model">
        /// The model to add or to update.
        /// </param>
        public virtual void AddOrUpdate(TEntity model)
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
                this._currentDbSet.Add(model);
                this._stateChanged = true;
            }
        }

        /// <summary>
        /// Deletes a model with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The model key.</param>
        public virtual void Delete(int id)
        {
            var model = this.GetById(id);
            if (model != null)
            {
                this._currentDbSet.Remove(model);
                this._stateChanged = true;
            }
        }

        /// <summary>
        /// Deletes the <paramref name="model"/> from the repository.
        /// </summary>
        /// <param name="model">
        /// The model to remove.
        /// </param>
        public virtual void Delete(TEntity model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            Delete(model.Id);
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
            if (!_disposed)
            {
                try
                {
                    // to ensure that the changes are not lost
                    this.SaveChanges();
                }
                catch
                {
                    // no error in Dispose method!!!
                }

                if (disposing)
                {
                    this._dbContext?.Dispose();
                    this._disposed = true;
                }
            }
        }

        #endregion //IDisposable Pattern

        #region Protected Methods

        /// <summary>
        /// Loads additional references.
        /// </summary>
        /// <param name="queryResult">
        /// The query result.
        /// </param>
        protected virtual IQueryable<TEntity> LoadAdditionalInfo(IQueryable<TEntity> queryResult)
        {
            return queryResult;
        }

        #endregion //Protected Methods
    }
}