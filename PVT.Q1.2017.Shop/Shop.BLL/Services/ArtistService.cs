namespace Shop.BLL.Services
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;

    using Shop.BLL.Services.Infrastructure;
    using Shop.Common.Models;
    using Shop.Common.Models.ViewModels;
    using Shop.DAL.Infrastruture;

    /// <summary>
    ///     The Artist service.
    /// </summary>
    public class ArtistService : BaseService, IArtistService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ArtistService" /> class.
        /// </summary>
        /// <param name="factory">
        ///     The repository factory.
        /// </param>
        public ArtistService(IRepositoryFactory factory)
            : base(factory)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <returns>
        /// </returns>
        public ArtistDetailsViewModel GetArtistInfo(int id)
        {
            using (var repository = this.Factory.GetArtistRepository())
            {
                var artist = repository.GetById(id);
                if (artist == null)
                {
                    return null;
                }

                var viewModel = Mapper.Map<ArtistDetailsViewModel>(artist);
                return viewModel;
            }
        }

        /// <summary>
        ///     Returns all registered Artists.
        /// </summary>
        /// <returns>
        ///     All registered Artists.
        /// </returns>
        public ICollection<Artist> GetArtistsList()
        {
            using (var repository = this.Factory.GetArtistRepository())
            {
                return repository.GetAll();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="artistId">
        ///     The artist id.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public ICollection<Track> GetTracksList(int artistId)
        {
            throw new NotImplementedException();
        }
    }
}