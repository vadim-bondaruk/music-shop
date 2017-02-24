namespace Shop.BLL.Services
{
    using Common.Models;
    using Exceptions;
    using Infrastructure.Repositories;
    using Infrastructure.Validators;

    /// <summary>
    /// The track service.
    /// </summary>
    public class TrackService
    {
        #region Fields

        /// <summary>
        /// The tracks repository.
        /// </summary>
        private readonly IRepository<Track> _repository;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackService"/> class.
        /// </summary>
        /// <param name="repository">
        /// The repository with tracks.
        /// </param>
        public TrackService(IRepository<Track> repository)
        {
            this._repository = repository;
        }

        #endregion //Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the track validator.
        /// </summary>
        public IValidator<Track> TrackValidator { get; set; }

        #endregion //Properties

        /// <summary>
        /// Registers new <paramref name="track"/>.
        /// </summary>
        /// <param name="track">
        /// The track to register.
        /// </param>
        public void RegisterNewTrack(Track track)
        {
            if (this.TrackValidator != null)
            {
                this.TrackValidator.Validate(track);
            }

            this._repository.AddOrUpdate(track);
        }

        /// <summary>
        /// Updates the specified <paramref name="track"/>.
        /// </summary>
        /// <param name="track">
        /// The track to update.
        /// </param>
        public void UpdateTrack(Track track)
        {
            if (this.TrackValidator != null)
            {
                this.TrackValidator.Validate(track);
            }

            if (track.Id < 0)
            {
                throw new TrackNotFoundException();
            }

            this._repository.AddOrUpdate(track);
        }
    }
}