﻿namespace PVT.Q1._2017.Shop.Areas.User.Controllers
{
    using System;
    using System.Web.Mvc;
    using global::Shop.BLL.Exceptions;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.Common.Utils;
    using global::Shop.DAL.Infrastruture;
    using global::Shop.Infrastructure.Security;
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
        private IUserRepository _userRepository;

        /// <summary>
        /// 
        /// </summary>
        public AccountController(IUserService userService, IAuthModule authModule, IUserRepository _userRepository)
        {
            this._userService = userService;
            this._authModule = authModule;
            this._userRepository = _userRepository;
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
            var a = !(this._userRepository
                .GetAll(u => u.Login.Equals(login, StringComparison.OrdinalIgnoreCase))
                .IsAny());
            return this.Json(a, JsonRequestBehavior.AllowGet);
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
            var a = !(this._userRepository
                .GetAll(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
                .IsAny());
            return this.Json(a, JsonRequestBehavior.AllowGet);
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
            try
            {
                this._authModule.LogIn(model.UserIdentity, model.Password);
            }
            catch (UserValidationException ex)
            {
                ModelState.AddModelError(ex.UserProperty, ex.Message);
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
            /// TODO: Add insert logic here
            if (ModelState.IsValid)
            {
                try
                {
                    AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<UserViewModel, User>());
                    var userDB = AutoMapper.Mapper.Map<User>(user);
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
        public ActionResult Success()
        {
            return this.View();
        }
    }
}