namespace PVT.Q1._2017.Shop.Areas.Payment.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using global::Shop.BLL.Services;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.Common.ViewModels;
    using global::Shop.DAL.Infrastruture;
    using Shop.Controllers;
    using global::Shop.BLL.Exceptions;
    using BLL.Utils;
    /// <summary>
    /// Контоллер для корзины покупателя
    /// </summary>
    public class CartController : BaseController
    {
        /// <summary>
        /// Репозиторий для хранения корзины
        /// </summary>
        private ICartRepository _cartRepository;

        /// <summary>
        /// Сервис для работы с данными в корзине
        /// </summary>
        private ICartService _cartService;

        /// <summary>
        /// ViewModel для отображения корзины 
        /// </summary>
        private CartViewModel _viewModel;

        /// <summary>
        /// id текущего юзера 
        /// </summary>
        private int _currentUserId;

        /// <param name="cartService">
        /// Сервис для работы с данными в корзине
        /// </param>
        /// <param name="repositoryFactory">
        /// Фабрика для работы с репозиториями
        /// </param>
        public CartController(ICartService cartService, IRepositoryFactory repositoryFactory) 
        {
            this._cartRepository = repositoryFactory.GetCartRepository();
            this._cartService = cartService;
            this._viewModel = new CartViewModel { Tracks = new List<Track>(), Albums = new List<Album>(), IsEmpty = true };
            try
            {
                _currentUserId = CurrentUser.Id;
            }
            catch
            {
                _currentUserId = 0;
            }
        }

        /// <summary>
        /// Действие для отображения корзины
        /// </summary>
        [HttpGet]
        [Authorize]
        //TODO: отлавливать в параметре контроллера статус оплаты.
        public ViewResult Index()
        {
            if (_cartRepository.GetByUserId(_currentUserId) == null)
            {
                _cartRepository.AddOrUpdate(new Cart(_currentUserId));
                _cartRepository.SaveChanges();
            }
            var cart = this._cartRepository.GetByUserId(_currentUserId);
            this._viewModel.Tracks = _cartService.GetOrderTracks(_currentUserId);
            this._viewModel.Albums = _cartService.GetOrderAlbums(_currentUserId);
            this._viewModel.CurrentUserId = _currentUserId;

            /// <summary>
            /// Временные данные: пользователь выбрал отображение в долларах
            /// </summary>
            var userCurrency = new Currency();
            userCurrency.Code = 840;
            userCurrency.ShortName = "USD";
            this._viewModel.CurrencyShortName = userCurrency.ShortName;
            CartViewModelService.SetTotalPrice(this._viewModel, userCurrency);

            return this.View(this._viewModel);
        }

        /// <summary>
        /// Добавление альбома в корзину и перенаправление на действие отображения корзины
        /// </summary>
        /// <param name="albumId">
        /// Id добавляемого альбома
        /// </param>
        [HttpPost]
        [Authorize]
        public ActionResult AddAlbum(int albumId = 0)
        {
            try
            {
                this._cartService.AddAlbum(_currentUserId, albumId);
            }
            catch (InvalidTrackIdException ex)
            {
                //Logger.Log("Error : " + ex.Message);
                return HttpNotFound($"Извините, при выборе альбома произошла ошибка! Попробуйте позже!");
            }
            return this.RedirectToRoute(new { controller = "Cart", action = "Index"});
        }

        /// <summary>
        /// Удаление альбома из корзины и перенаправление на действие отображения корзины
        /// </summary>
        /// <param name="albumId">
        /// Id удаляемого альбома
        /// </param>
        [HttpPost]
        [Authorize]
        public ActionResult DeleteAlbum(int albumId = 0)
        {
            try
            {
                this._cartService.RemoveAlbum(_currentUserId, albumId);
            }
            catch (InvalidTrackIdException ex)
            {
                //Logger.Log("Error : " + ex.Message);
                return HttpNotFound($"Извините, при удалении альбома произошла ошибка! Попробуйте позже!");
            }

            return this.RedirectToRoute(new { controller = "Cart", action = "Index"});
        }

        /// <summary>
        /// Добавление трэка в корзину и перенаправление на действие отображения корзины
        /// </summary>
        /// <param name="trackId">
        /// Id добавляемого трэка
        /// </param>
        [HttpPost]
        [Authorize]
        public ActionResult AddTrack(int trackId = 0)
        {
            try
            {
                this._cartService.AddTrack(_currentUserId, trackId);
            }
            catch (InvalidTrackIdException ex)
            {
                //Logger.Log("Error : " + ex.Message);
                return HttpNotFound($"Извините, при выборе трэка произошла ошибка! Попробуйте позже!");
            }

            return this.RedirectToRoute(new { controller = "Cart", action = "Index"});
        }

        /// <summary>
        /// Удаление трэка из корзины и перенаправление на действие отображения корзины
        /// </summary>
        /// <param name="trackId">
        /// Id удаляемого трэка
        /// </param>
        [HttpPost]
        [Authorize]
        public ActionResult DeleteTrack(int trackId = 0)
        {
            try
            {
                this._cartService.RemoveTrack(_currentUserId, trackId);
            }
            catch (InvalidTrackIdException ex)
            {
                //Logger.Log("Error : " + ex.Message);
                return HttpNotFound($"Извините, при удалении трэка произошла ошибка! Попробуйте позже!");
            }

            return this.RedirectToRoute(new { controller = "Cart", action = "Index"});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _cartRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}