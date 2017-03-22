namespace PVT.Q1._2017.Shop.Security.Identity
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web;
    using Infrastructure;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;

    /// <summary>
    /// Web security contract
    /// </summary>
    public interface IWebSecurity : IDisposable
    {
        /// <summary>
        /// Log off
        /// </summary>
        void LogOff();

        /// <summary>
        /// Get callback url
        /// </summary>
        /// <param name="email"></param>
        /// <param name="actionName"></param>
        /// <param name="conrollerName"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<string> GetCallbackUrlAsync(
            string email,
            string actionName,
            string conrollerName,
            string token);

        /// <summary>
        /// Generate password reset token
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<string> GeneratePasswordResetTokenAsync(string email);

        /// <summary>
        /// Email is confirmed
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<bool> IsEmailConfirmedAsync(string email);

        /// <summary>
        /// Confirm email
        /// </summary>
        /// <param name="id"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<OperationResult> ConfirmEmailAsync(string id, string code);

        /// <summary>
        /// Create role
        /// </summary>
        /// <param name="nameRole"></param>
        /// <returns></returns>
        Task<bool> CreateRole(string nameRole);

        /// <summary>
        /// Is confirmed
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool?> IsConfirmedAsync(string userName, string password);

        /// <summary>
        /// Get user id
        /// </summary>
        /// <returns></returns>
        Guid GetAuthUserId();

        /// <summary>
        /// Create user and account
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="requireConfirmationToken"></param>
        /// <returns></returns>
        Task<OperationResult> CreateUserAndAccountAsync(
            string userName,
            string password,
            string email,
            bool requireConfirmationToken = false);

        /// <summary>
        /// Does role exists
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<bool> RoleExists(string name);

        /// <summary>
        /// Add user role
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        Task<bool> AddUserToRole(string userId, string roleName);

        /// <summary>
        /// Remoe user from Database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> RemoveUserAsync(string id);

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="persistCookie"></param>
        /// <returns></returns>
        Task<bool> Login(string userName, string password, bool persistCookie = false);

        /// <summary>
        /// Login asynchronously
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="persistCookie"></param>
        /// <returns></returns>
        Task<LoginAttemptStatus> LoginAsync(string userName, string password, bool persistCookie = false);

        /// <summary>
        /// Return user id by name
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<string> GetIdByNameAsync(string username);

        /// <summary>
        /// Return user id by email
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<string> GetIdByEmailAsync(string username);

        /// <summary>
        /// Check if email confirmed
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        bool IsConfirmed(string username);

        /// <summary>
        /// Check if user name already exists
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<bool> IsUserNameExistAsync(string userName);

        /// <summary>
        /// Check if user name already exists asynchronously
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<bool> IsEmailExistAsync(string email);

        /// <summary>
        /// Return email confirmation token asynchronously
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<string> GenerateEmailConfirmationTokenAsync(string id);

        /// <summary>
        /// Send email to user asynchronously
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        Task<bool> SendEmailAsync(string email, string subject, string body);

        /// <summary>
        /// Reset password
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<OperationResult> ResetPassword(string id, string token, string password);
    }

    /// <summary>
    /// Web security
    /// </summary>
    public class WebSecurity : IWebSecurity
    {
        /// <summary>
        /// user manager
        /// </summary>
        private AppUserManager _userManager;

        /// <summary>
        /// role manager
        /// </summary>
        private AppRoleManager _roleManager;

        /// <summary>
        /// sign in manager
        /// </summary>
        private AppSignInManager _signInManager;

        /// <summary>
        /// SignIn manager
        /// </summary>
        private AppSignInManager SignInManager
        {
            get
            {
                return this._signInManager ?? HttpContext.Current.GetOwinContext().Get<AppSignInManager>();
            }
            set
            {
                this._signInManager = value;
            }
        }

        /// <summary>
        /// Get or set user manager
        /// </summary>
        private AppUserManager UserManager
        {
            get
            {
                return this._userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>();
            }
            set
            {
                this._userManager = value;
            }
        }

        /// <summary>
        /// Get or set role manager
        /// </summary>
        private AppRoleManager RoleManager
        {
            get
            {
                return this._roleManager ?? HttpContext.Current.GetOwinContext().Get<AppRoleManager>();

            }
            set
            {
                this._roleManager = value;
            }
        }

        /// <summary>
        /// Authentication manager
        /// </summary>
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }

        /// <summary>
        /// C'tor
        /// </summary>
        public WebSecurity()
        {
        }

        /// <summary>
        /// Is user confirmed
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool?> IsConfirmedAsync(string userName, string password)
        {
            var user = await UserManager.FindAsync(userName, password);
            return user != null ? (bool?)user.IsConfirmed : null;
        }

        /// <summary>
        /// Create user and account
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="requireConfirmationToken"></param>
        /// <returns></returns>
        public async Task<OperationResult> CreateUserAndAccountAsync(
            string userName,
            string password,
            string email,
            bool requireConfirmationToken = false)
        {
            var user = new AppUser
            {
                UserName = userName,
                Email = email,
                IsConfirmed = !requireConfirmationToken,
            };

            var result = await UserManager.CreateAsync(user, password);
            return GetResult(result);

        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="persistCookie"></param>
        /// <returns></returns>
        public async Task<bool> Login(string userName, string password, bool persistCookie = false)
        {
            var user = await UserManager.FindAsync(userName, password);
            if (user != null && user.IsConfirmed)
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = persistCookie }, identity);
            }
            else return false;

            return true;
        }
        
        /// <summary>
        /// <see cref="IDisposable" implementation/>
        /// </summary>     
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Return user id by name
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<string> GetIdByNameAsync(string username)
        {
            var user = await UserManager.FindByNameAsync(username);
            return user.Id;
        }

        /// <summary>
        /// Does role exists
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<bool> RoleExists(string name)
        {
            return await RoleManager.RoleExistsAsync(name);
        }

        /// <summary>
        /// Create role
        /// </summary>
        /// <param name="nameRole"></param>
        /// <returns></returns>
        public async Task<bool> CreateRole(string nameRole)
        {
            var idResult = await RoleManager.CreateAsync(new IdentityRole(nameRole));
            return idResult.Succeeded;
        }

        /// <summary>
        /// Add user role
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task<bool> AddUserToRole(string userId, string roleName)
        {
            var idResult = await UserManager.AddToRoleAsync(userId, roleName);
            return idResult.Succeeded;
        }

        /// <summary>
        /// Does user confirmed
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool IsConfirmed(string username)
        {
            var isConfirmed = false;
            using (var context = HttpContext.Current.GetOwinContext().Get<SecurityContext>())
            {
                var user = context.Users
                   .SingleOrDefault(appUser => appUser.UserName == username);
                if (user != null)
                    isConfirmed = user.IsConfirmed;
            }

            return isConfirmed;
        }

        /// <summary>
        /// Check if user name exist
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<bool> IsUserNameExistAsync(string userName)
        {
            return await UserManager.FindByNameAsync(userName) != null;
        }

        /// <summary>
        /// Get user id
        /// </summary>
        /// <returns></returns>
        public Guid GetAuthUserId()
        {
            return Guid.Parse(AuthenticationManager.User.Identity.GetUserId());
        }

        /// <summary>
        /// Check if user name already exists asynchronously
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> IsEmailExistAsync(string email)
        {
            return await Task.Run(() => UserManager.Users.Any(user => user.Email == email));
        }

        /// <summary>
        /// Login asynchronously
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="persistCookie"></param>
        /// <returns></returns>
        public async Task<LoginAttemptStatus> LoginAsync(string userName, string password, bool persistCookie = false)
        {
            var status = await SignInManager.PasswordSignInAsync(userName, password, persistCookie, shouldLockout: false);
            switch (status)
            {
                case SignInStatus.Success:
                    return LoginAttemptStatus.Success;
                case SignInStatus.RequiresVerification:
                    return LoginAttemptStatus.RequiresVerification;
                case SignInStatus.LockedOut:
                    return LoginAttemptStatus.LockedOut;
                case SignInStatus.Failure:
                    return LoginAttemptStatus.Failure;
                default:
                    throw new InvalidEnumArgumentException(status.ToString());
            }
        }

        /// <summary>
        /// Return email confirmation token asynchronously
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> GenerateEmailConfirmationTokenAsync(string id)
        {
            return await UserManager.GenerateEmailConfirmationTokenAsync(id);
        }

        /// <summary>
        /// Send email to user asynchronously
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public async Task<bool> SendEmailAsync(string id, string subject, string body)
        {
            bool result;
            try
            {
                await UserManager.SendEmailAsync(id, subject, body);
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Confirm email
        /// </summary>
        /// <param name="id"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<OperationResult> ConfirmEmailAsync(string id, string code)
        {
            var result = await UserManager.ConfirmEmailAsync(id, code);
            return GetResult(result);
        }

        /// <summary>
        /// Remoe user from Database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> RemoveUserAsync(string id)
        {
            var user = await this.UserManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await this.UserManager.DeleteAsync(user);
                return result.Succeeded;
            }
            return false;
        }

        /// <summary>
        /// Check if email confirmed asynchronously
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> IsEmailConfirmedAsync(string email)
        {
            var user = await UserManager.FindByEmailAsync(email);
            return user != null && await UserManager.IsEmailConfirmedAsync(user.Id);
        }

        /// <summary>
        /// Generate password reset token asynchronously
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<string> GeneratePasswordResetTokenAsync(string email)
        {
            var user = await UserManager.FindByEmailAsync(email);
            if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
            {
                return string.Empty;
            }
            return await UserManager.GeneratePasswordResetTokenAsync(user.Id);
        }

        /// <summary>
        /// Get callback url asynchronously
        /// </summary>
        /// <param name="email"></param>
        /// <param name="actionName"></param>
        /// <param name="conrollerName"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string> GetCallbackUrlAsync(
            string email,
            string actionName,
            string conrollerName,
            string token)
        {
            var user = await UserManager.FindByEmailAsync(email);

            return new System.Web.Mvc.UrlHelper(HttpContext.Current.Request.RequestContext)
                .Action(actionName, conrollerName, new { userId = user.Id, code = token });
        }

        /// <summary>
        /// Get user id by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<string> GetIdByEmailAsync(string email)
        {
            var user = await UserManager.FindByEmailAsync(email);
            return user != null
                ? user.Id
                : string.Empty;

        }

        /// <summary>
        /// Reset password
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<OperationResult> ResetPassword(string id, string token, string password)
        {
            try
            {
                var result = await this.UserManager.ResetPasswordAsync(id, token, password);
                return this.GetResult(result);
            }
            catch (Exception)
            {
                return new OperationResult(new[] { "При изменение пароля произошла ошибка" });
            }
        }

        /// <summary>
        /// Log off
        /// </summary>
        public void LogOff()
        {
            AuthenticationManager.SignOut();
        }

        /// <summary>
        /// <see cref="IDisposable" implementation/>
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (UserManager != null)
                {
                    UserManager.Dispose();
                    UserManager = null;
                }
                if (RoleManager != null)
                {
                    RoleManager.Dispose();
                    RoleManager = null;
                }
                if (SignInManager != null)
                {
                    SignInManager.Dispose();
                    RoleManager = null;
                }
            }
        }

        /// <summary>
        /// Get result helper
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private OperationResult GetResult(IdentityResult result)
        {
            return result.Succeeded
                ? OperationResult.Success
                : new OperationResult(result.Errors);
        }
    }
}