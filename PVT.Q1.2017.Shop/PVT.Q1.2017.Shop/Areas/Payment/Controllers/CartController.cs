namespace PVT.Q1._2017.Shop.Areas.Payment.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using global::Shop.BLL.Services;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.Common.ViewModels;
    using global::Shop.DAL.Infrastruture;

    /// <summary>
    /// Контоллер для корзины покупателя
    /// </summary>
    public class CartController : Controller
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

        /// <param name="cartService">
        /// Сервис для работы с данными в корзине
        /// </param>
        /// <param name="repositoryFactory">
        /// Фабрика для работы с репозиториями
        public CartController(ICartService cartService, IRepositoryFactory repositoryFactory) 
        {
            this._cartRepository = repositoryFactory.GetCartRepository();
            this._cartService = cartService;
            this._viewModel = new CartViewModel { Tracks = new List<Track>(), Albums = new List<Album>() };
        }

        /// <summary>
        /// Действие для отображения корзины
        /// </summary>
        /// <param name="currentUserId">
        /// Id текущего пользователя
        /// </param>
        [HttpGet]
        [Authorize]
        public ViewResult Index(int currentUserId = 0)
        {
            var cart = this._cartRepository.GetAll(c => c.UserId == currentUserId).FirstOrDefault();
            this._viewModel.Tracks = (ICollection<Track>)cart?.Tracks;
            this._viewModel.Albums = (ICollection<Album>)cart?.Albums;
            this._viewModel.CurrentUserId = currentUserId;

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
        /// <param name="currentUserId">
        /// Id текущего пользователя
        /// </param>
        /// <param name="albumId">
        /// Id добавляемого альбома
        /// </param>
        [HttpPost]
        [Authorize]
        public RedirectToRouteResult AddAlbum(int currentUserId = 0, int albumId = 0)
        {
            this._cartService.AddAlbum(currentUserId, albumId);
            return this.RedirectToRoute(new { controller = "Cart", action = "Index", currentUserId = currentUserId });
        }

        /// <summary>
        /// Удаление альбома из корзины и перенаправление на действие отображения корзины
        /// </summary>
        /// <param name="currentUserId">
        /// Id текущего пользователя
        /// </param>
        /// <param name="albumId">
        /// Id удаляемого альбома
        /// </param>
        [HttpPost]
        public RedirectToRouteResult DeleteAlbum(int currentUserId = 0, int albumId = 0)
        {
            this._cartService.RemoveAlbum(currentUserId, albumId);
            return this.RedirectToRoute(new { controller = "Cart", action = "Index", currentUserId = currentUserId });
        }

        /// <summary>
        /// Добавление трэка в корзину и перенаправление на действие отображения корзины
        /// </summary>
        /// <param name="currentUserId">
        /// Id текущего пользователя
        /// </param>
        /// <param name="trackId">
        /// Id добавляемого трэка
        /// </param>
        [HttpPost]
        [Authorize]
        public RedirectToRouteResult AddTrack(int currentUserId = 0, int trackId = 0)
        {
            this._cartService.AddTrack(currentUserId, trackId);
            return this.RedirectToRoute(new { controller = "Cart", action = "Index", currentUserId = currentUserId });
        }

        /// <summary>
        /// Удаление трэка из корзины и перенаправление на действие отображения корзины
        /// </summary>
        /// <param name="currentUserId">
        /// Id текущего пользователя
        /// </param>
        /// <param name="trackId">
        /// Id удаляемого трэка
        /// </param>
        [HttpPost]
        [Authorize]
        public RedirectToRouteResult DeleteTrack(int currentUserId = 0, int trackId = 0)
        {
            this._cartService.RemoveTrack(currentUserId, trackId);
            return this.RedirectToRoute(new { controller = "Cart", action = "Index", currentUserId = currentUserId });
        }
    }
}