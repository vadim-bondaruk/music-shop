namespace PVT.Q1._2017.Shop.Areas.User.Controllers
{
    using System;
    using System.Web.Mvc;
    using global::Shop.BLL.Exceptions;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.Common.Utils;
    using global::Shop.DAL.Infrastruture;
    using ViewModels;
    using System.Linq;
    using System.Web.Security;
    using global::Shop.Common.ViewModels;
    using global::Shop.BLL.Utils;
    using Shop.Controllers;
    using App_Start;
    using global::Shop.Infrastructure.Enums;
    using Helpers;
    using global::Shop.BLL.Utils.Infrastructure;
    using System.Web;

    /// <summary>
    /// 
    /// </summary>
    [ShopAuthorize(UserRoles.User, UserRoles.Admin)]
    public class ManageController : BaseController
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
        public ManageController(IUserService userService, IAuthModule authModule, IRepositoryFactory factory)
        {
            this._userService = userService;
            this._authModule = authModule;
            this.Factory = factory;
        }

        /// <summary>
        /// GET: User/Manage/Index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Message = "Редактировать аккаунт";
            return this.View();
        }

        /// <summary>
        /// GET: User/Manage/UpdatePersonal 
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdatePersonal()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Message = "Ваш логин: " + User.Identity.Name;
            }
            else
            {
                ViewBag.Message = "Вы не авторизованы";
                return this.View();
            }
            int id = this.CurrentUser.Id;            
            User userDB = null;
            using (var userRepository = this.Factory.GetUserRepository())
            {
                userDB = userRepository.GetById(id);                                     
            }
            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<User, UserPersonalViewModel>());
            var user = AutoMapper.Mapper.Map<UserPersonalViewModel>(userDB);
            return this.View(user);
        }

        /// <summary>
        /// POST: User/Manage/UpdatePersonal 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePersonal([Bind(Include = @"FirstName, LastName, Sex, BirthDate, Country, PhoneNumber")] UserPersonalViewModel user)
        {
            bool result = false;            
            if (ModelState.IsValid)
            {
                AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<UserPersonalViewModel, User>());
                var userDB = AutoMapper.Mapper.Map<User>(user);
                //var userDB = UserMapper.GetUserModel(user);
                result = _userService.UpdatePersonal(userDB, this.CurrentUser.Id);   
                                                                                             
                if (result)
                {
                    return this.RedirectToAction("UpdatePersonal");
                }
            }

            return this.View(user);
        }

        /// <summary>
        /// GET: User/Manage/ChangePassword 
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePassword()
        {
            return View();
        }

        /// <summary>
        /// POST: User/Manage/ChangePassword 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword([Bind(Include = @"OldPassword, Password, ConfirmPassword")] ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (_userService.UpdatePassword(this.CurrentUser.Id, model.Password, model.OldPassword))
                    {
                        TempData["message"] = "Пароль успешно изменён";
                        return this.RedirectToAction("ChangeAccepted");
                    }
                }
                catch (UserValidationException ex)
                {
                    ModelState.AddModelError(ex.UserProperty, ex.Message);
                    return View();
                }
            }
            return View();
        }

        /// <summary>
        /// GET: User/Manage/ChangeLogin  
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangeLogin()
        {
            return View();
        }

        /// <summary>
        /// POST: User/Manage/ChangeLogin  
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeLogin([Bind(Include = @"Login")] ChangeLoginViewModel user)
        {
            if (ModelState.IsValid)
            {                               
                if (_userService.UpdateLogin(User.Identity.Name, user.Login))
                {                   
                    string subject = "Ваш Логин был изменен";
                    string body = "Новый логин: " + user.Login;
                    string usetEmail = _userService.GetEmailByUserIdentity(user.Login);
                    if (!MailDispatch.SendingMail(usetEmail, subject, body))
                    {
                        ModelState.AddModelError("", "Ошибка отправки");
                        return View();
                    }
                    this.HttpContext.Session.Abandon();
                    this._authModule.LogOut();
                    return this.RedirectToAction("Login", "Account", new { area = "User" });
                }
            }
            return View();
        }

        /// <summary>
        /// <summary>
        /// GET: User/Manage/Delete 
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            return View();
        }

        /// <summary>
        /// POST: User/Manage/Delete 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? Id)
        {
            try
            {
                using (var userRepository = this.Factory.GetUserRepository())
                {
                    //userRepository.Delete(this.CurrentUser.Id);
                    //userRepository.SaveChanges();
                    _userService.SoftDelete(this.CurrentUser.Id);
                    this.HttpContext.Session.Abandon();
                    this._authModule.LogOut();
                }
            }           
            catch (Exception ex)
            {
                //TODO 
                throw; 
            }               
            return this.RedirectToAction("Index", "Home", new { area = string.Empty });
        }

        /// <summary>
        /// Unified action, displays messages about account changes
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangeAccepted()
        {
            ViewBag.Message = TempData["message"];

            return this.View();
        }
    }
}