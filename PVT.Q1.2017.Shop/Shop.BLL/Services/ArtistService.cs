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
    /// The artist service
    /// </summary>
    public class ArtistService : Service<IArtistRepository, Artist>, IArtistService
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistService"/> class.
        /// </summary>
        /// <param name="factory">
        /// The factory.
        /// </param>
        /// <param name="validator">
        /// The validator.
        /// </param>
        public ArtistService(IFactory factory, IValidator<Artist> validator) : base(factory, validator)
        {
        }

        #endregion //Constructors

        #region IArtistService Members

        public Artist GetArtistInfo(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Album> GetAlbumsList(Artist artist)
        {
            throw new NotImplementedException();
        }

        public ICollection<Track> GetTracksList(Artist artist)
        {
            throw new NotImplementedException();
        }

        public ICollection<Track> GetTracksWithoutPriceConfigured(Artist artist)
        {
            throw new NotImplementedException();
        }

        public ICollection<Track> GetTracksWithPriceConfigured(Artist artist)
        {
            throw new NotImplementedException();
        }

        public ICollection<Album> GetAlbumsWithoutPriceConfigured(Artist artist)
        {
            throw new NotImplementedException();
        }

        public ICollection<Album> GetAlbumsWithPriceConfigured(Artist artist)
        {
            throw new NotImplementedException();
        }

        #endregion //IArtistService Members
    }
}