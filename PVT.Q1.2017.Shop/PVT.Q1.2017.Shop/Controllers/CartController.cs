namespace PVT.Q1._2017.Shop.Controllers.Cart
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using global::Shop.Common.Models;
    using global::Shop.Infrastructure.Repositories;
    using ViewModels;

    /// <summary>
    /// Контоллер для корзины покупателя
    /// </summary>
    public class CartController : Controller
    {
        #region Fields
        /// <summary>
        /// Репозиторий для хранения корзины
        /// </summary>
        private IRepository<Cart> _cartRepository;

        /// <summary>
        /// Репозиторий для хранения пользователей 
        /// </summary>
        private IRepository<User> _userRepository;
        #endregion
        /// <summary>
        /// Конструктор для контроллера корзины
        /// </summary>
        /// <param name="cartRepo">
        /// Репозиторий для хранения корзины
        /// </param>
        /// <param name="userRepo">
        /// Репозиторий для хранения пользователей
        /// </param>
        public CartController(IRepository<Cart> cartRepo, IRepository<User> userRepo)
        {
            this._cartRepository = cartRepo;
            this._userRepository = userRepo;
        }

        #region Actions
        /// <summary>
        /// Действие для отображения корзины
        /// </summary>
        /// <param name="currentUserId">
        /// Id текущего пользователя
        /// </param>
        [HttpGet]
        public ViewResult Index(int currentUserId)
        {
            var cart = this._cartRepository.GetAll(c => c.User.Id == currentUserId).FirstOrDefault();
            var currentUser = this._userRepository.GetById(currentUserId);
            var cartView = new CartView { Tracks = new List<Track>() };
            if (cart != null)
            {
                foreach (var t in cart.Tracks)
                {
                    cartView.Tracks.Add(t);
                }
    
                /// <summary>
                /// Временные данные: пользователь выбрал отображение в долларах
                /// </summary>
                var userCurrency = new Currency();
                userCurrency.Code = 840;
                userCurrency.Name = "USD";
                cartView.CurrencyShortName = userCurrency.Name;
                cartView.SetTotalPrice(userCurrency);
            }

            return this.View(cartView);
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
        public RedirectToRouteResult AddTrackToCart(int currentUserId, Track track)
        {
            if (track == null)
            {
                return this.RedirectToRoute(new { controller = "Cart", action = "Index", currentUserId = currentUserId });
            }

            var cart = this._cartRepository.GetAll(c => c.User.Id == currentUserId).FirstOrDefault();
            var currentUser = this._userRepository.GetById(currentUserId);
            if (cart == null && currentUser != null)
            {
                var model = new Cart { User = currentUser, Tracks = new List<Track> { track } };
                this._cartRepository.AddOrUpdate(model);
            }

            if (cart != null)
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
        public RedirectToRouteResult DeleteTrackFromCart(int currentUserId, Track track)
        {
            var cart = this._cartRepository.GetAll(c => c.User.Id == currentUserId).FirstOrDefault(t => t.Tracks.Contains(track));
            if (cart == null || track == null)
            {
                return this.RedirectToRoute(new { controller = "Cart", action = "Index", currentUserId = currentUserId });
            }

            if (cart.Tracks.Count == 1)
            {
                this._cartRepository.Delete(cart);
            }
            else
            {
                cart.Tracks.Remove(track);
                this._cartRepository.AddOrUpdate(cart);
            }

            return this.RedirectToRoute(new { controller = "Cart", action = "Index", currentUserId = currentUserId });
        }
        #endregion 
    }
}