namespace Shop.BLL
{
    using AutoMapper;
    using Common.Models;
    using ViewModels;

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
        }
    }
}