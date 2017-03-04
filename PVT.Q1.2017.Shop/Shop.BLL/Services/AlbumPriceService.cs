namespace Shop.BLL.Services
{
    using System;
    using Common.Models;
    using DAL.Repositories.Infrastruture;
    using Infrastructure;
    using Shop.Infrastructure;
    using Shop.Infrastructure.Validators;

    /// <summary>
    /// The album price service.
    /// </summary>
    public class AlbumPriceService : Service<IAlbumPriceRepository, AlbumPrice>, IAlbumPriceService
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumPriceService"/> class.
        /// </summary>
        /// <param name="factory">
        /// The factory.
        /// </param>
        /// <param name="validator">
        /// The validator.
        /// </param>
        public AlbumPriceService(IFactory factory, IValidator<AlbumPrice> validator) : base(factory, validator)
        {
        }

        #endregion //Constructors

        #region IAlbumPriceService Members

        public AlbumPrice GeAlbumPrice(Album album, PriceLevel priceLevel, Currency currency)
        {
            throw new NotImplementedException();
        }

        public void AddAlbumPrice(Album album, decimal price, Currency currency, PriceLevel priceLevel)
        {
            throw new NotImplementedException();
        }

        public TrackPrice GetTrackPriceInfo(int id)
        {
            throw new NotImplementedException();
        }

        #endregion //IAlbumPriceService Members
    }
}