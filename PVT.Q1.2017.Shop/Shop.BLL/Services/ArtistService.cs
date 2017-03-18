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
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <returns>
        /// </returns>
        public ArtistDetailsViewModel GetArtistViewModel(int id)
        {
            using (var repository = this.Factory.GetArtistRepository())
            {
                var artist = repository.GetById(id);
                if (artist == null)
                {
                    return null;
                }

                Mapper.Initialize(
                    cfg =>
                        cfg.CreateMap<Artist, ArtistDetailsViewModel>()
                            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday))
                            .ForMember(dest => dest.Biography, opt => opt.MapFrom(src => src.Biography))
                            .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.Photo)));
                return Mapper.Map<ArtistDetailsViewModel>(artist);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="viewModel">
        ///     The view model.
        /// </param>
        public int SaveNewArtist(ArtistDetailsViewModel viewModel)
        {
            using (var artistRepo = this.repositoryFactory.GetArtistRepository())
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
                var artist = Mapper.Map<Artist>(viewModel);
                artistRepo.AddOrUpdate(artist);
                artistRepo.SaveChanges();
                return artist.Id;
            }
        }
    }
}