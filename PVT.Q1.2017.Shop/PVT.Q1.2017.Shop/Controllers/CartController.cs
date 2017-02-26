namespace PVT.Q1._2017.Shop.Controllers.Cart
{
    using System.Web.Mvc;
    using global::Shop.Common.Models;
    using global::Shop.Infrastructure.Repositories;

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
        #endregion
        /// <summary>
        /// Конструктор для контроллера корзины
        /// </summary>
        /// <param name="repo">
        /// Репозиторий для хранения корзины
        /// </param>
        public CartController(IRepository<Cart> repo)
        {
            this._cartRepository = repo;
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
            // создание ViewModel
            return this.View();
        }

        /// <summary>
        /// Добавление песни в корзину и перенаправление на действие отображения корзины
        /// </summary>
        /// <param name="currentUserId">
        /// Id текущего пользователя
        /// </param>
        /// <param name="trackID">
        /// Id добавляемой песни
        /// </param>
        /// <param name="returnUrl">
        /// Путь для возврата к покупкам
        /// </param>
        public RedirectToRouteResult AddToCart(int currentUserId, int trackID, string returnUrl)
        {
            // добавление в репозиторий
            return this.RedirectToRoute(new { controller = "CartController", action = "Index", currentUserId = currentUserId, returnUrl = returnUrl });
        }

        /// <summary>
        /// Удаление песни из корзины и перенаправление на действие отображения корзины
        /// </summary>
        /// <param name="currentUserId">
        /// Id текущего пользователя
        /// </param>
        /// <param name="trackID">
        /// Id удаляемой песни
        /// </param>
        /// <param name="returnUrl">
        /// Путь для возврата к покупкам
        /// </param>
        public RedirectToRouteResult DeleteFromCart(int currentUserId, int trackID, string returnUrl)
        {
            // удаление из репозитория
            return this.RedirectToRoute(new { controller = "CartController", action = "Index", currentUserId = currentUserId, returnUrl = returnUrl });
        }
        #endregion 
    }
}
