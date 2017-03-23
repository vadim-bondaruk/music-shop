namespace Shop.Infrastructure.Enums
{
    /// <summary>
    /// Determines possible user roles in this shop
    /// </summary>
    public enum UserRoles
    {
        /// <summary>
        /// Ordinary user
        /// </summary>
        User,

        /// <summary>
        /// User with huge wallet
        /// </summary>
        VIPUser,

        /// <summary>
        /// Admin
        /// </summary>
        Admin
    }
}
