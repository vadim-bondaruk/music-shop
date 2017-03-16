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
        /// <param name="userId">User's ID</param> 
        /// <param name="trackId">Added Track ID</param> 
        void AddTrack(int userId, int trackId);

        /// <summary> 
        /// Add track list to User's Cart 
        /// </summary> 
        /// <param name="userId">User's ID</param> 
        /// <param name="trackIds">Added Tracks IDs</param>
        void AddTrack(int userId, IEnumerable<int> trackIds);

        /// <summary> 
        /// Remove track from User's Cart 
        /// </summary> 
        /// <param name="userId">User's ID</param> 
        /// <param name="trackId">Removed Track ID</param>
        void RemoveTrack(int userId, int trackId);

        /// <summary> 
        /// Remove track list from User's Cart 
        /// </summary> 
        /// <param name="userId">User's ID</param> 
        /// <param name="trackIds">Removed Tracks IDs</param> 
        void RemoveTrack(int userId, IEnumerable<int> trackIds);
    }
}