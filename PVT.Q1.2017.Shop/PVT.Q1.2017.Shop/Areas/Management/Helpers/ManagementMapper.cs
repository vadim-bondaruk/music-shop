namespace PVT.Q1._2017.Shop.Areas.Management.Helpers
{
    using AutoMapper;
    using Extensions;
    using global::Shop.Common.Models;
    using global::Shop.Common.ViewModels;
    using ViewModels;

    /// <summary>
    /// The management mapper
    /// </summary>
    internal static class ManagementMapper
    {
        /// <summary>
        /// The mapper for models with detailed information.
        /// </summary>
        private static readonly IMapper _managementModelsMapper;

        /// <summary>
        /// Initializes static members of the <see cref="ManagementMapper"/> class.
        /// </summary>
        static ManagementMapper()
        {
            _managementModelsMapper = CreateManagementMapper();
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
            return _managementModelsMapper.Map<Track>(track);
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
            return _managementModelsMapper.Map<TrackManagementViewModel>(track);
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
            return _managementModelsMapper.Map<Album>(album);
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
            return _managementModelsMapper.Map<AlbumManagementViewModel>(album);
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

                cfg.CreateMap<TrackDetailsViewModel, TrackManagementViewModel>();

                cfg.CreateMap<TrackManagementViewModel, Track>()
                   .ForMember(dest => dest.Artist, opt => opt.MapFrom(src => src.Artist))
                   .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre))
                   .ForMember(dest => dest.TrackFile, opt => opt.ResolveUsing(src => src.PostedTrackFile.ToBytes()))
                   .ForMember(dest => dest.TrackSample, opt => opt.ResolveUsing(src => src.PostedTrackSample.ToBytes()))
                   .ForMember(dest => dest.Image, opt => opt.ResolveUsing(src => src.PostedImage.ToBytes()));

                cfg.CreateMap<AlbumDetailsViewModel, AlbumManagementViewModel>()
                   .ForMember(dest => dest.ArtistId, opt => opt.MapFrom(src => src.Artist.Id))
                   .ForMember(dest => dest.ArtistName, opt => opt.MapFrom(src => src.Artist.Name));

                cfg.CreateMap<AlbumManagementViewModel, Album>()
                                            .ForMember(dest => dest.Cover, opt => opt.ResolveUsing(src => src.PostedCover.ToBytes()));
            });

            return managementConfiguration.CreateMapper();
        }
    }
}