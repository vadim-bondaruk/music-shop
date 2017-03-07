namespace Shop.DAL.Infrastruture
{
    /// <summary>
    /// The common repository factory.
    /// </summary>
    /// <remarks>
    /// //// read!!! https://github.com/ninject/Ninject.Extensions.Factory/wiki/Factory-interface
    /// </remarks>
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Returns the album price repository.
        /// </summary>
        /// <returns>
        /// The album price repository.
        /// </returns>
        IAlbumPriceRepository CreateAlbumPriceRepository();

        /// <summary>
        /// Returns the album repository.
        /// </summary>
        /// <returns>
        /// The album repository.
        /// </returns>
        IAlbumRepository CreateAlbumRepository();

        /// <summary>
        /// Returns the artist repository.
        /// </summary>
        /// <returns>
        /// The artist repository.
        /// </returns>
        IArtistRepository CreatetArtistRepository();

        /// <summary>
        /// Returns the currency repository.
        /// </summary>
        /// <returns>
        /// The currency repository.
        /// </returns>
        ICurrencyRepository CreateCurrencyRepository();

        /// <summary>
        /// Returns the currency rate repository.
        /// </summary>
        /// <returns>
        /// The track repository.
        /// </returns>
        ICurrencyRateRepository CreateCurrencyRateRepository();

        /// <summary>
        /// Returns the feedback repository.
        /// </summary>
        /// <returns>
        /// The feedback repository.
        /// </returns>
        IFeedbackRepository CreateFeedbackRepository();

        /// <summary>
        /// Returns the genre repository.
        /// </summary>
        /// <returns>
        /// The track repository.
        /// </returns>
        IGenreRepository CreateGenreRepository();

        /// <summary>
        /// Returns the price level repository.
        /// </summary>
        /// <returns>
        /// The price level repository.
        /// </returns>
        IPriceLevelRepository CreatePriceLevelRepository();

        /// <summary>
        /// Returns the track price repository.
        /// </summary>
        /// <returns>
        /// The track price repository.
        /// </returns>
        ITrackPriceRepository CreateTrackPriceRepository();

        /// <summary>
        /// Returns the track repository.
        /// </summary>
        /// <returns>
        /// The track repository.
        /// </returns>
        ITrackRepository CreateTrackRepository();

        /// <summary>
        /// Returns the user repository.
        /// </summary>
        /// <returns>
        /// The user repository.
        /// </returns>
        IUserRepository CreateUserRepository();

        /// <summary>
        /// Returns the vote repository.
        /// </summary>
        /// <returns>
        /// The track repository.
        /// </returns>
        IVoteRepository CreateVoteRepository();
    }
}