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
        /// <param name="repo">
        /// Репозиторий для хранения корзины
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
        /// <param name="returnUrl">
        /// Путь для возврата к покупкам
        /// </param>
        [HttpGet]
        public ActionResult Index(int currentUserId, string returnUrl)
        {
            var cart = this._cartRepository.GetAll(c => c.User.Id == currentUserId);
            var currentUser = this._userRepository.GetById(currentUserId);
            var cardView = new CartView { Tracks = new List<Track>() };
            if (cart != null && currentUser != null)
            {
                foreach (var c in cart)
                {
                    foreach (var t in c.Tracks)
                    {
                        cardView.Tracks.Add(t);
                    }
                }

                ////cardView.SetTotalPrice(Currency.Usd);
            }

            return this.View(cardView);
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
        /// <param name="returnUrl">
        /// Путь для возврата к покупкам
        /// </param>
        [HttpPost]
        public RedirectToRouteResult AddTrackToCart(int currentUserId, Track track, string returnUrl)
        {
            if (track == null)
            {
                return this.RedirectToRoute(new { controller = "CartController", action = "Index", currentUserId = currentUserId, returnUrl = returnUrl });
            }

            var cart = this._cartRepository.GetAll(c => c.User.Id == currentUserId);
            var currentUser = this._userRepository.GetById(currentUserId);
            if (cart == null && currentUser != null)
            {
                var model = new Cart { User = currentUser, Tracks = new List<Track> { track } };
                this._cartRepository.AddOrUpdate(model);
            }

            if (cart != null)
            {
                foreach (var model in cart)
                {
                    model.Tracks.Add(track);
                    this._cartRepository.AddOrUpdate(model);
                }
            }

            return this.RedirectToRoute(new { controller = "CartController", action = "Index", currentUserId = currentUserId, returnUrl = returnUrl });
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
        /// <param name="returnUrl">
        /// Путь для возврата к покупкам
        /// </param>
        [HttpPost]
        public RedirectToRouteResult DeleteTrackFromCart(int currentUserId, Track track, string returnUrl)
        {
            var cart = this._cartRepository.GetAll(c => c.User.Id == currentUserId).Where(t => t.Tracks.Contains(track));
            if (cart == null || track == null)
            {
                return this.RedirectToRoute(new { controller = "CartController", action = "Index", currentUserId = currentUserId, returnUrl = returnUrl });
            }

            foreach (var c in cart)
            {
                if (c.Tracks.Count == 1)
                {
                    this._cartRepository.Delete(c);
                }
                else
                {
                    c.Tracks.Remove(track);
                    this._cartRepository.AddOrUpdate(c);
                }
            }

            return this.RedirectToRoute(new { controller = "CartController", action = "Index", currentUserId = currentUserId, returnUrl = returnUrl });
        }
        #endregion 
    }
}