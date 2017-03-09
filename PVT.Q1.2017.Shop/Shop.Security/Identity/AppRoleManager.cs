namespace PVT.Q1._2017.Shop.Security.Identity
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;

    /// <summary>
    /// Application role manager
    /// </summary>
    public class AppRoleManager : RoleManager<IdentityRole>
    {
        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="roleStore"></param>
        public AppRoleManager(IRoleStore<IdentityRole, string> roleStore)
            : base(roleStore)
        {
        }

        /// <summary>
        /// Create instance of <see cref="AppRoleManager"/>
        /// </summary>
        /// <param name="options"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static AppRoleManager Create(IdentityFactoryOptions<AppRoleManager> options, IOwinContext context)
        {
            return new AppRoleManager(new RoleStore<IdentityRole>(context.Get<SecurityContext>()));
        }
    }
}