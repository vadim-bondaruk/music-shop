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
    using System;

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

        /// <summary>
        /// валюта текущего пользователя
        /// </summary>
        private CurrencyViewModel _userCurrency;

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
            this._viewModel = new CartViewModel { Tracks = new List<TrackDetailsViewModel>(),
                Albums = new List<AlbumDetailsViewModel>(),
                IsEmpty = true };
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
            this._viewModel.Tracks = _cartService.GetOrderTracks(_currentUserId, _userCurrency.Code);
            this._viewModel.Albums = _cartService.GetOrderAlbums(_currentUserId, _userCurrency.Code);
            this._viewModel.CurrentUserId = _currentUserId;

            //// Установка текущей валюты пользователя и пересчёт суммы к оплате
            if (_currentUserId > 0)
            {
                this._viewModel.CurrencyShortName = _userCurrency.ShortName;
                CartViewModelService.SetTotalPrice(this._viewModel, _userCurrency);
                TempData["cart"] = _viewModel;
            }

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
            catch (InvalidAlbumIdException ex)
            {
                //Logger.Log("Error : " + ex.Message);
                return HttpNotFound($"Извините, при выборе альбома произошла ошибка! Попробуйте позже!");
            }
            return this.RedirectToAction("Index", "Cart", new { Area = "Payment" });
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
            catch (InvalidAlbumIdException ex)
            {
                //Logger.Log("Error : " + ex.Message);
                return HttpNotFound($"Извините, при удалении альбома произошла ошибка! Попробуйте позже!");
            }

            return this.RedirectToAction("Index", "Cart", new { Area = "Payment" });
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

            return this.RedirectToAction("Index", "Cart", new { Area = "Payment" });
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

            return this.RedirectToAction("Index", "Cart", new { Area = "Payment" });
        }

        /// <summary>
        /// Очищает всю корзину текущего пользователя
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult ClearCart()
        {
            this._cartService.RemoveAll(_currentUserId);
            return this.RedirectToAction("Index", "Cart", new { Area = "Payment" });
        }

        /// <summary>
        /// Перемещает товары пользователя в купленные при успешной оплате
        /// </summary>
        /// <param name="isAccepted"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult AcceptPayment(bool isAccepted)
        {
            if (isAccepted)
            {
                _cartService.AcceptPayment(_currentUserId);
                return this.RedirectToAction("Index", "Home", new { Area = string.Empty });
            }
            return this.RedirectToAction("Index", "Cart", new { Area = "Payment" });
        }

        /// <summary>
        /// Возвращает количество заказов покупателя в корзине
        /// </summary>
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public JsonResult GetOrdersCount()
        {
            int count = 0;

            var cart = _cartRepository.GetByUserId(_currentUserId);
            if (cart != null)
            {
                count += cart.OrderTracks.Count;
                count += cart.OrderAlbums.Count;
                if (Session != null)
                {
                    Session["OrdersCount"] = count;
                }
            }

            return Json(new { Count = count }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Set current user from base controller
        /// </summary>
        public void SetCurrentUser()
        {
            /// TODO: Moq для HttpConext чтобы убрать try-catch
            try
            {
                _currentUserId = CurrentUser == null ? 0 : CurrentUser.Id;
                _userCurrency = this.GetCurrentUserCurrency();
            }
            catch (NullReferenceException)
            {
                _userCurrency = null;
            }

            if (_userCurrency == null)
            {
                _userCurrency = new CurrencyViewModel()
                { FullName = "EURO", ShortName = "EUR", Code = 978 };
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            SetCurrentUser();
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