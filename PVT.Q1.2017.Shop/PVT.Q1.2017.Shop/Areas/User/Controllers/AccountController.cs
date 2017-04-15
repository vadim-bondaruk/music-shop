namespace PVT.Q1._2017.Shop.Areas.User.Controllers
{
    using System.Web.Mvc;
    using App_Start;
    using Helpers;
    using global::Shop.BLL.Exceptions;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.BLL.Utils.Infrastructure;
    using global::Shop.Common.ViewModels;
    using global::Shop.DAL.Infrastruture;
    using global::Shop.Infrastructure.Enums;

    /// <summary>
    /// 
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        private IUserService _userService;

        /// <summary>
        /// 
        /// </summary>
        private IAuthModule _authModule;

        /// <summary>
        /// 
        /// </summary>
        private IRepositoryFactory _factory;

        /// <summary>
        /// 
        /// </summary>
        public AccountController(IUserService userService, IAuthModule authModule, IRepositoryFactory factory)
        {
            this._userService = userService;
            this._authModule = authModule;
            this._factory = factory;
        }

        /// <summary>
        /// Method for remote validation (login)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult IsLoginUnique(string login)
        {
            var isUnique = !this._userService.IsUserExist(login);

            return this.Json(isUnique, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method for remote validation (email)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult IsEmailUnique(string email)
        {
            var isUnique = !this._userService.IsUserExist(email);

            return this.Json(isUnique, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userIdentity"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult IsUserNotExist(string userIdentity)
        {
            var isUnique = this._userService.IsUserExist(userIdentity);

            return this.Json(isUnique, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// GET: User/Account/Login
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return this.View();
        }

        /// <summary>
        /// POST: User/Account/Login
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "UserIdentity, Password, RememberMe")] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {   
                    this._authModule.LogIn(model.UserIdentity, model.Password, System.Web.HttpContext.Current);

                    var returnUrl = HttpContext.Request.QueryString["ReturnUrl"];

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return this.Redirect(returnUrl);
                    }
                }
                catch (UserValidationException ex)
                {
                    ModelState.AddModelError(ex.UserProperty, ex.Message);
                }
            }

            return this.RedirectToAction("Index", "Home", new {area = string.Empty });
        }

        /// <summary>
        /// POST: User/Account/LogOut
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            this.Session.Abandon();
            this._authModule.LogOut();

            return this.RedirectToAction("Login", "Account", new { area = "User" });
        }

        /// <summary>
        /// GET: User/Account/Register
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return this.View();
        }

        /// <summary>
        /// POST: User/Account/Register
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = @"FirstName, LastName, Login, Password, ConfirmPassword, 
                                                    Email, Sex, BirthDate, Country, PhoneNumber")] UserViewModel user)
        {
            bool result = false;
           
            if (ModelState.IsValid)
            {
                try
                {                   
                    var userDB = UserMapper.GetUserModel(user);
                    result = this._userService.RegisterUser(userDB);
                }
                catch (UserValidationException ex)
                {
                    ModelState.AddModelError(ex.UserProperty, ex.Message);
                }
               
                if (result)
                {
                    return this.RedirectToAction("Success");
                }
            }

            ModelState.AddModelError(string.Empty, "Возникла ошибка при сохранении данных");

            return this.View(user);
        }

        /// <summary>
        /// GET: User/Account/ForgotPassword
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return this.View();
        }

        /// <summary>
        /// POST: User/Account/ForgotPassword
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(FormCollection collection)
        {
                return this.View();
        }

        /// <summary>
        /// User/Account/Success
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ShopAuthorize(UserRoles.User)]
        public ActionResult Success()
        {
            return this.View();
        }
    }
}
