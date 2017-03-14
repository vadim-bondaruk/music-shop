namespace PVT.Q1._2017.Shop.Controllers
{
    using System.Web.Mvc;
    using System.Web.Security;
    using Filters;
    using global::Shop.BLL.DTO;
    using global::Shop.BLL.Exceptions;
    using global::Shop.BLL.Services.Infrastructure;    
    using global::Shop.Infrastructure.Security;
    using ViewModels;

    /// <summary>
    /// 
    /// </summary>
    public class UserController : Controller
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
        public UserController(IUserService userService, IAuthModule authModule)
        {
            this._userService = userService;
            this._authModule = authModule;
        }

        /// <summary>
        /// GET: User
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// GET: User/Details/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            return this.View();
        }

        /// <summary>
        /// GET: /User/Login
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
        /// POST: /User/Login
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                this._authModule.LogIn(model.UserIdentity, model.Password);
            }
            catch (UserValidationException ex)
            {
                ModelState.AddModelError(ex.UserProperty, ex.Message);
            }

                return this.View();
            }

        /// <summary>
        /// POST: /User/LogOut
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            this._authModule.LogOut();
            return this.RedirectToAction("Login");
        }

        /// <summary>
        /// GET: User/Create
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return this.View();
        }

        /// <summary>
        /// POST: User/Create
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel user)
        {
                bool result = false;   
                /// TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                try
                {
                    AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<UserViewModel, UserDTO>());
                    var u = AutoMapper.Mapper.Map<UserDTO>(user);
                    result = this._userService.RegisterUser(AutoMapper.Mapper.Map<UserDTO>(user));
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
        /// GET: User/Edit/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            return this.View();
        }

        /// <summary>
        /// POST: User/Edit/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                /// TODO: Add update logic here

                return this.RedirectToAction("Index");
            }
            catch
            {
                return this.View();
            }
        }

        /// <summary>
        /// GET: User/Delete/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            return this.View();
        }

        /// <summary>
        /// POST: User/Delete/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                /// TODO: Add delete logic here

                return this.RedirectToAction("Index");
            }
            catch
            {
                return this.View();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Success()
        {
            return this.View();
        }
    }
}
