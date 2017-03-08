//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Track.cs" company="PVT.Q1.2017">
//    PVT.Q1.2017
//  </copyright>
//  <summary>
//    The track.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------

namespace Shop.DAL.Repositories
{
    using System.Data.Entity;

    using Shop.Common.Models;
    using Shop.DAL.Infrastruture;

    /// <summary>
    ///     The user repository (have to be extended)
    /// </summary>
    public class UserBaseRepository : BaseRepository<User>, IUserRepository
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserBaseRepository" /> class.
        /// </summary>
        /// <param name="dbContext">
        ///     The db context.
        /// </param>
        public UserBaseRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        #endregion //Constructors

        #region Protected Methods

        /// <summary>
        ///     Adds the specified <paramref name="user" /> into Db.
        /// </summary>
        /// <param name="user">
        ///     The user to add.
        /// </param>
        protected override void Add(User user)
        {
            EntityState priveLevelEntryState;
            EntityState currencyEntryState;

            // Detaching the navigation properties in case if they are attached to prevent unexpected behaviour of the DbContext.
            // The UserBaseRepository should be SOLID, should only add information about user! Not about price level or user currency!
            this.DetachNavigationProperty(user.PriceLevel, out priveLevelEntryState);
            this.DetachNavigationProperty(user.UserCurrency, out currencyEntryState);

            user.PriceLevel = null;
            user.UserCurrency = null;

            // detach more navigation properties here...

            // adding the user into Db
            base.Add(user);
        }

        #endregion //Protected Methods
    }
}