namespace Shop.DAL.Context
{
    using System;
    using System.Linq;
    using System.Data.Entity.ModelConfiguration;
    using System.Data.Entity;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Data.Entity.Migrations;
    using System.Reflection;
    using Migrations;
    using Infrastructure.Repositories;
    using Infrastructure.Core;
    using Infrastructure.Models;
    using System.Linq.Expressions;
    using System.Data.Entity.Core;
    using System.Data.Entity.Infrastructure;

    /// <summary>
    /// Music shop Db
    /// </summary>
    public class ShopContext : DbContext, IRepository 
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ShopContext"/> class.
        /// </summary>
        public ShopContext() : this("ShopConnection")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShopContext"/> class.
        /// </summary>
        /// <param name="connectionStringOrName">
        /// The connection string or Db name.
        /// </param>
        public ShopContext(string connectionStringOrName) : base(connectionStringOrName)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ShopContext, Configuration>());

            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
        }

        #endregion //Constructors

        #region Protected Methods

        /// <summary>
        /// The Db configuration.
        /// </summary>
        /// <param name="modelBuilder">
        /// The model builder.
        /// </param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
               .Where(type => !string.IsNullOrEmpty(type.Namespace))
               .Where(type => type.BaseType != null && type.BaseType.IsGenericType
                   && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var configurationInstance in typesToRegister.Select(Activator.CreateInstance))
            {
                modelBuilder.Configurations.Add((dynamic)configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }

        #endregion //Protected Methods

        #region IRepository implementation

        T IRepository.GetFirstOrNew<T>(out bool isNew)
        {
            var first = Set<T>().FirstOrDefault();
            if (first != null)
            {
                isNew = false;
                return first;
            }
            isNew = true;
            return base.Set<T>().Create();
        }
        T IRepository.GetFirstOrDefault<T>(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includes)
        {
            return Set<T>().IncludeMultiple(includes).FirstOrDefault(match);
        }
        T IRepository.GetFirst<T>(params Expression<Func<T, object>>[] includes)
        {
            return Set<T>().IncludeMultiple(includes).First();
        }
        public void AddOrUpdate<T>(IEnumerable<T> items) where T : class, new()
        {
            var array = items as T[] ?? items.ToArray();
            Set<T>().AddOrUpdate(array);
        }
        void IRepository.AddEntity<T>(T entity)
        {
            Set<T>().Add(entity);
        }
        IEnumerable<T> IRepository.TakeAll<T>(params Expression<Func<T, object>>[] includes)
        {
            return Set<T>().IncludeMultiple(includes);
        }
        IEnumerable<T> IRepository.Where<T>(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includes)
        {
            return Set<T>().Where(match).IncludeMultiple(includes);
        }
        T IRepository.Find<T>(object[] parameters)
        {
            return Set<T>().Find(parameters);
        }
        bool IUnitOfWork.SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return SaveChanges() > 0;
        }
        T IRepository.Create<T>()
        {
            return Set<T>().Create();
        }
        public async Task<ICollection<T>> WhereAsync<T>(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includes) where T : class, new()
        {
            return await Set<T>().IncludeMultiple(includes).Where(match).ToListAsync();
        }
        public async new Task<bool> SaveChangesAsync()
        {
            ChangeTracker.DetectChanges();
            return await base.SaveChangesAsync() > 0;
        }
        public async Task<T> GetFirstOrDefaultAsync<T>(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includes) where T : class, new()
        {
            return await Set<T>().IncludeMultiple(includes).FirstOrDefaultAsync(match);
        }
        public async Task<T> GetFirstAsync<T>(params Expression<Func<T, object>>[] includes) where T : class, new()
        {
            return await Set<T>().IncludeMultiple(includes).FirstAsync();
        }
        public async Task<T> GetFirstAsync<T>(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includes) where T : class, new()
        {
            return await Set<T>().IncludeMultiple(includes).FirstAsync(match);
        }
        public async Task<IEnumerable<T>> TakeAllAsync<T>(params Expression<Func<T, object>>[] includes) where T : class, new()
        {
            return await Set<T>().IncludeMultiple(includes).ToListAsync();
        }
        public async Task<IEnumerable<T>> Paging<T, T1>(Expression<Func<T, bool>> match, Expression<Func<T, T1>> orderBy, int? startIndex, int? pageSize, params Expression<Func<T, object>>[] includes) where T : class, new()
        {
            if (startIndex.HasValue && pageSize.HasValue)
            {
                return await Set<T>().Where(match).OrderBy(orderBy).Skip(startIndex.Value).Take(pageSize.Value).IncludeMultiple(includes).ToListAsync();
            }
            return await Set<T>().Where(match).OrderBy(orderBy).ToListAsync();
        }
        public void SetModified<T>(T itemForUpdate) where T : class, new()
        {
            if (Entry(itemForUpdate).State == EntityState.Detached)
            {
                Set<T>().Attach(itemForUpdate);
            }

            Entry(itemForUpdate).State = EntityState.Modified;
        }
        public void SetDeleted<T>(T itemForUpdate) where T : class, new()
        {
            Entry(itemForUpdate).State = EntityState.Deleted;
        }
        public void SetAdded<T>(T itemForUpdate) where T : class, new()
        {
            Entry(itemForUpdate).State = EntityState.Added;
        }
        public T GetFirstOrNew<T>(Expression<Func<T, bool>> match, out bool isNew, params Expression<Func<T, object>>[] includes) where T : class, new()
        {
            var result = Set<T>().IncludeMultiple(includes).FirstOrDefault(match);
            isNew = result == null;
            return result ?? new T();
        }
        public bool IsEntityExist<T>(T entity) where T : class, new()
        {
            object value;
            var dbContext = this as IObjectContextAdapter;
            var objectSet = dbContext.ObjectContext.CreateObjectSet<T>().EntitySet;
            var entityKeyValues = (from member in objectSet.ElementType.KeyMembers
                                   let info = entity.GetType().GetProperty(member.Name)
                                   let tempValue = info.GetValue(entity, null)
                                   select new KeyValuePair<string, object>(member.Name, tempValue)).ToList();

            var key = new EntityKey(objectSet.EntityContainer.Name + "." + objectSet.Name, entityKeyValues);
            return dbContext.ObjectContext.TryGetObjectByKey(key, out value) && value != null;
        }
        public T GetFirst<T>(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includes) where T : class, new()
        {
            return Set<T>().IncludeMultiple(includes).First(match);
        }
        public object Create(Type target)
        {
            return Set(target).Create();
        }

        #endregion
    }
}