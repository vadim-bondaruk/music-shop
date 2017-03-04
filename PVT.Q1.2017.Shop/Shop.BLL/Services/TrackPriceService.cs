namespace Shop.BLL.Services
{
    using System.Linq;
    using Common.Models;
    using DAL.Repositories.Infrastruture;
    using Helpers;
    using Infrastructure;
    using Shop.Infrastructure;
    using Shop.Infrastructure.Validators;

    /// <summary>
    /// The track price service.
    /// </summary>
    public class TrackPriceService : Service<ITrackPriceRepository, TrackPrice>, ITrackPriceService
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackPriceService"/> class.
        /// </summary>
        /// <param name="factory">
        /// The repository factory.
        /// </param>
        /// <param name="validator">
        /// The validator.
        /// </param>
        public TrackPriceService(IFactory factory, IValidator<TrackPrice> validator) : base(factory, validator)
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
            ValidatorHelper.CheckTrackForNull(track);
            ValidatorHelper.CheckPriceLevelForNull(priceLevel);
            ValidatorHelper.CheckCurrencyForNull(currency);

            using (var repository = this.Factory.Create<ITrackPriceRepository>())
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

            this.Validator.Validate(trackPrice);

            using (var repository = this.Factory.Create<ITrackPriceRepository>())
            {
                repository.AddOrUpdate(trackPrice);
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
            using (var repository = this.Factory.Create<ITrackPriceRepository>())
            {
                return repository.GetById(id, p => p.Track, p => p.Currency, p => p.PriceLevel);
            }
        }

        #endregion ITrackPriceService Members
    }
}