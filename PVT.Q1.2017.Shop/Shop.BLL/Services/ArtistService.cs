namespace Shop.BLL.Services
{
    using Shop.BLL.Helpers;
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
        /// </summary>
        private readonly IRepositoryFactory repositoryFactory;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ArtistService" /> class.
        /// </summary>
        /// <param name="factory">
        ///     The factory.
        /// </param>
        public ArtistService(IRepositoryFactory factory)
            : base(factory)
        {
            this.repositoryFactory = factory;
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// </returns>
        public Artist GetArtist(int id)
        {
            using (var repository = this.Factory.GetArtistRepository())
            {
                var artist = repository.GetById(id);
                return artist;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <returns>
        /// </returns>
        public ArtistDetailsViewModel GetArtistDetailsViewModel(int id)
        {
            using (var repository = this.Factory.GetArtistRepository())
            {
                var artist = repository.GetById(id);
                return ModelsMapper.GetArtistDetailsViewModel(artist);
            }
        }
    }
}