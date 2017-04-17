﻿namespace PVT.Q1._2017.Shop.Areas.User.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Helpers;
    using global::Shop.BLL.Exceptions;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.BLL.Utils;
    using global::Shop.BLL.Utils.Infrastructure;
    using global::Shop.Common.ViewModels;
    using Shop.Controllers;
    using global::Shop.DAL.Infrastruture;
    using ViewModels;

    /// <summary>
    /// 
    /// </summary>
    public class AccountController : BaseController
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
        private IRepositoryFactory Factory;

        /// <summary>
        /// 
        /// </summary>
        public AccountController(IUserService userService, IAuthModule authModule, IRepositoryFactory factory)
        {
            this._userService = userService;
            this._authModule = authModule;
            this.Factory = factory;
        }

        /// <summary>
        /// Method for remote validation (login)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [OutputCache(NoStore = false, Duration = 0)]
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
        [OutputCache(NoStore = false, Duration = 0)]
        public ActionResult IsEmailUnique(string email)
        {
            var isUnique = !this._userService.IsUserExist(email);

            return this.Json(isUnique, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method for remote validation (userIdentity)
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
                    this._authModule.LogIn(model.UserIdentity, model.Password, System.Web.HttpContext.Current, model.RememberMe);

                    var returnUrl = HttpContext.Request.QueryString["ReturnUrl"];

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return this.Redirect(returnUrl);
                    }
                }
                catch (UserValidationException ex)
                {
                    ModelState.AddModelError(ex.UserProperty, ex.Message);
                    return this.View(model);
                }
            }

            return this.RedirectToAction("Index", "Home", new { area = string.Empty });
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
        public async Task<ActionResult> Register([Bind(Include = @"FirstName, LastName, Login, Password, ConfirmPassword, 
                                                    Email, Sex, BirthDate, Country, PhoneNumber")] UserViewModel user)
        {
            bool result = false;

            if (ModelState.IsValid)
            {
                try
                {
                    var userDB = UserMapper.GetUserModel(user);
                    result = this._userService.RegisterUser(userDB);
                    if (result)
                    {
                        string subject = "Подтверждение регистрации";
                        string body = string.Format(
                                                    "Для завершения регистрации перейдите по ссылке:" +
                                                    "<a href=\"{0}\" title=\"Подтвердить регистрацию\">{0}</a>",
                            Url.Action("ConfirmEmail", "Account", new { Token = userDB.Id, Email = userDB.Email }, Request.Url.Scheme));
                        if (await MailDispatch.SendingMailAsync(userDB.Email, subject, body))
                        {
                            return this.RedirectToAction("Confirm", "Account", new { Email = userDB.Email });
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Ошибка отправки сообщения");
                        }
                    }
                }
                catch (UserValidationException ex)
                {
                    ModelState.AddModelError(ex.UserProperty, ex.Message);
                }
            }

            return this.View(user);
        }

        /// <summary>
        /// GET: User/Account/Siterules
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Siterules()
        {
            return this.View();
        }

        /// <summary>
        /// GET: User/Account/Confirm
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Confirm(string Email)
        {
            if (Email != null)
                {
                ViewBag.Message = "На почтовый адрес " + Email + " Вам высланы дальнейшие " +
                    "инструкции по завершению регистрации";
                }

            return this.View();
            }

        /// <summary>
        /// GET: User/Account/ConfirmEmail
        /// </summary>
        /// <param name="token"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ConfirmEmail(string token, string email)
        {
            if (token == null)
            {
                throw new ArgumentException("Token");
            }

            if (token == null)
            {
                throw new ArgumentException("Email");
            }

            if (this._userService.UpdateConfirmEmail(token, email))
            {
                return this.RedirectToAction("Success", "Account", new { area = "User" });
            }

            return this.RedirectToAction("Confirm", "Account", new { Email = string.Empty });
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
        public async Task<ActionResult> ForgotPassword([Bind(Include = "UserIdentity")] ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string usetEmail = this._userService.GetEmailByUserIdentity(model.UserIdentity);
                    int id = this._userService.GetIdOflogin(usetEmail);
                    string newPassword = PasswordEncryptor.RendomPassword();
                    if (this._userService.UpdatePassword(id, newPassword))
                    {
                        string subject = "Ваш пароль был изменен";
                        string body = "Новый пароль: " + newPassword;
                        if (await MailDispatch.SendingMailAsync(usetEmail, subject, body))
                        {
                            return this.RedirectToAction("ForgotPasswordSuccess");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Ошибка отправки");
                        }
                    }
                }
                catch (UserValidationException ex)
                {
                    ModelState.AddModelError(ex.UserProperty, ex.Message);
                    return this.View();
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
        /// GET: User/Account/Success
        /// </summary>
        /// <returns></returns>
        public ActionResult Success()
        {
            return this.View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Error()
        {
            return this.View();
        }
    }
}
