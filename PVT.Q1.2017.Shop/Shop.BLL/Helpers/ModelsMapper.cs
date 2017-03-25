namespace Shop.BLL.Helpers
{
    using AutoMapper;
    using Common.Models;
    using Common.Models.ViewModels;

    /// <summary>
    /// Default models mapper.
    /// </summary>
    public static class ModelsMapper
    {
        /// <summary>
        /// The mapper for models with detailed information.
        /// </summary>
        private static readonly IMapper ModelDetailsMapper;

        /// <summary>
        /// The common mapper.
        /// </summary>
        private static readonly IMapper CommonMapper;

        /// <summary>
        /// The mapper for models which have a list of other models.
        /// </summary>
        private static readonly IMapper SpecialListMapper;

        /// <summary>
        /// Initializes static members of the <see cref="ModelsMapper"/> class.
        /// </summary>
        static ModelsMapper()
        {
            ModelDetailsMapper = CreateModelsDetailsMapper();
            CommonMapper = CreateCommonMapper();
            SpecialListMapper = CreateSpecialListMapper();
        }

        /// <summary>
        /// Executes a mapping from the <see cref="Album"/> model to a new <see cref="AlbumDetailsViewModel"/> model.
        /// </summary>
        /// <param name="album">
        /// The album DTO model.
        /// </param>
        /// <returns>
        /// A new <see cref="AlbumDetailsViewModel"/> model.
        /// </returns>
        public static AlbumDetailsViewModel GetAlbumDetailsViewModel(Album album)
        {
            return ModelDetailsMapper.Map<AlbumDetailsViewModel>(album);
        }

        /// <summary>
        /// Executes a mapping from the <see cref="Album"/> model to a new <see cref="AlbumViewModel"/> model.
        /// </summary>
        /// <param name="album">
        /// The album DTO model.
        /// </param>
        /// <returns>
        /// A new <see cref="AlbumViewModel"/> model.
        /// </returns>
        public static AlbumViewModel GetAlbumViewModel(Album album)
        {
            return CommonMapper.Map<AlbumViewModel>(album);
        }

        /// <summary>
        /// Executes a mapping from the <see cref="Artist"/> model to a new <see cref="ArtistDetailsViewModel"/> model.
        /// </summary>
        /// <param name="artist">
        /// The artist DTO model.
        /// </param>
        /// <returns>
        /// A new <see cref="ArtistDetailsViewModel"/> model.
        /// </returns>
        public static ArtistDetailsViewModel GetArtistDetailsViewModel(Artist artist)
        {
            return ModelDetailsMapper.Map<ArtistDetailsViewModel>(artist);
        }

        /// <summary>
        /// Executes a mapping from the <see cref="Artist"/> model to a new <see cref="ArtistViewModel"/> model.
        /// </summary>
        /// <param name="artist">
        /// The artist DTO model.
        /// </param>
        /// <returns>
        /// A new <see cref="ArtistViewModel"/> model.
        /// </returns>
        public static ArtistViewModel GetArtistViewModel(Artist artist)
        {
            return CommonMapper.Map<ArtistViewModel>(artist);
        }

        /// <summary>
        /// Executes a mapping from the <see cref="Track"/> model to a new <see cref="TrackDetailsViewModel"/> model.
        /// </summary>
        /// <param name="track">
        /// The track DTO model.
        /// </param>
        /// <returns>
        /// A new <see cref="TrackDetailsViewModel"/> model.
        /// </returns>
        public static TrackDetailsViewModel GetTrackDetailsViewModel(Track track)
        {
            return ModelDetailsMapper.Map<TrackDetailsViewModel>(track);
        }

        /// <summary>
        /// Executes a mapping from the <see cref="Track"/> model to a new <see cref="TrackViewModel"/> model.
        /// </summary>
        /// <param name="track">
        /// The track DTO model.
        /// </param>
        /// <returns>
        /// A new <see cref="TrackViewModel"/> model.
        /// </returns>
        public static TrackViewModel GetTrackViewModel(Track track)
        {
            return CommonMapper.Map<TrackViewModel>(track);
        }

        /// <summary>
        /// Executes a mapping from the <see cref="Currency"/> model to a new <see cref="CurrencyViewModel"/> model.
        /// </summary>
        /// <param name="currency">
        /// The currency DTO model.
        /// </param>
        /// <returns>
        /// A new <see cref="CurrencyViewModel"/> model.
        /// </returns>
        public static CurrencyViewModel GetCurrencyViewModel(Currency currency)
        {
            return CommonMapper.Map<CurrencyViewModel>(currency);
        }

        /// <summary>
        /// Executes a mapping from the <see cref="Genre"/> model to a new <see cref="GenreViewModel"/> model.
        /// </summary>
        /// <param name="genre">
        /// The genre DTO model.
        /// </param>
        /// <returns>
        /// A new <see cref="GenreViewModel"/> model.
        /// </returns>
        public static GenreViewModel GetGenreViewModel(Genre genre)
        {
            return CommonMapper.Map<GenreViewModel>(genre);
        }

        /// <summary>
        /// Executes a mapping from the <see cref="TrackPrice"/> model to a new <see cref="PriceViewModel"/> model.
        /// </summary>
        /// <param name="trackPrice">
        /// The track price DTO model.
        /// </param>
        /// <returns>
        /// A new <see cref="PriceViewModel"/> model.
        /// </returns>
        public static PriceViewModel GetPriceViewModel(TrackPrice trackPrice)
        {
            return CommonMapper.Map<PriceViewModel>(trackPrice);
        }

        /// <summary>
        /// Executes a mapping from the <see cref="AlbumPrice"/> model to a new <see cref="PriceViewModel"/> model.
        /// </summary>
        /// <param name="albumPrice">
        /// The album price DTO model.
        /// </param>
        /// <returns>
        /// A new <see cref="PriceViewModel"/> model.
        /// </returns>
        public static PriceViewModel GetPriceViewModel(AlbumPrice albumPrice)
        {
            return CommonMapper.Map<PriceViewModel>(albumPrice);
        }

        /// <summary>
        /// Executes a mapping from the <see cref="Track"/> model to a new <see cref="TrackAlbumsListViewModel"/> model.
        /// </summary>
        /// <param name="track">
        /// The track DTO model.
        /// </param>
        /// <returns>
        /// A new <see cref="TrackAlbumsListViewModel"/> model.
        /// </returns>
        public static TrackAlbumsListViewModel GetTrackAlbumsListViewModel(Track track)
        {
            return SpecialListMapper.Map<TrackAlbumsListViewModel>(track);
        }

        /// <summary>
        /// Executes a mapping from the <see cref="Album"/> model to a new <see cref="AlbumTracksListViewModel"/> model.
        /// </summary>
        /// <param name="album">
        /// The album DTO model.
        /// </param>
        /// <returns>
        /// A new <see cref="TrackAlbumsListViewModel"/> model.
        /// </returns>
        public static AlbumTracksListViewModel GetAlbumTracksListViewModel(Album album)
        {
            return SpecialListMapper.Map<AlbumTracksListViewModel>(album);
        }

        /// <summary>
        /// Executes a mapping from the <see cref="Feedback"/> model to a new <see cref="FeedbackViewModel"/> model.
        /// </summary>
        /// <param name="feedback">
        /// The feedback DTO model.
        /// </param>
        /// <returns>
        /// A new <see cref="FeedbackViewModel"/> model.
        /// </returns>
        public static FeedbackViewModel GetFeedbackViewModel(Feedback feedback)
        {
            return CommonMapper.Map<FeedbackViewModel>(feedback);
        }

        /// <summary>
        /// Configures and returns a new instance of the common mapper.
        /// </summary>
        /// <returns>
        /// A new instance of the common mapper.
        /// </returns>
        private static IMapper CreateCommonMapper()
        {
            var commonMapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Artist, ArtistViewModel>();

                cfg.CreateMap<Artist, ArtistDetailsViewModel>();

                cfg.CreateMap<Genre, GenreViewModel>();

                cfg.CreateMap<Currency, CurrencyViewModel>();

                cfg.CreateMap<Album, AlbumViewModel>()
                   .ForMember(dest => dest.Artist, opt => opt.MapFrom(a => a.Artist));

                cfg.CreateMap<Track, TrackViewModel>()
                   .ForMember(dest => dest.Artist, opt => opt.MapFrom(t => t.Artist));

                cfg.CreateMap<TrackPrice, PriceViewModel>()
                   .ForMember(dest => dest.Amount, opt => opt.ResolveUsing(p => p.Price))
                   .ForMember(dest => dest.Currency, opt => opt.MapFrom(p => p.Currency));

                cfg.CreateMap<AlbumPrice, PriceViewModel>()
                   .ForMember(dest => dest.Amount, opt => opt.ResolveUsing(p => p.Price))
                   .ForMember(dest => dest.Currency, opt => opt.MapFrom(p => p.Currency));

                cfg.CreateMap<Feedback, FeedbackViewModel>()
                   .ForMember(dest => dest.UserDataId, opt => opt.ResolveUsing(f => f.UserId));
            });

            return commonMapperConfiguration.CreateMapper();
        }

        /// <summary>
        /// Configures and returns a new instance of the mapper for models which have a list of other models.
        /// </summary>
        /// <returns>
        /// A new instance of the mapper for models which have a list of other models.
        /// </returns>
        private static IMapper CreateSpecialListMapper()
        {
            var specialListMapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Artist, ArtistViewModel>();

                cfg.CreateMap<Track, TrackAlbumsListViewModel>()
                   .ForMember(dest => dest.Artist, opt => opt.MapFrom(t => t.Artist));

                cfg.CreateMap<Album, AlbumTracksListViewModel>()
                   .ForMember(dest => dest.Artist, opt => opt.MapFrom(t => t.Artist));
            });

            return specialListMapperConfiguration.CreateMapper();
        }

        /// <summary>
        /// Configures and returns a new instance of the mapper for models with detailed information.
        /// </summary>
        /// <returns>
        /// A new instance of the mapper for models with detailed information.
        /// </returns>
        private static IMapper CreateModelsDetailsMapper()
        {
            var detailsMapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Genre, GenreViewModel>();

                cfg.CreateMap<Artist, ArtistViewModel>();

                cfg.CreateMap<Artist, ArtistDetailsViewModel>()
                    .ForMember(model => model.Photo, opt => opt.MapFrom(src => src.Photo));

                cfg.CreateMap<ArtistDetailsViewModel, Artist>()
                  .ForMember(model => model.Photo, opt => opt.MapFrom(src => src.Photo));

                cfg.CreateMap<Album, AlbumDetailsViewModel>()
                   .ForMember(dest => dest.Artist, opt => opt.MapFrom(a => a.Artist));

                cfg.CreateMap<Track, TrackDetailsViewModel>()
                   .ForMember(dest => dest.Artist, opt => opt.MapFrom(t => t.Artist))
                   .ForMember(dest => dest.Genre, opt => opt.MapFrom(t => t.Genre));
            });

            return detailsMapperConfiguration.CreateMapper();
        }
    }
}