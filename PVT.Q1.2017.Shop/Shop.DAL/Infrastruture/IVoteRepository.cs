namespace Shop.DAL.Infrastruture
{
    using Common.Models;
    using Infrastructure.Repositories;

    /// <summary>
    /// The vote repository.
    /// </summary>
    public interface IVoteRepository : IRepository<Vote>
    {
        /// <summary>
        /// Returns average track rating for the specified <paramref name="track"/>.
        /// </summary>
        /// <param name="track">
        /// The track.
        /// </param>
        /// <returns>
        /// The average track rating for the specified <paramref name="track"/>.
        /// </returns>
        double GetAverageTrackRating(Track track);
    }
}