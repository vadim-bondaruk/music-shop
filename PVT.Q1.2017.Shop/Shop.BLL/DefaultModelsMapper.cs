namespace Shop.BLL
{
    using AutoMapper;
    using Common.Models;
    using Common.Models.ViewModels;

    /// <summary>
    /// Default models mapper.
    /// </summary>
    public static class DefaultModelsMapper
    {
        /// <summary>
        /// Maps models and view-models.
        /// </summary>
        public static void MapModels()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Album, AlbumViewModel>());

            Mapper.Initialize(
                cfg =>
                    cfg.CreateMap<ArtistDetailsViewModel, Artist>()
                        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                        .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday))
                        .ForMember(dest => dest.Biography, opt => opt.MapFrom(src => src.Biography))
                        .ForMember(dest => dest.Photo, opt => opt.ResolveUsing(src => src.Photo)));
        }
    }
}