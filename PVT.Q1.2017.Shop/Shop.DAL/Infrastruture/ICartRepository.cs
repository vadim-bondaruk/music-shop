namespace Shop.DAL.Infrastruture
{
    using Common.Models;
    using Infrastructure.Repositories;
    using System.Collections.Generic;

    /// <summary>
    /// The album price repository.
    /// </summary>
    public interface ICartRepository : IRepository<Cart>
    {
        /// <summary> 
        /// Add track to User's Cart 
        /// </summary> 
        /// <param name="cartId">User's Cart ID</param> 
        /// <param name="trackId">Added Track ID</param> 
        void AddTrack(int cartId, int trackId);

        /// <summary> 
        /// Add track list to User's Cart 
        /// </summary> 
        /// <param name="cartId">User's Cart ID</param> 
        /// <param name="trackIds">Added Tracks IDs</param>
        void AddTrack(int cartId, IEnumerable<int> trackIds);

        /// <summary> 
        /// Remove track from User's Cart 
        /// </summary> 
        /// <param name="cartId">User's Cart ID</param> 
        /// <param name="trackId">Removed Track ID</param>
        void RemoveTrack(int cartId, int trackId);

        /// <summary> 
        /// Remove track list from User's Cart 
        /// </summary> 
        /// <param name="cartId">User's Cart ID</param> 
        /// <param name="trackId">Removed Tracks IDs</param> 
        void RemoveTrack(int cartId, IEnumerable<int> trackIds);
    }
}