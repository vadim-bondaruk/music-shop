namespace Shop.BLL
{
    using AutoMapper;

    using Shop.Common.Models;
    using Shop.Common.Models.ViewModels;

    /// <summary>
    ///     Default models mapper.
    /// </summary>
    public static class DefaultModelsMapper
    {
        /// <summary>
        ///     Maps models and view-models.
        /// </summary>
        public static void MapModels()
        {
            Mapper.Initialize(
                cfg =>
                    {
                        cfg.CreateMap<ArtistManageViewModel, Artist>()
                            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday))
                            .ForMember(dest => dest.Biography, opt => opt.MapFrom(src => src.Biography))
                            .ReverseMap();

                        cfg.CreateMap<AlbumManageViewModel, Album>()
                            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                            .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate))
                            .ForMember(dest => dest.Cover, opt => opt.ResolveUsing(src => src.Cover))
                            .ReverseMap();
                    });
        }
    }
}