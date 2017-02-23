namespace Shop.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Ship.Infrastructure.Repositories;
    using Shop.DAL.Data;
    using Shop.DAL.Entities;

    /// <summary>
    /// 
    /// </summary>
    public class UserRepository : IRepository<User>
    {
        /// <summary>
        /// Contains list of users
        /// </summary>
        private ListOfUsers _db;

        /// <summary>
        /// 
        /// </summary>
        public UserRepository()
        {
            // кандидат на использование DI?
            this._db = new ListOfUsers();
        }

        /// <summary>
        /// Returns user by his id number
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public User GetById(int id)
        {
            return this._db.Users.Find(p => p.Id == id);
        }

        /// <summary>
        /// Returns a list of all users
        /// </summary>
        /// <returns></returns>
        public ICollection<User> GetAll()
        {
            if (this._db != null)
            {
                return this._db.Users;
            }
            else
            {
                throw new Exception("problems in repository");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public ICollection<User> GetAll(Expression<Func<User, bool>> filter)
        {
            // временно не работает как надо
            
            if (this._db != null)
            {
                return this._db.Users;
            }
            else
            {
                throw new Exception("problems in repository");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void AddOrUpdate(User model)
        {
            int countOfUsers = this._db.Users.Count;
            if (model.Id > countOfUsers)
            {
                this._db.Users.Add(model);
            }
            else
            {
                this._db.Users.Insert(model.Id, model);
            }
        }

        /// <summary>
        /// Deletes user from DB
        /// </summary>
        /// <param name="id">id</param>
        public void Delete(int id)
        {
            User user = this._db.Users.Find(p => p.Id == id);
            if (user != null)
            {
                this._db.Users.Remove(user);
            }
        }

        /// <summary>
        /// Deletes user from DB
        /// </summary>
        /// <param name="model"></param>
        public void Delete(User model)
        {
            this._db.Users.Remove(model);
        }
    }
}
