namespace PVT.Q1._2017.Shop.Controllers.Cart
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Areas.Payment.Services;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;
    using ViewModels;

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
        /// Репозиторий для хранения пользователей 
        /// </summary>
        private IUserRepository _userRepository;

        /// <summary>
        /// Репозиторий для хранения трэков
        /// </summary>
        private ITrackRepository _trackRepository;

        /// <summary>
        /// ViewModel для отображения корзины 
        /// </summary>
        private CartViewModel _viewModel;

        /// <summary>
        /// Конструктор для контроллера корзины
        /// </summary>
        /// <param name="cartRepo">
        /// Репозиторий для хранения корзины
        /// </param>
        /// <param name="userRepo">
        /// Репозиторий для хранения пользователей
        /// </param>
        public CartController(ICartRepository cartRepo, IUserRepository userRepo, ITrackRepository trackRepo)
        {
            this._cartRepository = cartRepo;
            this._userRepository = userRepo;
            this._trackRepository = trackRepo;
            this._viewModel = new CartViewModel { Tracks = new List<Track>() };
        }

        /// <summary>
        /// Действие для отображения корзины
        /// </summary>
        /// <param name="currentUserId">
        /// Id текущего пользователя
        /// </param>
        [HttpGet]
        public ViewResult Index(int currentUserId)
        {
            var cart = this._cartRepository.GetAll(c => c.UserId == currentUserId).FirstOrDefault();
            if (cart != null)
            {
                this._viewModel.Tracks = (IList<Track>)cart.Tracks;
            }

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
        /// Добавление песни в корзину и перенаправление на действие отображения корзины
        /// </summary>
        /// <param name="currentUserId">
        /// Id текущего пользователя
        /// </param>
        /// <param name="track">
        /// Добавляемая песня
        /// </param>
        [HttpPost]
        public RedirectToRouteResult AddTrack(int currentUserId, int trackId = 0)
        {
            var track = this._trackRepository.GetById(trackId);
            if (track == null)
            {
                return this.RedirectToRoute(new { controller = "Cart", action = "Index", currentUserId = currentUserId });
            }

            var cart = this._cartRepository.GetAll(c => c.UserId == currentUserId).FirstOrDefault();
            if (cart == null)
            {
                var model = new Cart { UserId = currentUserId, Tracks = new List<Track> { track } };
                this._cartRepository.AddOrUpdate(model);
            }

            if (cart != null && cart.Tracks.Count(t => t.Id == trackId) == 0)
            {
                cart.Tracks.Add(track);
                this._cartRepository.AddOrUpdate(cart);
            }

            return this.RedirectToRoute(new { controller = "Cart", action = "Index", currentUserId = currentUserId });
        }

        /// <summary>
        /// Удаление песни из корзины и перенаправление на действие отображения корзины
        /// </summary>
        /// <param name="currentUserId">
        /// Id текущего пользователя
        /// </param>
        /// <param name="track">
        /// Удаляемая песня
        /// </param>
        [HttpPost]
        public RedirectToRouteResult DeleteTrack(int currentUserId, int trackId = 0)
        {
            var cart = this._cartRepository.GetAll(c => c.UserId == currentUserId).FirstOrDefault();
            if (cart == null || trackId == 0)
            {
                return this.RedirectToRoute(new { controller = "Cart", action = "Index", currentUserId = currentUserId });
            }

            var track = cart.Tracks.FirstOrDefault(t => t.Id == trackId);
            if (track != null)
            {
                cart.Tracks.Remove(track);
                this._cartRepository.AddOrUpdate(cart);
            }

            if (cart.Tracks.Count == 0)
            {
                this._cartRepository.Delete(cart);
            }

            return this.RedirectToRoute(new { controller = "Cart", action = "Index", currentUserId = currentUserId });
        }
    }
}