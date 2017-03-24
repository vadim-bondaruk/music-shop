namespace Shop.BLL.Services
{
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
        /// <param name="artist">
        /// The artist.
        /// </param>
        public void Delete(Artist artist)
        {
            using (var artistRepo = this.repositoryFactory.GetArtistRepository())
            {
                artistRepo.Delete(artist);
                artistRepo.SaveChanges();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <returns>
        /// </returns>
        public ArtistManagementViewModel GetById(int id)
        {
            using (var repository = this.Factory.GetArtistRepository())
            {
                var artist = repository.GetById(id);
                return artist == null ? null : Mapper.Map<ArtistManagementViewModel>(artist);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="viewModel">
        ///     The view model.
        /// </param>
        public int Save(ArtistManagementViewModel viewModel)
        {
            using (var artistRepo = this.repositoryFactory.GetArtistRepository())
            {
                if (viewModel.UploadedImage != null)
                {
                    var bs = new byte[viewModel.UploadedImage.ContentLength];
                    using (var fs = viewModel.UploadedImage.InputStream)
                    {
                        var offset = 0;
                        do
                        {
                            offset += fs.Read(bs, offset, bs.Length - offset);
                        }
                        while (offset < bs.Length);
                    }

                    viewModel.Photo = bs;
                }

                var artist = Mapper.Map<Artist>(viewModel);
                artistRepo.AddOrUpdate(artist);
                artistRepo.SaveChanges();
                return artist.Id;
            }
        }
    }
}