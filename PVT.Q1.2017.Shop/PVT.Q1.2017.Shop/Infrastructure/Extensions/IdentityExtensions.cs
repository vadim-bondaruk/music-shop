namespace PVT.Q1._2017.Shop.Infrastructure.Extensions
{
    using DataModels;

    /// <summary>
    /// Identity extensions
    /// </summary>
    public static class IdentityExtensions
    {
        /// <summary>
        /// Create new <see cref="UserDataModel"/> 
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static UserDataModel IdentityToUserDataModel(this System.Security.Principal.IIdentity identity)
        {
            var flag = identity != null;

            return new UserDataModel
            {
                IsAuthenticated = flag && identity.IsAuthenticated,
                Name = flag ? identity.Name : string.Empty
            };
        }
    }
}