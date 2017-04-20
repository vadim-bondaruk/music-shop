namespace Shop.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Shop.Infrastructure.Models;
    using Shop.Infrastructure.Repositories;

    /// <summary>
    ///     The models repository.
    /// </summary>
    public class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity, new()
    {
        /// <summary>
        ///     The current Db table.
        /// </summary>
        private readonly IDbSet<TEntity> _currentDbSet;

        /// <summary>
        ///     The Db context.
        /// </summary>
        private readonly DbContext _dbContext;

        /// <summary>
        ///     Indicates whether the inner resources are already disposed.
        /// </summary>
        private bool _disposed;

        /// <summary>
        ///     Indicates whether the repository state was changed.
        /// </summary>
        private bool _stateChanged;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseRepository{TEntity}" /> class.
        /// </summary>
        /// <param name="dbContext">
        ///     The Db context.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     When <paramref name="dbContext" /> is null.
        /// </exception>
        public BaseRepository(DbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            this._dbContext = dbContext;
            this._currentDbSet = this._dbContext.Set<TEntity>();
        }

        /// <summary>
        ///     Gets the current db set.
        /// </summary>
        protected IDbSet<TEntity> CurrentDbSet
        {
            get
            {
                return this._currentDbSet;
            }
        }

        /// <summary>
        ///     Gets the db context.
        /// </summary>
        protected DbContext DbContext
        {
            get
            {
                return this._dbContext;
            }
        }

        /// <summary>
        ///     Adds or updates the specified <paramref name="model" />.
        /// </summary>
        /// <param name="model">
        ///     The model to add or to update.
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
        ///     Adds or updates the specified array of <paramref name="models" />.
        /// </summary>
        /// <param name="models">A model to add or update.</param>
        public virtual void AddOrUpdate(TEntity[] models)
        {
            this._dbContext.Set<TEntity>().AddOrUpdate(models);
        }

        /// <summary>
        ///     Gets the number of entities contained in the repository.
        /// </summary>
        /// <param name="filter">
        ///     The filter.
        /// </param>
        /// <returns>
        ///     The number of entities contained in the repository.
        /// </returns>
        public int Count(Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter == null)
            {
                return this._currentDbSet.Count(x => !x.IsDeleted);
            }

            return this._currentDbSet.Where(x => !x.IsDeleted).Count(filter);
        }

        /// <summary>
        ///     Deletes a model with the specified <paramref name="id" />.
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
        ///     Deletes the <paramref name="model" /> from the repository.
        /// </summary>
        /// <param name="model">
        ///     The model to remove.
        /// </param>
        public virtual void Delete(TEntity model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            this.Delete(model.Id);
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
        ///     Determines whether items which correspond with the specified <paramref name="filter" /> exist in the repository.
        /// </summary>
        /// <param name="filter">
        ///     The filter.
        /// </param>
        /// <returns>
        ///     <b>true</b> if items which correspond with the specified <paramref name="filter" /> exist in the repository;
        ///     otherwise <b>false</b>.
        /// </returns>
        public bool Exist(Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter == null)
            {
                return this._currentDbSet.Any(x => !x.IsDeleted);
            }

            return this._currentDbSet.Where(x => !x.IsDeleted).Any(filter);
        }

        /// <summary>
        ///     Tries to find an entity from the repository using the specified <paramref name="filter" />.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includes">The additional include if needed.</param>
        /// <returns>Entities which correspond to the <paramref name="filter" />.</returns>
        public TEntity FirstOrDefault(
            Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, BaseEntity>>[] includes)
        {
            if (includes == null || includes.Length == 0)
            {
                return this._currentDbSet.FirstOrDefault(filter);
            }

            var query = this.LoadIncludes(this._currentDbSet.Where(filter).Where(x => !x.IsDeleted), includes);
            return query.FirstOrDefault();
        }

        /// <summary>
        ///     Returns all models from the repository.
        /// </summary>
        /// <param name="includes">The additional include if needed.</param>
        /// <returns>
        ///     All models from the repository.
        /// </returns>
        public virtual ICollection<TEntity> GetAll(params Expression<Func<TEntity, BaseEntity>>[] includes)
        {
            var query = this.GetActiveItems(includes);
            return query.ToList();
        }

        /// <summary>
        /// </summary>
        /// <param name="includes">
        /// The includes.
        /// </param>
        /// <returns>
        /// </returns>
        public virtual async Task<List<TEntity>> GetAllAsync(params Expression<Func<TEntity, BaseEntity>>[] includes)
        {
            return await this.GetActiveItems(includes).ToListAsync();
        }

        /// <summary>
        ///     Tries to find models from the repository using the specified <paramref name="filter" />.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includes">The additional include if needed.</param>
        /// <returns>Entities which correspond to the <paramref name="filter" />.</returns>
        public virtual ICollection<TEntity> GetAll(
            Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, BaseEntity>>[] includes)
        {
            var query = this.GetActiveItems(filter, includes);
            return query.ToList();
        }

        /// <summary>
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <param name="includes">
        /// The includes.
        /// </param>
        /// <returns>
        /// </returns>
        public virtual async Task<ICollection<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, BaseEntity>>[] includes)
        {
            return await this.GetActiveItems(filter, includes).ToListAsync();
        }

        /// <summary>
        ///     Returns all entities from the repository using pagination.
        /// </summary>
        /// <param name="page">The page number.</param>
        /// <param name="pageSize">The number of items to return.</param>
        /// <param name="includes">The additional include if needed.</param>
        /// <returns>
        ///     All entities from the repository using pagination.
        /// </returns>
        public PagedResult<TEntity> GetAll(
            int page,
            int pageSize,
            params Expression<Func<TEntity, BaseEntity>>[] includes)
        {
            var query = this.GetActiveItems(includes);
            return GetPagedResultForQuery(query, page, pageSize);
        }

        /// <summary>
        /// </summary>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <param name="includes">
        /// The includes.
        /// </param>
        /// <returns>
        /// </returns>
        public async Task<PagedResult<TEntity>> GetAllAsync(
            int page,
            int pageSize,
            params Expression<Func<TEntity, BaseEntity>>[] includes)
        {
            var query = this.GetActiveItems(includes);
            return await GetPagedResultForQueryAsync(query, page, pageSize);
        }

        /// <summary>
        ///     Tries to find entities from the repository using the specified <paramref name="filter" />.
        /// </summary>
        /// <param name="page">The page number.</param>
        /// <param name="pageSize">The number of items to return.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="includes">The additional include if needed.</param>
        /// <returns>Entities which correspond to the <paramref name="filter" /> using pagination.</returns>
        public PagedResult<TEntity> GetAll(
            int page,
            int pageSize,
            Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, BaseEntity>>[] includes)
        {
            var query = this.GetActiveItems(filter, includes);
            return GetPagedResultForQuery(query, page, pageSize);
        }

        /// <summary>
        /// </summary>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <param name="includes">
        /// The includes.
        /// </param>
        /// <returns>
        /// </returns>
        public async Task<PagedResult<TEntity>> GetAllAsync(
            int page,
            int pageSize,
            Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, BaseEntity>>[] includes)
        {
            var query = this.GetActiveItems(filter, includes);
            return await GetPagedResultForQueryAsync(query, page, pageSize);
        }

        /// <summary>
        ///     Tries to find a model by the specified <paramref name="id" />.
        /// </summary>
        /// <param name="id">
        ///     The model key.
        /// </param>
        /// <param name="includes">The additional include if needed.</param>
        /// <returns>
        ///     A model with the specified <paramref name="id" /> or null in case if there are now models with such
        ///     <paramref name="id" />.
        /// </returns>
        public virtual TEntity GetById(int id, params Expression<Func<TEntity, BaseEntity>>[] includes)
        {
            return this.FirstOrDefault(x => x.Id == id, includes);
        }

        /// <summary>
        ///     Saves all changes.
        /// </summary>
        public void SaveChanges()
        {
            if (this._stateChanged)
            {
                this._dbContext.SaveChanges();
                this._stateChanged = false;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="query">
        ///     The query.
        /// </param>
        /// <param name="page">
        ///     The page.
        /// </param>
        /// <param name="pageSize">
        ///     The page size.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        protected static PagedResult<TEntity> GetPagedResultForQuery(IQueryable<TEntity> query, int page, int pageSize)
        {
            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, @"Incorrect page size specified");
            }

            if (page <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(page), page, @"Incorrect current page value specified");
            }

            var result = query.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return new PagedResult<TEntity>(result, pageSize, page, query.Count());
        }

        /// <summary>
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        protected static async Task<PagedResult<TEntity>> GetPagedResultForQueryAsync(IQueryable<TEntity> query, int page, int pageSize)
        {
            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, @"Incorrect page size specified");
            }

            if (page <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(page), page, @"Incorrect current page value specified");
            }

            var result = query.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return await Task.Run(() => new PagedResult<TEntity>(result, pageSize, page, query.Count()));
        }

        /// <summary>
        ///     Adds the specified <paramref name="model" /> into Db.
        /// </summary>
        /// <param name="model">
        ///     The model.
        /// </param>
        protected virtual void Add(TEntity model)
        {
            // if it is a new model then we have to insert it
            this._currentDbSet.Add(model);
        }

        /// <summary>
        ///     Detaches the navigation property associated with the specified <paramref name="entity" />.
        /// </summary>
        /// <param name="entity">
        ///     The entity.
        /// </param>
        /// <param name="previousEntityState">
        ///     The state of the <paramref name="entity" /> before detach.
        /// </param>
        /// <typeparam name="T">
        ///     The entity type derived from <see cref="BaseEntity" />.
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

        /// <summary>
        ///     Disposes resources in case if <paramref name="disposing" /> is <b>true</b>
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    this._dbContext?.Dispose();
                    this._disposed = true;
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="includes">
        ///     The includes.
        /// </param>
        /// <returns>
        /// </returns>
        protected IQueryable<TEntity> GetActiveItems(params Expression<Func<TEntity, BaseEntity>>[] includes)
        {
            return this.LoadIncludes(this._currentDbSet.Where(x => !x.IsDeleted), includes);
        }

        /// <summary>
        /// </summary>
        /// <param name="filter">
        ///     The filter.
        /// </param>
        /// <param name="includes">
        ///     The includes.
        /// </param>
        /// <returns>
        /// </returns>
        protected IQueryable<TEntity> GetActiveItems(
            Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, BaseEntity>>[] includes)
        {
            return this.LoadIncludes(this._currentDbSet.Where(filter).Where(x => !x.IsDeleted), includes);
        }

        /// <summary>
        ///     Loads additional references.
        /// </summary>
        /// <param name="queryResult">
        ///     The query result.
        /// </param>
        /// <param name="includes"></param>
        protected IQueryable<TEntity> LoadIncludes(
            IQueryable<TEntity> queryResult,
            params Expression<Func<TEntity, BaseEntity>>[] includes)
        {
            foreach (var include in includes)
            {
                queryResult = queryResult.Include(include);
            }

            return queryResult;
        }

        /// <summary>
        ///     Marks the entity with the specified <paramref name="id" /> as deleted.
        /// </summary>
        /// <param name="id">
        ///     The entity id.
        /// </param>
        /// <remarks>
        ///     After calling this method the entity with the specified <paramref name="id" /> will not removed from Db.
        /// </remarks>
        protected virtual void MarkAsDeleted(int id)
        {
            var entity = this.GetById(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                this._dbContext.Entry(entity).State = EntityState.Modified;
                this._stateChanged = true;
            }
        }

        /// <summary>
        ///     Deletes the <paramref name="model" /> from the repository.
        /// </summary>
        /// <param name="model">
        ///     The model to remove.
        /// </param>
        protected virtual void MarkAsDeleted(TEntity model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            this.MarkAsDeleted(model.Id);
        }

        /// <summary>
        ///     Sets the current state to changed mode.
        /// </summary>
        protected void SetStateChanged()
        {
            this._stateChanged = true;
        }

        /// <summary>
        ///     Updates the specified <paramref name="modelFromDb" /> by values from <paramref name="model" />.
        /// </summary>
        /// <param name="modelFromDb">
        ///     The model from db.
        /// </param>
        /// <param name="model">
        ///     The model.
        /// </param>
        protected virtual void Update(TEntity modelFromDb, TEntity model)
        {
            var entry = this._dbContext.Entry(modelFromDb);
            entry.CurrentValues.SetValues(model);
        }
    }
}