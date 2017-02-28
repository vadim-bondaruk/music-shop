namespace Shop.BLL.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    
    /// <summary>
    /// 
    /// </summary>
    public class UserValidationException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="property"></param>
        public UserValidationException(string message, string property) : base(message)
        {
            this.UserProperty = property;
        }

        /// <summary>
        /// User property with exception
        /// </summary>
        public string UserProperty { get; set; }
    }
}
