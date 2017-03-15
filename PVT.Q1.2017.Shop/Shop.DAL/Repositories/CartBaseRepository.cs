namespace Shop.DAL.Repositories
{
    using System;
    using System.Data.Entity;
    using Common.Models;
    using Infrastruture;
    using System.Collections.Generic;


    /// <summary>
    /// Repository of User's Shop Cart
    /// </summary>
    public class CartBaseRepository : BaseRepository<Cart>, ICartRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyRateRepository"/> class.
        /// </summary>
        /// <param name="dbContext">
        /// The db context.
        /// </param>
        public CartBaseRepository(DbContext dbContext) : base(dbContext)
        {
        }

        #endregion //Constructors

        #region Protected Methods

        /// <summary> 
        /// Add track to User's Cart 
        /// </summary> 
        /// <param name="cartId">User's Cart ID</param> 
        /// <param name="trackId">Added Track ID</param> 
        protected void AddTrack(int cartId, int trackId)
        {
            var cart = GetById(cartId);
            var track = new TrackBaseRepository(DbContext).GetById(trackId);
            if (track == null)
                throw new Exception("Incorrect Track Id");
            cart.Tracks.Add(track);
            AddOrUpdate(cart);
        }

        /// <summary> 
        /// Add track list to User's Cart 
        /// </summary> 
        /// <param name="cartId">User's Cart ID</param> 
        /// <param name="trackIds">Added Tracks IDs</param> 
        protected void AddTrack(int cartId, IEnumerable<int> trackIds)
        {
            foreach (var trackId in trackIds)
                AddTrack(cartId, trackId);
        }

        /// <summary> 
        /// Remove track from User's Cart 
        /// </summary> 
        /// <param name="cartId">User's Cart ID</param> 
        /// <param name="trackId">Removed Track ID</param> 
        protected void RemoveTrack(int cartId, int trackId)
        {
            var cart = GetById(cartId);
            var track = new TrackBaseRepository(DbContext).GetById(trackId);
            if (track == null)
                throw new Exception("Incorrect Track Id");
            cart.Tracks.Remove(track);
            AddOrUpdate(cart);
        }

        /// <summary> 
        /// Remove track list from User's Cart 
        /// </summary> 
        /// <param name="cartId">User's Cart ID</param> 
        /// <param name="trackId">Removed Tracks IDs</param> 
        protected void RemoveTrack(int cartId, IEnumerable<int> trackIds)
        {
            foreach (var trackId in trackIds)
                RemoveTrack(cartId, trackId);
        }

        #endregion //Protected Methods
    }
}
