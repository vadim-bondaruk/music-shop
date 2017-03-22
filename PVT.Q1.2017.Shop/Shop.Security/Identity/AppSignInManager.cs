namespace PVT.Q1._2017.Shop.Security.Identity
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using Microsoft.Owin.Security;

    /// <summary>
    /// Application sign in manager 
    /// </summary>
    public class AppSignInManager : SignInManager<AppUser, string>
    {
        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="authenticationManager"></param>
        public AppSignInManager(AppUserManager userManager, IAuthenticationManager authenticationManager) :
            base(userManager, authenticationManager)
        {
        }

        /// <summary>
        /// Create instance of <see cref="AppSignInManager"/>
        /// </summary>
        /// <param name="options"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static AppSignInManager Create(IdentityFactoryOptions<AppSignInManager> options, IOwinContext context)
        {
            return new AppSignInManager(context.GetUserManager<AppUserManager>(), context.Authentication);
        }

        /// <summary>
        /// Generate instance of <see cref="ClaimsIdentity"/>
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public override Task<ClaimsIdentity> CreateUserIdentityAsync(AppUser user)
        {
            return user.GenerateUserIdentityAsync((AppUserManager)UserManager);
        }
    }
}