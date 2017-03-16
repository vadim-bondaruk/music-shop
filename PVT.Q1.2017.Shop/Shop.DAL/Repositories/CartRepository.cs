namespace Shop.DAL.Repositories
{
    using System;
    using System.Data.Entity;
    using Common.Models;
    using Infrastruture;


    /// <summary>
    /// Repository of User's Shop Cart
    /// </summary>
    public class CartRepository : BaseRepository<Cart>, ICartRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CartRepository"/> class.
        /// </summary>
        /// <param name="dbContext">
        /// The db context.
        /// </param>
        public CartRepository(DbContext dbContext) : base(dbContext)
        {
        }

        /// <summary> 
        /// Add track to User's Cart 
        /// </summary> 
        /// <param name="cartId">User's Cart ID</param> 
        /// <param name="trackId">Added Track ID</param> 
        protected void AddTrack(int cartId, int trackId)
        {
            var cart = GetById(cartId);
            var track = new TrackRepository(DbContext).GetById(trackId);
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
        protected void AddTrack(int cartId, int[] trackIds)
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
            var track = new TrackRepository(DbContext).GetById(trackId);
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
        protected void RemoveTrack(int cartId, int[] trackIds)
        {
            foreach (var trackId in trackIds)
                RemoveTrack(cartId, trackId);
        }
    }
}
