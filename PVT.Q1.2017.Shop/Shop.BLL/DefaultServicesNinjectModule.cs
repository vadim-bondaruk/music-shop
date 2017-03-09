﻿namespace Shop.BLL
{
    using DAL;
    using Ninject;
    using Ninject.Modules;
    using Services;
    using Services.Infrastructure;

    /// <summary>
    /// Default cofiguration module.
    /// </summary>
    public class DefaultServicesNinjectModule : NinjectModule
    {
        /// <summary>
        /// Loads configuration settings.
        /// </summary>
        public override void Load()
        {
            if (this.Kernel != null)
            {
                this.Kernel.Load(new DefaultRepositoriesNinjectModule());
            }

            this.BindServices();
        }
        
        /// <summary>
        /// Binds services.
        /// </summary>
        protected virtual void BindServices()
        {
            Bind<ITrackService>().To<TrackService>();
            Bind<IArtistService>().To<ArtistService>();
            Bind<IAlbumService>().To<AlbumService>();

            Bind<IVoteService>().To<VoteService>();
            Bind<IFeedbackService>().To<FeedbackService>();

            Bind<ITrackPriceService>().To<TrackPriceService>();
            Bind<IAlbumPriceService>().To<AlbumPriceService>();

            Bind<IUserService>().To<UserService>();

            Bind<ICurrencyService>().To<CurrencyService>();

            Bind<ICurrencyRateService>().To<CurrencyRateService>();
        }
    }
}