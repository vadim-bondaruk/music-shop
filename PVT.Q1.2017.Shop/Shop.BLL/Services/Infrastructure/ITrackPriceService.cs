namespace Shop.BLL.Services.Infrastructure
{
    using Common.Models;
    using Shop.Infrastructure.Services;

    /// <summary>
    /// </summary>
    public interface ITrackPriceService : IService<TrackPrice>
    {
        /// <summary>
        /// Returns the track price in the specified <paramref name="currency"/> for the specified  <paramref name="priceLevel"/>.
        /// </summary>
        /// <param name="track">
        /// The track.
        /// </param>
        /// <param name="priceLevel">
        /// The price level.
        /// </param>
        /// <param name="currency">
        /// The currency.
        /// </param>
        /// <returns>
        /// The track price in the specified currency for the specified  <paramref name="priceLevel"/> or <b>null</b>.
        /// </returns>
        TrackPrice GeTrackPrice(Track track, PriceLevel priceLevel, Currency currency);

        /// <summary>
        /// Adds the <paramref name="track"/> price.
        /// </summary>
        /// <param name="track">
        /// The track.</param>
        /// <param name="price">
        /// The <paramref name="track"/> price.
        /// </param>
        /// <param name="currency">
        /// The currency.
        /// </param>
        /// <param name="priceLevel">
        /// The price level.
        /// </param>
        void AddTrackPrice(Track track, decimal price, Currency currency, PriceLevel priceLevel);

        /// <summary>
        /// Returns the track price with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The track price id.</param>
        /// <returns>
        /// The track price with the specified <paramref name="id"/> or <b>null</b> if track price doesn't exist.
        /// </returns>
        TrackPrice GetTrackPriceInfo(int id);
    }
}