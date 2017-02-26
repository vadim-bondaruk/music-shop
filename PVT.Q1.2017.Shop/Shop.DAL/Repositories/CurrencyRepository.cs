namespace Shop.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Shop.Infrastructure.Repositories;
    using Shop.Common.Models;
    using System.Linq.Expressions;
    using System.Data.Entity;

    /// <summary>
    /// Repository of Currency
    /// </summary>
    public class CurrencyRepository : IRepository<Currency>
    {
        /// <summary>
        /// DataBase context
        /// </summary>
        /// <param name="model"></param>
        private readonly DbContext _dbContext;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="dbContext"></param>
        public CurrencyRepository(DbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException();
            }

            this._dbContext = dbContext;
        }

        /// <summary>
        /// Adds or update Currency
        /// </summary>
        /// <param name="model"></param>
        public void AddOrUpdate(Currency model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }

            var result = this._dbContext.Set<Currency>();
            if (result.Find(model.Id) != null)
            {
                result.Remove(model);
            }

            result.Add(model);
        }

        /// <summary>
        /// Delete Currency
        /// </summary>
        /// <param name="model"></param>
        public void Delete(Currency model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }

            var result = this._dbContext.Set<Currency>();
            if (result.Find(model.Id) != null)
            {
                result.Remove(model);
            }
        }

        /// <summary>
        /// Delete Currency by Id
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var result = this._dbContext.Set<Currency>();
            var removeItem = result.Find(id);
            if (removeItem == null)
            {
                throw new ArgumentNullException();
            }
            result.Remove(removeItem);
        }

        /// <summary>
        /// Get all Currency
        /// </summary>
        /// <returns></returns>
        public ICollection<Currency> GetAll()
        {
            var result = this._dbContext.Set<Currency>();
            return result.ToList();
        }

        /// <summary>
        /// Get Currency with filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public ICollection<Currency> GetAll(Expression<Func<Currency, bool>> filter)
        {
            var result = this._dbContext.Set<Currency>();
            return result.Where(filter).ToList();
        }

        /// <summary>
        /// Get Currency by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Currency GetById(int id)
        {
            var result = this._dbContext.Set<Currency>();
            return result.Find(id);
        }
    }
}
