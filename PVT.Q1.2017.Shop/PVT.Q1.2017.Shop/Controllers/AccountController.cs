namespace PVT.Q1._2017.Shop.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Infrastructure;
    using Security.Identity;
    using ViewModels;

    /// <summary>
    /// Account controller
    /// </summary>
    public class AccountController : BaseController
    {
        /// <summary>
        /// Security service
        /// </summary>
        private IWebSecurity _securityService;

        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="securityService"></param>
        public AccountController(IWebSecurity securityService)
        {
            this._securityService = securityService;
        }

        /// <summary>
        /// Login method
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return this.View();
        }

        /// <summary>
        /// Login method
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (await this._securityService.Login(login.UserName, login.Password, login.RememberMe))
                    {
                        if (!string.IsNullOrEmpty(login.ReturnUrl))
                        {
                            var localUrl = Server.UrlDecode(login.ReturnUrl);
                            if (Url.IsLocalUrl(localUrl))
                            {
                                return this.Redirect(localUrl);
                            }
                        }
                        else
                        {
                            return this.RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Wrong password or login");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Try a little bit later");
                }
            }

            return this.View("Login", login);
        }

        /// <summary>
        /// Logout method
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session.Clear();
            this._securityService.LogOff();
            return this.RedirectToAction("Login", "Account");
        }
    }
}