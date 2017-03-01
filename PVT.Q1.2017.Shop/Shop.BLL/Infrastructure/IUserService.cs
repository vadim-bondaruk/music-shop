namespace Shop.BLL.Infrastructure
{
    using Shop.BLL.DTO;
    
    /// <summary>
    /// 
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool RegisterUser(UserDTO user);
    }
}
