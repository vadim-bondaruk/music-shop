namespace PVT.Q1._2017.Shop
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Areas.Management.Extensions;
    using Areas.Management.ViewModels;
    using AutoMapper;
    using FluentValidation.Mvc;
    using global::Shop.Common.Models;
    using global::Shop.Common.Models.ViewModels;

    /// <summary>
    ///     Base class in an ASP.NET application
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        /// <summary>
        ///     Start application
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            FluentValidationModelValidatorProvider.Configure();

            // temporary... will be removed in the future...
            Mapper.Initialize(cfg => cfg.CreateMap<TrackDetailsViewModel, TrackManagementViewModel>()
                                        .ForMember(dest => dest.Image, opt => opt.UseValue<HttpPostedFileBase>(null))
                                        .ForMember(dest => dest.TrackFile, opt => opt.UseValue<HttpPostedFileBase>(null)));

            Mapper.Initialize(cfg => cfg.CreateMap<TrackManagementViewModel, Track>()
                                        .ForMember(dest => dest.ArtistId, opt => opt.ResolveUsing(src => src.Artist.Id))
                                        .ForMember(dest => dest.GenreId, opt => opt.ResolveUsing(src => src.Genre.Id))
                                        .ForMember(dest => dest.TrackFile, opt => opt.ResolveUsing(src => src.TrackFile.ToBytes()))
                                        .ForMember(dest => dest.Image, opt => opt.ResolveUsing(src => src.Image)));
        }
    }
}