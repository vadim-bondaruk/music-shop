namespace PVT.Q1._2017.Shop.Security.Identity
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    /// <summary>
    /// Model of user
    /// </summary>
    public class AppUser : IdentityUser
    {
        /// <summary>
        /// Confirmation Token
        /// </summary>
        public string ConfirmationToken { get; set; }

        /// <summary>
        /// Is confirmed
        /// </summary>
        public bool IsConfirmed { get; set; }

        /// <summary>
        /// Password reset token
        /// </summary>
        public string PasswordResetToken { get; set; }

        /// <summary>
        /// Geneate instance of <see cref="ClaimsIdentity"/>
        /// </summary>
        /// <param name="manager"></param>
        /// <returns></returns>
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
    }
}