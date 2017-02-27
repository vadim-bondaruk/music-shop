namespace Shop.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    using Shop.Common.Models;
    using Shop.Infrastructure.Repositories;

    /// <summary>
    /// Repository for UserPaymentMethod 
    /// </summary>
    public class UserPaymentMethodRepository : IRepository<UserPaymentMethod>
    {
        /// <summary>
        /// DataBase Context
        /// </summary>
        private readonly DbContext _dbContext;

        /// <summary>
        /// Default ctor on dbContext
        /// </summary>
        /// <param name="dbContext">Database context</param>
        public UserPaymentMethodRepository(DbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException();
            }

            this._dbContext = dbContext;
        }

        /// <summary>
        /// Tries to find an entity by the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The key.</param>
        /// <returns>
        /// An entity with the specified <paramref name="id"/> or null in case if there are now enity with such <paramref name="id"/>
        /// </returns>
        public UserPaymentMethod GetById(int id)
        {
            var result = this._dbContext.Set<UserPaymentMethod>();
            return result.Find(id);
        }

        /// <summary>
        /// Returns all entities from the repository.
        /// </summary>
        /// <returns>
        /// All entities from the repository.
        /// </returns>
        public ICollection<UserPaymentMethod> GetAll()
        {
            var result = this._dbContext.Set<UserPaymentMethod>();
            return result.ToList();
        }

        /// <summary>
        /// Gets all entity by some filter
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <returns></returns>
        public ICollection<UserPaymentMethod> GetAll(Expression<Func<UserPaymentMethod, bool>> filter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds or updates the specified <paramref name="model"/>.
        /// </summary>
        /// <param name="model">A model to add or update.</param>
        public void AddOrUpdate(UserPaymentMethod model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }

            var result = this._dbContext.Set<UserPaymentMethod>();
            if (result.Find(model.Id) != null)
            {
                result.Remove(model);
            }

            result.Add(model);
        }

        /// <summary>
        /// Deletes one model from DB by id
        /// </summary>
        /// <param name="id">target model id</param>
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes one model from DB
        /// </summary>
        /// <param name="model">target model</param>
        public void Delete(UserPaymentMethod model)
        {
            throw new NotImplementedException();
        }
    }
}
