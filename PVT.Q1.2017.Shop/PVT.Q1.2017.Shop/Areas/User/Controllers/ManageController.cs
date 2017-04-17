namespace PVT.Q1._2017.Shop.Areas.User.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using App_Start;
    using Helpers;
    using global::Shop.BLL.Exceptions;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.BLL.Utils;
    using global::Shop.BLL.Utils.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.Common.ViewModels;
    using Shop.Controllers;
    using global::Shop.DAL.Infrastruture;
    using global::Shop.Infrastructure.Enums;
    using ViewModels;

    /// <summary>
    /// 
    /// </summary>
    [ShopAuthorize(UserRoles.Customer, UserRoles.Admin)]
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
                result = this._userService.UpdatePersonal(userDB, this.CurrentUser.Id);

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
            return this.View();
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
                    if (this._userService.UpdatePassword(this.CurrentUser.Id, model.Password, model.OldPassword))
                    {
                        this.TempData["message"] = "Пароль успешно изменён";
                        return this.RedirectToAction("ChangeAccepted");
                    }
                }
                catch (UserValidationException ex)
                {
                    ModelState.AddModelError(ex.UserProperty, ex.Message);
                    return this.View();
                }
            }

            return this.View();
        }

        /// <summary>
        /// GET: User/Manage/ChangeLogin  
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangeLogin()
        {
            return this.View();
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
                if (this._userService.UpdateLogin(User.Identity.Name, user.Login))
                {
                    string subject = "Ваш Логин был изменен";
                    string body = "Новый логин: " + user.Login;
                    string usetEmail = this._userService.GetEmailByUserIdentity(user.Login);
                    if (!MailDispatch.SendingMail(usetEmail, subject, body))
                    {
                        ModelState.AddModelError(string.Empty, "Ошибка отправки");
                        return this.View();
                    }

                    this.HttpContext.Session.Abandon();
                    this._authModule.LogOut();
                    return this.RedirectToAction("Login", "Account", new { area = "User" });
                }
            }

            return this.View();
        }

        /// <summary>
        /// <summary>
        /// GET: User/Manage/Delete 
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            return this.View();
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
                    this._userService.SoftDelete(this.CurrentUser.Id);
                    this.HttpContext.Session.Abandon();
                    this._authModule.LogOut();
                }
            }
            catch (Exception)
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
            ViewBag.Message = this.TempData["message"];

            return this.View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UsersEdit(int id = 1)
        {
            int countPerPage = 10;
            this.TempData["CurrentPage"] = id;
            ViewBag.PageInfo = new PageInfo { PageNumber = id, PageSize = countPerPage, TotalItems = this._userService.GetUsersCount() };
            var list = this._userService.GetDataPerPage(id, countPerPage);
            var result = list.Select(u => UserMapper.GetUserEditView(u));
            return this.View(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public ActionResult GetMatchingData(string term)
        {
            var list = this._userService.GetLastNameMatchingData(term);
            return this.Json(list);
        }

        /// <summary>
        /// Edit user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = this.Factory.GetUserRepository().GetById(id);

            if (user != null)
            {
                var editUser = UserMapper.GetUserEditView(user);
                return this.View(editUser);
            }

            return this.HttpNotFound();
        }

        /// <summary>
        /// Edit user in DB
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UserEditView model)
        {
            if (model != null)
            {
                var userDB = UserMapper.GetUserByUserEditView(model);

                if (userDB != null)
                {
                    userDB.Password = this.Factory.GetUserRepository().GetById(userDB.Id).Password;
                    using (var repository = this.Factory.GetUserRepository())
                    {
                        try
                        {
                            repository.AddOrUpdate(userDB);
                            repository.SaveChanges();
                        }
                        catch (Exception)
                        {
                            //TODO: Write to log
                            throw;
                        }
                    }
                }
            }

            return this.RedirectToAction("UsersEdit", new { controller = "Manage", area = "User", id = TempData["CurrentPage"] });
        }
    }
}