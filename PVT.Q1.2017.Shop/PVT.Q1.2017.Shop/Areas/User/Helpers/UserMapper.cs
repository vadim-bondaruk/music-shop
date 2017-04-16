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
        private static readonly IMapper UserModelsMapper;

        /// <summary>
        /// Initializes static members of the <see cref="UserMapper"/> class.
        /// </summary>
        static UserMapper()
        {
            UserModelsMapper = CreateMapper();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userView"></param>
        /// <returns></returns>
        public static User GetUserModel(UserViewModel userView)
        {
            return UserModelsMapper.Map<User>(userView);
        }

        public static UserEditView GetUserEditView(User user)
        {
            return UserModelsMapper.Map<UserEditView>(user);
        }

        public static User GetUserByUserEditView(UserEditView user)
        {
            return UserModelsMapper.Map<User>(user);
        }
        /// <summary>
        /// Configures and returns a new instance of the mapper for models which have a list of other models.
        /// </summary>
        /// <returns>
        /// A new instance of the mapper for models which have a list of other models.
        /// </returns>
        private static IMapper CreateMapper()
        {
            MapperConfiguration managementConfiguration = new MapperConfiguration(cfg => { cfg.CreateMap<UserViewModel, User>(); cfg.CreateMap<User, UserEditView>(); cfg.CreateMap<UserEditView, User>(); });
  
            return managementConfiguration.CreateMapper();
        }
    }
}