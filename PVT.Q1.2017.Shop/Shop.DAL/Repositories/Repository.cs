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

        /// <summary>
        /// Indicates whether the inner resources are already disposed.
        /// </summary>
        private bool _disposed;

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

        #region Properties

        /// <summary>
        /// Gets the db context.
        /// </summary>
        protected DbContext DbContext
        {
            get { return this._dbContext; }
        }

        /// <summary>
        /// Gets the current db set.
        /// </summary>
        protected IDbSet<TEntity> CurrentDbSet
        {
            get { return this._currentDbSet; }
        }

        #endregion //Properties

        #region IRepository<TEntity> Members

        /// <summary>
        /// Tries to find a model by the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">
        /// The model key.
        /// </param>
        /// <param name="includes">The additional include if needed.</param>
        /// <returns>
        /// A model with the specified <paramref name="id"/> or null in case if there are now models with such <paramref name="id"/>.
        /// </returns>
        public virtual TEntity GetById(int id, params Expression<Func<TEntity, BaseEntity>>[] includes)
        {
            IQueryable<TEntity> query = this.LoadIncludes(this._currentDbSet.Where(x => x.Id == id), includes);
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Returns all models from the repository.
        /// </summary>
        /// <param name="includes">The additional include if needed.</param>
        /// <returns>
        /// All models from the repository.
        /// </returns>
        public virtual ICollection<TEntity> GetAll(params Expression<Func<TEntity, BaseEntity>>[] includes)
        {
            IQueryable<TEntity> query = this.LoadIncludes(this._currentDbSet, includes);
            return query.ToList();
        }

        /// <summary>
        /// Tries to find models from the repository using the specified <paramref name="filter"/>.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includes">The additional include if needed.</param>
        /// <returns>Entities which corespond to <paramref name="filter"/>.</returns>
        public virtual ICollection<TEntity> GetAll(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, BaseEntity>>[] includes)
        {
            IQueryable<TEntity> query = this._currentDbSet.Where(filter);
            return query.ToList();
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
            var originalModel = this.GetById(model.Id);
            if (originalModel != null)
            {
                this.Update(originalModel, model);
                this._stateChanged = true;
            }
            else
            {
                this.Add(model);
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

        #endregion //IRepository<TEntity> Members

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
        /// Updates the specified <paramref name="modelFromDb"/> by values from <paramref name="model"/>.
        /// </summary>
        /// <param name="modelFromDb">
        /// The model from db.
        /// </param>
        /// <param name="model">
        /// The model.
        /// </param>
        protected virtual void Update(TEntity modelFromDb, TEntity model)
        {
            var entry = this._dbContext.Entry(modelFromDb);
            entry.CurrentValues.SetValues(model);
        }

        /// <summary>
        /// Adds the specified <paramref name="model"/> into Db.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        protected virtual void Add(TEntity model)
        {
            // if it is a new model then we have to insert it
            this._currentDbSet.Add(model);
        }

        /// <summary>
        /// Loads additional references.
        /// </summary>
        /// <param name="queryResult">
        /// The query result.
        /// </param>
        /// <param name="includes"></param>
        protected IQueryable<TEntity> LoadIncludes(IQueryable<TEntity> queryResult, params Expression<Func<TEntity, BaseEntity>>[] includes)
        {
            foreach (var include in includes)
            {
                queryResult = queryResult.Include(include);
            }

            return queryResult;
        }

        /// <summary>
        /// Detaches the navigation property associated with the specified <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <param name="previousEntityState">
        /// The state of the <paramref name="entity"/> before detach.
        /// </param>
        /// <typeparam name="T">
        /// The entity type derived from <see cref="BaseEntity"/>.
        /// </typeparam>
        protected void DetachNavigationProperty<T>(T entity, out EntityState previousEntityState) where T : BaseEntity
        {
            if (entity != null)
            {
                var entry = this._dbContext.Entry(entity);
                previousEntityState = entry.State;
                entry.State = EntityState.Detached;
            }
            else
            {
                previousEntityState = EntityState.Detached;
            }
        }

        #endregion //Protected Methods
    }
}