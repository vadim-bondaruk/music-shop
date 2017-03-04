namespace Shop.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using Common.Models;
    using DAL.Repositories.Infrastruture;
    using Infrastructure;
    using Shop.Infrastructure;
    using Shop.Infrastructure.Validators;

    /// <summary>
    /// The album service.
    /// </summary>
    public class AlbumService : Service<IAlbumRepository, Album>, IAlbumService
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumService"/> class.
        /// </summary>
        /// <param name="factory">
        /// The factory.
        /// </param>
        /// <param name="validator">
        /// The validator.
        /// </param>
        public AlbumService(IFactory factory, IValidator<Album> validator) : base(factory, validator)
        {
        }

        #endregion //Constructors

        #region IAlbumService Members

        public void AddAlbum(Artist artist, string name)
        {
            throw new NotImplementedException();
        }

        public Album GetAlbumInfo(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Track> GetTracksList(Album album)
        {
            throw new NotImplementedException();
        }

        public ICollection<Track> GetTracksWithoutPriceConfigured(Album album)
        {
            throw new NotImplementedException();
        }

        public ICollection<Track> GetTracksWithPriceConfigured(Album album)
        {
            throw new NotImplementedException();
        }

        public ICollection<Album> GetAlbumsList()
        {
            throw new NotImplementedException();
        }

        public ICollection<Album> GetAlbumsWithoutPriceConfigured()
        {
            throw new NotImplementedException();
        }

        public ICollection<Album> GetAlbumsWithPriceConfigured()
        {
            throw new NotImplementedException();
        }

        public ICollection<AlbumPrice> GetAlbumPrices(Album album, PriceLevel priceLevel)
        {
            throw new NotImplementedException();
        }

        public ICollection<AlbumPrice> GetAlbumPrices(Album album)
        {
            throw new NotImplementedException();
        }

        #endregion //IAlbumService Members
    }
}