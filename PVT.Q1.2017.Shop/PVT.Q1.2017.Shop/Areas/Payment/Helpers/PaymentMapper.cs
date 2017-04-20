
namespace PVT.Q1._2017.Shop.Areas.Payment.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using global::Shop.Common.Models;
    using global::Shop.Common.ViewModels;
    using global::Shop.Infrastructure.Models;

    public class PaymentMapper
    {

        /// <summary>
        ///     The mapper for models with detailed information.
        /// </summary>
        private static readonly IMapper _paymentModelsMapper;

        /// <summary>
        ///     Initializes static members of the <see cref="PaymentMapper" /> class.
        /// </summary>
        static PaymentMapper()
        {
            _paymentModelsMapper = CreatePaymentMapper();
        }

        /// <summary>
        /// New instance of Payment transaction view model
        /// </summary>
        /// <param name="paymentTransaction">source payment transaction</param>
        /// <returns></returns>
        public static PaymentTransactionViewModel GetPaymentTransactionViewModel(PaymentTransaction paymentTransaction)
        {
            return _paymentModelsMapper.Map<PaymentTransactionViewModel>(paymentTransaction);
        }

        /// <summary>
        /// New instance of Payment transaction view models in IEnamerable collection
        /// </summary>
        /// <param name="paymentTransaction">source payment transaction</param>
        /// <returns></returns>
        public static IEnumerable<PaymentTransactionViewModel> GetPaymentTransactionViewModels(IEnumerable<PaymentTransaction> paymentTransactions)
        {
            var result = new List<PaymentTransactionViewModel>();
            if (paymentTransactions != null)
            {
                foreach (var transaction in paymentTransactions)
                {
                    result.Add(_paymentModelsMapper.Map<PaymentTransactionViewModel>(transaction));
                }
            }
            return result;
        }

        public static PagedResult<PaymentTransactionViewModel> GetPaymentTransactionsToEdit(PagedResult<PaymentTransaction> paymentTransactions)
        {
            return new PagedResult<PaymentTransactionViewModel>(_paymentModelsMapper.Map<ICollection<PaymentTransactionViewModel>>(paymentTransactions.Items),
                                                 paymentTransactions.PageSize, paymentTransactions.CurrentPage, paymentTransactions.TotalItemsCount);
        }

        /// <summary>
        ///     Configures and returns a new instance of the mapper for models which have a list of other models.
        /// </summary>
        /// <returns>
        ///     A new instance of the mapper for models which have a list of other models.
        /// </returns>
        private static IMapper CreatePaymentMapper()
        {
            var managementConfiguration = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<PaymentTransaction, PaymentTransactionViewModel>()
                        .ForMember(dest => dest.UserName,
                                    //opt => opt.MapFrom(src => src.User.User.Login))
                                    opt => opt.MapFrom(src => src.User.User.Login))
                        .ForMember(dest => dest.CurrencyShortName,
                                    opt => opt.MapFrom(src => src.Currency.ShortName));


                    //cfg.CreateMap<ArtistViewModel, Artist>();

                    //cfg.CreateMap<ArtistManagementViewModel, Artist>()
                    //    .ForMember(
                    //        dest => dest.Photo,
                    //        opt =>
                    //            opt.MapFrom(src => src.PostedPhoto != null ? src.PostedPhoto.ToBytes() : src.Photo));

                    //cfg.CreateMap<GenreViewModel, Genre>();
                    //cfg.CreateMap<GenreDetailsViewModel, GenreManagementViewModel>();

                    //cfg.CreateMap<Track, TrackManagementViewModel>();

                    //cfg.CreateMap<TrackDetailsViewModel, TrackManagementViewModel>()
                    //    .ForMember(dst => dst.Price, opt => opt.MapFrom(src => src.Price.Amount));

                    //cfg.CreateMap<TrackManagementViewModel, Track>()
                    //    .ForMember(dest => dest.Artist, opt => opt.MapFrom(src => src.Artist))
                    //    .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre))
                    //    .ForMember(
                    //        dest => dest.TrackFile,
                    //        opt =>
                    //            opt.MapFrom(
                    //                src =>
                    //                    src.PostedTrackFile != null ? src.PostedTrackFile.ToBytes() : src.TrackFile))
                    //    .ForMember(
                    //        dest => dest.Image,
                    //        opt =>
                    //            opt.MapFrom(src => src.PostedImage != null ? src.PostedImage.ToBytes() : src.Image));

                    //cfg.CreateMap<ArtistDetailsViewModel, ArtistManagementViewModel>()
                    //    .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.Photo));

                    //cfg.CreateMap<Album, AlbumManagementViewModel>();

                    //cfg.CreateMap<AlbumDetailsViewModel, AlbumManagementViewModel>();

                    //cfg.CreateMap<AlbumManagementViewModel, Album>()
                    //    .ForMember(dest => dest.Artist, opt => opt.MapFrom(src => src.Artist))
                    //    .ForMember(
                    //        dest => dest.Cover,
                    //        opt =>
                    //            opt.MapFrom(src => src.PostedCover == null ? src.Cover : src.PostedCover.ToBytes()));
                });

            return managementConfiguration.CreateMapper();
        }
    }
}