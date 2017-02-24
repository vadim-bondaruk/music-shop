using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common.Models
{
    class Role:BaseEntity
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
