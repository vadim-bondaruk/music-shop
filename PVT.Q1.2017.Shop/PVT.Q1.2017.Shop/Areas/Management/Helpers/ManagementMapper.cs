namespace PVT.Q1._2017.Shop.Areas.Management.Helpers
{
    using System.Web;
    using AutoMapper;
    using Extensions;
    using global::Shop.Common.Models;
    using global::Shop.Common.Models.ViewModels;
    using ViewModels;

    /// <summary>
    /// The management mapper
    /// </summary>
    internal static class ManagementMapper
    {
        /// <summary>
        /// The mapper for models with detailed information.
        /// </summary>
        private static readonly IMapper ManagementModelsMapper;

        /// <summary>
        /// Initializes static members of the <see cref="ManagementMapper"/> class.
        /// </summary>
        static ManagementMapper()
        {
            ManagementModelsMapper = CreateManagementMapper();
        }

        /// <summary>
        /// Executes a mapping from the <see cref="TrackManagementViewModel"/> model to a new <see cref="Track"/> model.
        /// </summary>
        /// <param name="track">
        /// The track management view model.
        /// </param>
        /// <returns>
        /// A new <see cref="Track"/> DTO model.
        /// </returns>
        public static Track GetTrackModel(TrackManagementViewModel track)
        {
            return ManagementModelsMapper.Map<Track>(track);
        }

        /// <summary>
        /// Executes a mapping from the <see cref="TrackDetailsViewModel"/> model to a new <see cref="TrackManagementViewModel"/> model.
        /// </summary>
        /// <param name="track">
        /// The track domain model.
        /// </param>
        /// <returns>
        /// A new <see cref="TrackManagementViewModel"/> model.
        /// </returns>
        public static TrackManagementViewModel GetTrackManagementViewModel(TrackDetailsViewModel track)
        {
            return ManagementModelsMapper.Map<TrackManagementViewModel>(track);
        }

        /// <summary>
        /// Executes a mapping from the <see cref="AlbumManagementViewModel"/> model to a new <see cref="Album"/> model.
        /// </summary>
        /// <param name="album">
        /// The album management view model.
        /// </param>
        /// <returns>
        /// A new <see cref="Album"/> DTO model.
        /// </returns>
        public static Album GetAlbumModel(AlbumManagementViewModel album)
        {
            return ManagementModelsMapper.Map<Album>(album);
        }

        /// <summary>
        /// Executes a mapping from the <see cref="AlbumDetailsViewModel"/> model to a new <see cref="AlbumManagementViewModel"/> model.
        /// </summary>
        /// <param name="album">
        /// The album domain model.
        /// </param>
        /// <returns>
        /// A new <see cref="AlbumManagementViewModel"/> model.
        /// </returns>
        public static AlbumManagementViewModel GetAlbumManagementViewModel(AlbumDetailsViewModel album)
        {
            return ManagementModelsMapper.Map<AlbumManagementViewModel>(album);
        }

        /// <summary>
        /// Configures and returns a new instance of the mapper for models which have a list of other models.
        /// </summary>
        /// <returns>
        /// A new instance of the mapper for models which have a list of other models.
        /// </returns>
        private static IMapper CreateManagementMapper()
        {
            MapperConfiguration managementConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ArtistViewModel, Artist>();
                cfg.CreateMap<GenreViewModel, Genre>();

                cfg.CreateMap<TrackDetailsViewModel, TrackManagementViewModel>()
                                            .ForMember(dest => dest.Image, opt => opt.UseValue<HttpPostedFileBase>(null))
                                            .ForMember(dest => dest.TrackFile, opt => opt.UseValue<HttpPostedFileBase>(null));

                cfg.CreateMap<TrackManagementViewModel, Track>()
                                            .ForMember(dest => dest.Artist, opt => opt.MapFrom(src => src.Artist))
                                            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre))
                                            .ForMember(dest => dest.TrackFile, opt => opt.ResolveUsing(src => src.TrackFile.ToBytes()))
                                            .ForMember(dest => dest.Image, opt => opt.ResolveUsing(src => src.Image.ToBytes()));

                cfg.CreateMap<AlbumDetailsViewModel, AlbumManagementViewModel>()
                                            .ForMember(dest => dest.Cover, opt => opt.UseValue<HttpPostedFileBase>(null));

                cfg.CreateMap<AlbumManagementViewModel, Album>()
                                            .ForMember(dest => dest.Artist, opt => opt.MapFrom(src => src.Artist))
                                            .ForMember(dest => dest.Cover, opt => opt.ResolveUsing(src => src.Cover.ToBytes()));
            });

            return managementConfiguration.CreateMapper();
        }
    }
}