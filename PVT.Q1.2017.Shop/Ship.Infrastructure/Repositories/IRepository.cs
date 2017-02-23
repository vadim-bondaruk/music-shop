namespace Shop.Infrastructure.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Shop.Infrastructure.Models;

    /// <summary>
    /// Main Repository 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ICollection<TEntity> GetAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        ICollection<TEntity> GetAll(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        void AddOrUpdate(TEntity model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        void Delete(TEntity model);
    }
}