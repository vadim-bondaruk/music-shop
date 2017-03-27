namespace PVT.Q1._2017.Shop.Areas.User.Helpers
{
    using System.Web;
    using AutoMapper;
    using global::Shop.Common.Models;
    using global::Shop.Common.ViewModels;

    /// <summary>
    /// The user mapper
    /// </summary>
    internal static class UserMapper
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly IMapper ManagementModelsMapper;

        /// <summary>
        /// Initializes static members of the <see cref="UserMapper"/> class.
        /// </summary>
        static UserMapper()
        {
            ManagementModelsMapper = CreateMapper();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userView"></param>
        /// <returns></returns>
        public static User GetUserModel(UserViewModel userView)
        {
            return ManagementModelsMapper.Map<User>(userView);
        }


        /// <summary>
        /// Configures and returns a new instance of the mapper for models which have a list of other models.
        /// </summary>
        /// <returns>
        /// A new instance of the mapper for models which have a list of other models.
        /// </returns>
        private static IMapper CreateMapper()
        {
            MapperConfiguration managementConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<UserViewModel, User>());
  
            return managementConfiguration.CreateMapper();
        }
    }
}