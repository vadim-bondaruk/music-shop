﻿namespace Shop.BLL.Services
{
    using Common.Models;
    using Infrastructure;

    /// <summary>
    /// The track price service.
    /// </summary>
    public class TrackPriceService : BaseService, ITrackPriceService
    {
        #region Constructors

        public TrackPriceService(IRepositoryFactory factory) : base(factory)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackPriceService"/> class.
        /// </summary>
        /// <param name="factory">
        /// The repository factory.
        /// </param>
        /// <param name="validator">
        /// The validator.
        /// </param>
        

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
            var _trackRepo = _factory.GetAlbumPriceRepository();

            using (var repository = this.CreateRepository())
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
        /// Sets the <paramref name="track"/> price.
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
        public void AddTrackPrice(Track track, decimal price, Currency currency, PriceLevel priceLevel)
        {
            ValidatorHelper.CheckTrackForNull(track);
            ValidatorHelper.CheckCurrencyForNull(currency);
            ValidatorHelper.CheckPriceLevelForNull(priceLevel);

            var trackPrice = new TrackPrice
            {
                Price = price,
                PriceLevelId = priceLevel.Id,
                CurrencyId = currency.Id,
                TrackId = track.Id
            };

            this.Register(trackPrice);
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
            using (var repository = this.CreateRepository())
            {
                return repository.GetById(id, p => p.Track, p => p.Currency, p => p.PriceLevel);
            }
        }

        #endregion ITrackPriceService Members
    }
}