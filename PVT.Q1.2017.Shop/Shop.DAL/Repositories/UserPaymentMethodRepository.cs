using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Shop.Common.Models;
using Shop.Infrastructure.Repositories;

namespace Shop.DAL.Repositories
{
    class UserPaymentMethodRepository : IRepository<UserPaymentMethod>
    {
        /// <summary>
        /// DataBase Context
        /// </summary>
        private readonly DbContext _dbContext;

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

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(UserPaymentMethod model)
        {
            throw new NotImplementedException();
        }
    }
}
