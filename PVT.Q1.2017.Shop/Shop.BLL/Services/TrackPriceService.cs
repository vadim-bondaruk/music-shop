namespace Shop.BLL.Services
{
    using System.Linq;
    using Common.Models;
    using DAL.Infrastruture;
    using Infrastructure;

    /// <summary>
    /// The track price service.
    /// </summary>
    public class TrackPriceService : BaseService, ITrackPriceService
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackPriceService"/> class.
        /// </summary>
        /// <param name="factory">
        /// The repository factory.
        /// </param>
        public TrackPriceService(IRepositoryFactory factory) : base(factory)
        {
        }

        #endregion //Constructors

        #region ITrackPriceService Members

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
        public TrackPrice GeTrackPrice(Track track, PriceLevel priceLevel, Currency currency)
        {
            using (var repository = this.Factory.GetTrackPriceRepository())
            {
                return repository.GetAll(
                                         p => p.TrackId == track.Id &&
                                              p.PriceLevelId == priceLevel.Id &&
                                              p.CurrencyId == currency.Id,
                                         p => p.Track,
                                         p => p.Currency,
                                         p => p.PriceLevel).FirstOrDefault();
            }
        }

        /// <summary>
        /// Returns the track price with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The track price id.</param>
        /// <returns>
        /// The track price with the specified <paramref name="id"/> or <b>null</b> if track price doesn't exist.
        /// </returns>
        public TrackPrice GetTrackPriceInfo(int id)
        {
            using (var repository = this.Factory.GetTrackPriceRepository())
            {
                return repository.GetById(id, p => p.Track, p => p.Currency, p => p.PriceLevel);
            }
        }

        #endregion ITrackPriceService Members
    }
}