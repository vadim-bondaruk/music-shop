namespace PVT.Q1._2017.Shop.Areas.User.Controllers
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
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public class ManageController : Controller
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
        public ManageController(IUserService userService, IAuthModule authModule, IUserRepository _userRepository)
        {
            this._userService = userService;
            this._authModule = authModule;
            this._userRepository = _userRepository;
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
            int Id = _userService.GetIdOflogin(User.Identity.Name);
            var userDB = _userRepository.GetById(Id);
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
                int Id = _userService.GetIdOflogin(User.Identity.Name);
                AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<UserPersonalViewModel, User>());
                var userDB = AutoMapper.Mapper.Map<User>(user);
                result = _userService.UpdatePersonal(userDB, Id);   
                                                                                             
                if (result)
                {
                    return this.RedirectToAction("UpdatePersonal");
                }
            }

            return this.View(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePassword()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword([Bind(Include = @"OldPassword, Password, ConfirmPassword")] ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                int id = _userService.GetIdOflogin(User.Identity.Name);
                try
                {
                    if (_userService.UpdatePassword(id, model.Password, model.OldPassword))
                    {
                        return this.RedirectToAction("ChangePasswordSuccess");
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
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangeLogin()
        {
            return View();
        }
    }
}