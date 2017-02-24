namespace Shop.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using Common.Models;
    using Infrastructure.Repositories;


    /// <summary>
    /// Repository of User's Shop Cart
    /// </summary>
    public class CartRepository : IRepository<Cart>
    {
        /// <summary>
        /// DataBase Context
        /// </summary>
        private readonly DbContext _dbContext;


        public CartRepository(DbContext dbContext)
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
        public Cart GetById(int id)
        {
            var result = this._dbContext.Set<Cart>();
            return result.Find(id);
        }

        /// <summary>
        /// Returns all entities from the repository.
        /// </summary>
        /// <returns>
        /// All entities from the repository.
        /// </returns>
        public ICollection<Cart> GetAll()
        {
            var result = this._dbContext.Set<Cart>();
            return result.ToList();
        }

        /// <summary>
        /// Tries to find entities from the repository using the specified <paramref name="filter"/>.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>Entities which corespond to </returns>
        public ICollection<Cart> GetAll(Expression<Func<Cart, bool>> filter)
        {
            var result = this._dbContext.Set<Cart>();
            return result.Where(filter).ToList();
        }

        /// <summary>
        /// Adds or updates the specified <paramref name="model"/>.
        /// </summary>
        /// <param name="model">A model to add or update.</param>
        public void AddOrUpdate(Cart model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }

            var result = this._dbContext.Set<Cart>();
            if (result.Find(model.Id) != null)
            {
                result.Remove(model);
            }

            result.Add(model);
        }

        /// <summary>
        /// Deletes a model with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The model key.</param>
        public void Delete(int id)
        {
            var result = this._dbContext.Set<Cart>();
            var removeItem = result.Find(id);
            if (removeItem == null)
            {
                throw new ArgumentNullException();
            }

            result.Remove(removeItem);
        }

        /// <summary>
        /// Deletes the <paramref name="model"/> from the repository.
        /// </summary>
        /// <param name="model">The model to delete.</param>
        public void Delete(Cart model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }

            var result = this._dbContext.Set<Cart>();
            if (result.Find(model.Id) != null)
            {
                result.Remove(model);
            }
        }
    }
}
