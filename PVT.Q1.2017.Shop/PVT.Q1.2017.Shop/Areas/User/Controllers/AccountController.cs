namespace PVT.Q1._2017.Shop.Areas.User.Controllers
{
    using System;
    using System.Net.Mail;
    using System.Web.Mvc;
    using global::Shop.BLL.Exceptions;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.Common.ViewModels;
    using global::Shop.DAL.Infrastruture;
    using global::Shop.Infrastructure.Security;
    using Helpers;
    using App_Start;
    using global::Shop.Infrastructure.Enums;
    using ViewModels;

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
            var isUnique = !_userService.IsUserExist(login);

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
            var isUnique = !_userService.IsUserExist(email);

            return this.Json(isUnique, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult IsUserNotExist(string userIdentity)
        {
            var isUnique = _userService.IsUserExist(userIdentity);

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
            return View();
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
                    this._authModule.LogIn(model.UserIdentity, model.Password);
                    return this.RedirectToAction("Index", "Home", new { area = string.Empty });
                }
                catch (UserValidationException ex)
                {
                    ModelState.AddModelError(ex.UserProperty, ex.Message);
                }
            }
            return View();
        }

        /// <summary>
        /// POST: User/Account/LogOut
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            this.HttpContext.Session.Abandon();
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
            // TODO: Add insert logic here
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

            ModelState.AddModelError("", "Возникла ошибка при сохранении данных");

            return this.View(user);
        }

        /// <summary>
        /// GET: User/Account/ForgotPassword
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ForgotPassword(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return this.View();
        }


        /// <summary>
        /// POST: User/Account/ForgotPassword
        /// </summary>
        /// <param name="model"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword([Bind(Include = "UserIdentity")] ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string UsetEmail = _userService.GetEmailByUserIdentity(model.UserIdentity);

                    //TODO 
                    string newPassword = "!Ivan87";

                    //int id = _userService.GetIdOflogin(User.Identity.Name);

                    //try
                    //{
                    //    if (_userService.UpdatePassword(id, newPassword, model.OldPassword))
                    //    {
                    //        return this.RedirectToAction("ChangePasswordSuccess");
                    //    }
                    //}
                    //catch (UserValidationException ex)
                    //{
                    //    ModelState.AddModelError(ex.UserProperty, ex.Message);
                    //    return View();
                    //}

                    MailAddress from = new MailAddress("flash87@tut.by", "Music Shop");
                    // кому отправляем
                    MailAddress to = new MailAddress(UsetEmail);
                    // создаем объект сообщения
                    MailMessage message = new MailMessage(from, to);
                    // тема письма
                    message.Subject = "Ваш пароль был изменен";
                    // текст письма - включаем в него ссылку
                    message.Body = string.Format("Новый пароль: " + newPassword);
                    message.IsBodyHtml = true;
                    // адрес smtp-сервера, с которого мы и будем отправлять письмо
                    SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 587);
                    // логин и пароль
                    smtp.Credentials = new System.Net.NetworkCredential("flash87@tut.by", "1258flash");
                    smtp.EnableSsl = true;
                    smtp.Send(message);
                    return RedirectToAction("ForgotPasswordSuccess");
                }
                catch (UserValidationException ex)
                {
                    ModelState.AddModelError(ex.UserProperty, ex.Message);
                    return View();
                }
                catch (Exception ex)
                {
                    throw;
                }

            }
            else
            {
                return this.View();
            }
            return this.View();
        }

        /// <summary>
        /// GET: User/Account/ForgotPasswordSuccess
        /// </summary>
        /// <returns></returns>
        public ActionResult ForgotPasswordSuccess()
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
