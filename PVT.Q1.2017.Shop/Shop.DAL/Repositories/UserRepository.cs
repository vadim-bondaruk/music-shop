namespace Shop.DAL.Repositories
{
    #region using

    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq.Expressions;

    using Shop.Common.Models;
    using Shop.DAL.Repositories.Infrastruture;

    #endregion

    /// <summary>
    /// The user repository (have to be extended)
    /// </summary>
    public class UserRepository : Repository<User>, IUserRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository" />
        /// class.
        /// </summary>
        /// <param name="dbContext">The db context.</param>
        public UserRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public new ICollection<User> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public new ICollection<User> GetAll(Expression<Func<User, bool>> filter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds the specified <paramref name="user" /> into Db.
        /// </summary>
        /// <param name="user">The user to add.</param>
        protected override void Add(User user)
        {
            EntityState priveLevelEntryState;
            EntityState currencyEntryState;

            // Detaching the navigation properties in case if they are attached to prevent unexpected behaviour of the DbContext.
            // The UserRepository should be SOLID, should only add information about user! Not about price level or user currency!
            this.DetachNavigationProperty(user.PriceLevel, out priveLevelEntryState);
            this.DetachNavigationProperty(user.UserCurrency, out currencyEntryState);

            user.PriceLevel = null;
            user.UserCurrency = null;

            // detach more navigation properties here...

            // adding the user into Db
            base.Add(user);
        }
    }
}