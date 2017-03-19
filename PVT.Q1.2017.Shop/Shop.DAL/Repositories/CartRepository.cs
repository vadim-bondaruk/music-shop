namespace Shop.DAL.Repositories
{
    using System;
    using System.Data.Entity;
    using Common.Models;
    using Infrastruture;
    using System.Collections.Generic;
    using Ninject;


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
        /// <param name="userId">User's ID</param> 
        /// <param name="trackId">Added Track ID</param> 
        public void AddTrack(int userId, int trackId)
        {
            var cart = GetById(userId);
            var kernel = new StandardKernel(new DefaultRepositoriesNinjectModule());
            var trackRepo = kernel.Get<ITrackRepository>();
            var track = trackRepo.GetById(trackId);
            if (track == null)
                throw new Exception("Incorrect Track Id");
            cart.Tracks.Add(track);
            AddOrUpdate(cart);
        }

        /// <summary> 
        /// Add track list to User's Cart 
        /// </summary> 
        /// <param name="userId">User's ID</param> 
        /// <param name="trackIds">Added Tracks IDs</param> 
        public void AddTrack(int userId, IEnumerable<int> trackIds)
        {
            foreach (var trackId in trackIds)
                AddTrack(userId, trackId);
        }

        /// <summary> 
        /// Remove track from User's Cart 
        /// </summary> 
        /// <param name="userId">User's ID</param> 
        /// <param name="trackId">Removed Track ID</param> 
        public void RemoveTrack(int userId, int trackId)
        {
            var cart = GetById(userId);
            var kernel = new StandardKernel(new DefaultRepositoriesNinjectModule());
            var trackRepo = kernel.Get<ITrackRepository>();
            var track = trackRepo.GetById(trackId);
            if (track == null)
                throw new Exception("Incorrect Track Id");
            cart.Tracks.Remove(track);
            AddOrUpdate(cart);
        }

        /// <summary> 
        /// Remove track list from User's Cart 
        /// </summary> 
        /// <param name="userId">User's ID</param> 
        /// <param name="trackIds">Removed Tracks IDs</param> 
        public void RemoveTrack(int userId, IEnumerable<int> trackIds)
        {
            foreach (var trackId in trackIds)
                RemoveTrack(userId, trackId);
        }
    }
}
