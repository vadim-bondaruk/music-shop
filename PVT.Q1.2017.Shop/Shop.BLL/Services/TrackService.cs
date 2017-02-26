namespace Shop.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using Common.Models;
    using Exceptions;
    using Helpers;
    using Infrastructure.Repositories;
    using Infrastructure.Validators;
    using Validators;

    /// <summary>
    /// The track service.
    /// </summary>
    public class TrackService : Service<Track>, ITrackService
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackService"/> class.
        /// </summary>
        /// <param name="repositoryFactory">
        /// The repository factory.
        /// </param>
        /// <param name="validator">
        /// The track validator.
        /// </param>
        public TrackService(IRepositoryFactory repositoryFactory, IValidator<Track> validator) : base(repositoryFactory, validator)
        {
        }

        #endregion //Constructors

        #region Public Methods

        /// <summary>
        /// Returns all tracks with the specified <paramref name="name"/>.
        /// </summary>
        /// <param name="name">
        /// The track name.
        /// </param>
        /// <returns>
        /// All tracks with the specified <paramref name="name"/>.
        /// </returns>
        public ICollection<Track> GetTracksByName(string name)
        {
            if (!TrackValidator.IsTrackNameValid(name))
            {
                return new List<Track>();
            }

            ICollection<Track> tracks;
            using (var repository = this.RepositoryFactory.CreateRepository<Track>())
            {
                tracks = repository.GetAll(t => t.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            }

            // fills information about artists
            TrackHelper.FillArtistInfo(this.RepositoryFactory, tracks);

            return tracks;
        }

        /// <summary>
        /// Returns all tracks with the specified <paramref name="genre"/>.
        /// </summary>
        /// <param name="genre">
        /// The track genre.
        /// </param>
        /// <returns>
        /// All tracks with the specified <paramref name="genre"/>.
        /// </returns>
        public ICollection<Track> GetTracksByGenre(Genre genre)
        {
            ICollection<Track> tracks;
            using (var repository = this.RepositoryFactory.CreateRepository<Track>())
            {
                tracks = repository.GetAll(t => t.Genre == genre);
            }

            // fills information about artists
            TrackHelper.FillArtistInfo(this.RepositoryFactory, tracks);

            return tracks;
        }

        /// <summary>
        /// Returns all registered tracks.
        /// </summary>
        /// <returns>
        /// All registered tracks.
        /// </returns>
        public ICollection<Track> GetAllRegisteredTracks()
        {
            ICollection<Track> tracks;
            using (var repository = this.RepositoryFactory.CreateRepository<Track>())
            {
                tracks = repository.GetAll();
            }

            // fills information about artists
            TrackHelper.FillArtistInfo(this.RepositoryFactory, tracks);

            return tracks;
        }

        #endregion //Public Methods

        #region Protected Methods

        /// <summary>
        /// Occurs when the service tries to update a non-existent track.
        /// </summary>
        /// <exception cref="TrackNotFoundException">
        /// When a track doesn't exist in the system.
        /// </exception>
        protected override void OnUpdateEntityNotFoundException()
        {
            throw new EntityNotFoundException("The model doesn't exist. Nothing to update.");
        }

        #endregion //Protected Methods
    }
}