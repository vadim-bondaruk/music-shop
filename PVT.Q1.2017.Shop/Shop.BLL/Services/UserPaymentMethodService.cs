namespace Shop.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Common.Models;
    using DAL.Infrastruture;
    using Infrastructure;

    /// <summary>
    /// 
    /// </summary>
    public class UserPaymentMethodService : BaseService, IUserPaymentMethodService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserPaymentMethodService"/> class.
        /// </summary>
        /// <param name="factory">
        /// The repository factory.
        /// </param>
        public UserPaymentMethodService(IRepositoryFactory factory) : base(factory)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ICollection<UserPaymentMethod> UserPaymentMethodsByUser(User user)
        {
            if (user != null)
            {
                using (var repository = this.Factory.GetUserPaymentMethodRepository())
                {
                    return repository.GetAll(a => a.User.Equals(user));
                }
            }

            return null;
        }
    }
}
