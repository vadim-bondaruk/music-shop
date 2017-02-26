namespace Shop.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using Common.Models;
    using Context;
    using Ship.Infrastructure.Repositories;
    
    /// <summary>
    /// 
    /// </summary>
    public class UserRepository : IRepository<User>
    {
        /// <summary>
        /// Contains list of users
        /// </summary>
        private UserContext _db;

        /// <summary>
        /// 
        /// </summary>
        public UserRepository()
        {
            // кандидат на использование DI?
            this._db = new UserContext();
        }

        /// <summary>
        /// Returns user by his id number
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public User GetById(int id)
        {
            return this._db.Users.Find(id);
        }

        /// <summary>
        /// Returns a list of all users
        /// </summary>
        /// <returns></returns>
        public ICollection<User> GetAll()
        {
            if (this._db != null)
            {
                return this._db.Users.ToList();
            }
            else
            {
                throw new Exception("problems in DBContext");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public ICollection<User> GetAll(Expression<Func<User, bool>> filter)
        {
            IQueryable<User> query = this._db.Set<User>();
            if (this._db != null && filter != null)
            {
                query = query.Where(filter);
            }
            else
            {
                throw new Exception("problems in DBContext");
            }

            return query.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void AddOrUpdate(User model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }

            if (this._db != null)
            {
                if (this._db.Users.Contains(model))
                {
                    this._db.Users.Attach(model);
                    this._db.Entry(model).State = EntityState.Modified;
                }
                else
                {
                    this._db.Users.Add(model);
                }
            }
            else
            {
                throw new Exception("problems in DBContext");
            }
        }

        /// <summary>
        /// Deletes user from DB
        /// </summary>
        /// <param name="id">id</param>
        public void Delete(int id)
        {
            User userToDelete = this._db.Users.Find(id);
            this.Delete(userToDelete);
        }

        /// <summary>
        /// Deletes user from DB
        /// </summary>
        /// <param name="model"></param>
        public void Delete(User model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }

            this._db.Users.Remove(model);
        }
    }
}
