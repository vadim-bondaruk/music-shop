using Shop.Infrastructure.Core;
using Shop.Infrastructure.Models;
using Shop.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shop.DAL
{
    public class Repository<T> : BaseDisposable, IRepository<T> where T : BaseEntity, new()
    {
        private DbContext _dbContext;
        public Repository(DbContext context)
        {
            _dbContext = context;
        }

        #region IRepository implementation

        public T GetFirstOrNew(out bool isNew)
        {
            var first = _dbContext.Set<T>().FirstOrDefault();
            isNew = first == null;
            return isNew ? _dbContext.Set<T>().Create() : first;

            
        }
        public T GetFirstOrDefault(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includes)
        {
            return _dbContext.Set<T>().IncludeMultiple(includes).FirstOrDefault(match);
        }
        public T GetFirst(params Expression<Func<T, object>>[] includes)
        {
            return _dbContext.Set<T>().IncludeMultiple(includes).First();
        }
        public void AddOrUpdate(IEnumerable<T> items)
        {
            var array = items as T[] ?? items.ToArray();
            _dbContext.Set<T>().AddOrUpdate(array);
        }
        void AddEntity(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }
        public IEnumerable<T> TakeAll(params Expression<Func<T, object>>[] includes)
        {
            return _dbContext.Set<T>().IncludeMultiple(includes);
        }
        public IEnumerable<T> Where(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includes)
        {
            return _dbContext.Set<T>().Where(match).IncludeMultiple(includes);
        }
        public T Find(object[] parameters)
        {
            return _dbContext.Set<T>().Find(parameters);
        }
        public bool SaveChanges()
        {
            _dbContext.ChangeTracker.DetectChanges();
            return _dbContext.SaveChanges() > 0;
        }
        public T Create()
        {
            return _dbContext.Set<T>().Create();
        }
        public async Task<ICollection<T>> WhereAsync(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includes)
        {
            return await _dbContext.Set<T>().IncludeMultiple(includes).Where(match).ToListAsync();
        }
        public async Task<bool> SaveChangesAsync()
        {
            _dbContext.ChangeTracker.DetectChanges();
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includes)
        {
            return await _dbContext.Set<T>().IncludeMultiple(includes).FirstOrDefaultAsync(match);
        }
        public async Task<T> GetFirstAsync(params Expression<Func<T, object>>[] includes)
        {
            return await _dbContext.Set<T>().IncludeMultiple(includes).FirstAsync();
        }
        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includes)
        {
            return await _dbContext.Set<T>().IncludeMultiple(includes).FirstAsync(match);
        }
        public async Task<IEnumerable<T>> TakeAllAsync(params Expression<Func<T, object>>[] includes)
        {
            return await _dbContext.Set<T>().IncludeMultiple(includes).ToListAsync();
        }
        public async Task<IEnumerable<T>> Paging<T, T1>(Expression<Func<T, bool>> match, Expression<Func<T, T1>> orderBy, int? startIndex, int? pageSize, params Expression<Func<T, object>>[] includes) where T : class, new()
        {
            if (startIndex.HasValue && pageSize.HasValue)
            {
                return await _dbContext.Set<T>().Where(match).OrderBy(orderBy).Skip(startIndex.Value).Take(pageSize.Value).IncludeMultiple(includes).ToListAsync();
            }
            return await _dbContext.Set<T>().Where(match).OrderBy(orderBy).ToListAsync();
        }
        public void SetModified(T itemForUpdate)
        {
            if (_dbContext.Entry(itemForUpdate).State == EntityState.Detached)
            {
                _dbContext.Set<T>().Attach(itemForUpdate);
            }

            _dbContext.Entry(itemForUpdate).State = EntityState.Modified;
        }
        public void SetDeleted(T itemForUpdate)
        {
            _dbContext.Entry(itemForUpdate).State = EntityState.Deleted;
        }
        public void SetAdded(T itemForUpdate)
        {
            _dbContext.Entry(itemForUpdate).State = EntityState.Added;
        }
        public T GetFirstOrNew(Expression<Func<T, bool>> match, out bool isNew, params Expression<Func<T, object>>[] includes)
        {
            var result = _dbContext.Set<T>().IncludeMultiple(includes).FirstOrDefault(match);
            isNew = result == null;
            return result ?? new T();
        }
        public bool IsEntityExist(T entity)
        {
            object value;
            var dbContext = _dbContext as IObjectContextAdapter;
            var objectSet = dbContext.ObjectContext.CreateObjectSet<T>().EntitySet;
            var entityKeyValues = (from member in objectSet.ElementType.KeyMembers
                                   let info = entity.GetType().GetProperty(member.Name)
                                   let tempValue = info.GetValue(entity, null)
                                   select new KeyValuePair<string, object>(member.Name, tempValue)).ToList();

            var key = new EntityKey(objectSet.EntityContainer.Name + "." + objectSet.Name, entityKeyValues);
            return dbContext.ObjectContext.TryGetObjectByKey(key, out value) && value != null;
        }
        public T GetFirst(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includes)
        {
            return _dbContext.Set<T>().IncludeMultiple(includes).First(match);
        }
        public object Create(Type target)
        {
            return _dbContext.Set(target).Create();
        }

        void IRepository<T>.AddEntity(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<T1>> WhereAsync<T1>(Expression<Func<T1, bool>> match, params Expression<Func<T1, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<T1> GetFirstAsync<T1>(Expression<Func<T1, bool>> match, params Expression<Func<T1, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> Paging<T1>(Expression<Func<T, bool>> match, Expression<Func<T, T1>> orderBy, int? startIndex, int? pageSize, params Expression<Func<T, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if(disposing && _dbContext != null)
            {
                _dbContext.Dispose();
                _dbContext = null;
            }
        }
    }
}
