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
        IAlbumPriceRepository GetAlbumPriceRepository();

        /// <summary>
        /// Returns the album repository.
        /// </summary>
        /// <returns>
        /// The album repository.
        /// </returns>
        IAlbumRepository GetAlbumRepository();

        /// <summary>
        /// Returns the artist repository.
        /// </summary>
        /// <returns>
        /// The artist repository.
        /// </returns>
        IArtistRepository GetArtistRepository();

        /// <summary>
        /// Returns the currency repository.
        /// </summary>
        /// <returns>
        /// The currency repository.
        /// </returns>
        ICurrencyRepository GetCurrencyRepository();

        /// <summary>
        /// Returns the currency rate repository.
        /// </summary>
        /// <returns>
        /// The track repository.
        /// </returns>
        ICurrencyRateRepository GetCurrencyRateRepository();

        /// <summary>
        /// Returns the feedback repository.
        /// </summary>
        /// <returns>
        /// The feedback repository.
        /// </returns>
        IFeedbackRepository GetFeedbackRepository();

        /// <summary>
        /// Returns the genre repository.
        /// </summary>
        /// <returns>
        /// The track repository.
        /// </returns>
        IGenreRepository GetGenreRepository();

        /// <summary>
        /// Returns the price level repository.
        /// </summary>
        /// <returns>
        /// The price level repository.
        /// </returns>
        IPriceLevelRepository GetPriceLevelRepository();

        /// <summary>
        /// Returns the track price repository.
        /// </summary>
        /// <returns>
        /// The track price repository.
        /// </returns>
        ITrackPriceRepository GetTrackPriceRepository();

        /// <summary>
        /// Returns the track repository.
        /// </summary>
        /// <returns>
        /// The track repository.
        /// </returns>
        ITrackRepository GetTrackRepository();

        /// <summary>
        /// Returns the user repository.
        /// </summary>
        /// <returns>
        /// The user repository.
        /// </returns>
        IUserRepository GetUserRepository();

        /// <summary>
        /// Returns the vote repository.
        /// </summary>
        /// <returns>
        /// The track repository.
        /// </returns>
        IVoteRepository GetVoteRepository();

        /// <summary>
        /// Returns the album and track relation repository.
        /// </summary>
        /// <returns>
        /// The album and track relation repository.
        /// </returns>
        IAlbumTrackRelationRepository GetAlbumTrackRelationRepository();
    }
}