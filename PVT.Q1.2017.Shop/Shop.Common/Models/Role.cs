namespace Shop.Common.Models
{
    using System.Collections.Generic;
    using Ship.Infrastructure.Models;    

    /// <summary>
    /// 
    /// </summary>
    public class Role : BaseEntity
    {
        /// <summary>
        /// Name of the role
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Collection of users in this role
        /// </summary>
        public ICollection<User> Users { get; set; }
    }
}
