namespace PVT.Q1._2017.Shop.Areas.Management.Helpers
{
    using System.Web;

    using AutoMapper;

    using global::Shop.Common.Models;
    using global::Shop.Common.Models.ViewModels;

    using PVT.Q1._2017.Shop.Areas.Management.Extensions;
    using PVT.Q1._2017.Shop.Areas.Management.ViewModels;

    /// <summary>
    ///     The management mapper
    /// </summary>
    internal static class ManagementMapper
    {
        /// <summary>
        ///     The mapper for models with detailed information.
        /// </summary>
        private static readonly IMapper ManagementModelsMapper;

        /// <summary>
        ///     Initializes static members of the <see cref="ManagementMapper" /> class.
        /// </summary>
        static ManagementMapper()
        {
            ManagementModelsMapper = CreateManagementMapper();
        }

        /// <summary>
        /// </summary>
        /// <param name="album">
        ///     The album.
        /// </param>
        /// <returns>
        /// </returns>
        public static AlbumManagementViewModel GetAlbumManagementViewModel(Album album)
        {
            return ManagementModelsMapper.Map<AlbumManagementViewModel>(album);
        }

        /// <summary>
        ///     Executes a mapping from the <see cref="AlbumManagementViewModel" /> model to a new <see cref="Album" /> model.
        /// </summary>
        /// <param name="album">
        ///     The album management view model.
        /// </param>
        /// <returns>
        ///     A new <see cref="Album" /> DTO model.
        /// </returns>
        public static Album GetAlbumModel(AlbumManagementViewModel album)
        {
            return ManagementModelsMapper.Map<Album>(album);
        }

        /// <summary>
        /// </summary>
        /// <param name="album">
        ///     The album.
        /// </param>
        /// <param name="viewModel"></param>
        /// <returns>
        /// </returns>
        public static ArtistManagementViewModel GetArtistManagementViewModel(ArtistDetailsViewModel viewModel)
        {
            return ManagementModelsMapper.Map<ArtistManagementViewModel>(viewModel);
        }

        /// <summary>
        /// </summary>
        /// <param name="viewModel">
        ///     The view model.
        /// </param>
        /// <returns>
        /// </returns>
        public static Artist GetArtistModel(ArtistManagementViewModel viewModel)
        {
            return ManagementModelsMapper.Map<Artist>(viewModel);
        }

        /// <summary>
        ///     Executes a mapping from the <see cref="TrackDetailsViewModel" /> model to a new
        ///     <see cref="TrackManagementViewModel" /> model.
        /// </summary>
        /// <param name="track">
        ///     The track domain model.
        /// </param>
        /// <returns>
        ///     A new <see cref="TrackManagementViewModel" /> model.
        /// </returns>
        public static TrackManagementViewModel GetTrackManagementViewModel(TrackDetailsViewModel track)
        {
            return ManagementModelsMapper.Map<TrackManagementViewModel>(track);
        }

        /// <summary>
        ///     Executes a mapping from the <see cref="TrackManagementViewModel" /> model to a new <see cref="Track" /> model.
        /// </summary>
        /// <param name="trackManagementViewModel">
        ///     The track management view model.
        /// </param>
        /// <returns>
        ///     A new <see cref="Track" /> DTO model.
        /// </returns>
        public static Track GetTrackModel(TrackManagementViewModel trackManagementViewModel)
        {
            return ManagementModelsMapper.Map<Track>(trackManagementViewModel);
        }

        /// <summary>
        ///     Configures and returns a new instance of the mapper for models which have a list of other models.
        /// </summary>
        /// <returns>
        ///     A new instance of the mapper for models which have a list of other models.
        /// </returns>
        private static IMapper CreateManagementMapper()
        {
            var managementConfiguration = new MapperConfiguration(
                cfg =>
                    {
                        cfg.CreateMap<GenreViewModel, Genre>();

                        cfg.CreateMap<TrackDetailsViewModel, TrackManagementViewModel>()
                            .ForMember(dest => dest.Image, opt => opt.UseValue<HttpPostedFileBase>(null))
                            .ForMember(dest => dest.TrackFile, opt => opt.UseValue<HttpPostedFileBase>(null));

                        cfg.CreateMap<TrackManagementViewModel, Track>()
                            .ForMember(dest => dest.Artist, opt => opt.MapFrom(src => src.Artist))
                            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre))
                            .ForMember(dest => dest.TrackFile, opt => opt.ResolveUsing(src => src.TrackFile.ToBytes()))
                            .ForMember(dest => dest.Image, opt => opt.ResolveUsing(src => src.Image.ToBytes()));

                        cfg.CreateMap<AlbumDetailsViewModel, Album>()
                            .ForMember(dest => dest.Cover, opt => opt.UseValue<HttpPostedFileBase>(null));

                        cfg.CreateMap<AlbumManagementViewModel, Album>()
                            .ForMember(dest => dest.Artist, opt => opt.MapFrom(src => src.Artist))
                            .ForMember(dest => dest.Cover, opt => opt.ResolveUsing(src => src.Cover.ToBytes()));

                        cfg.CreateMap<ArtistManagementViewModel, Artist>()
                            .ForMember(
                                dest => dest.Photo,
                                opt =>
                                    opt.MapFrom(
                                        src =>
                                            src.PostedPhoto !=null
                                                ? src.PostedPhoto.ToBytes()
                                                : src.Photo));

                        cfg.CreateMap<ArtistDetailsViewModel, ArtistManagementViewModel>()
                            .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.Photo));
                    });

            return managementConfiguration.CreateMapper();
        }
    }
}