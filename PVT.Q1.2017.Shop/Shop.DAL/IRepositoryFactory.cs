using Shop.Infrastructure.Models;

namespace Shop.DAL
{
    using Common.Models;
    using Infrastructure.Repositories;

    /// <summary>
    /// The repository factory
    /// </summary>
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Returns the repository for the specified model.
        /// </summary>
        /// <typeparam name="TEntity">
        /// A model.
        /// </typeparam>
        /// <returns>
        /// Returns the repository for the specified model.
        /// </returns>
        IDisposableRepository<TEntity> CreateRepository<TEntity>() where TEntity : BaseEntity, new();

        /// <summary>
        /// Returns the tracks repository.
        /// </summary>
        /// <returns>
        /// The tracks repository.
        /// </returns>
        IDisposableRepository<Track> CreateTrackRepository();

        /// <summary>
        /// Returns the artists repository.
        /// </summary>
        /// <returns>
        /// The artists repository.
        /// </returns>
        IDisposableRepository<Artist> CreateArtistRepository();
        
        /// <summary>
        /// Returns the album repository.
        /// </summary>
        /// <returns>
        /// The album repository.
        /// </returns>
        IDisposableRepository<Album> CreateAlbumRepository();

        /// <summary>
        /// Returns the feedback repository.
        /// </summary>
        /// <returns>
        /// The feedback repository.
        /// </returns>
        IDisposableRepository<Feedback> CreateFeedbackRepository();

        /// <summary>
        /// Returns the vote repository.
        /// </summary>
        /// <returns>
        /// The vote repository.
        /// </returns>
        IDisposableRepository<Vote> CreateVoteRepository();

        /// <summary>
        /// Returns the genre repository.
        /// </summary>
        /// <returns>
        /// The genre repository.
        /// </returns>
        IDisposableRepository<Genre> CreateGenreRepository();
    }
}