namespace Shop.Common.Helpers
{
    using Shop.Infrastructure.Enums;

    /// <summary>
    /// 
    /// </summary>
    public static class RolesHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRole"></param>
        /// <returns></returns>
        public static string GetRoleName(UserRoles userRole)
        {
            return userRole.ToString();
        }
    }
}
