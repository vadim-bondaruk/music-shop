namespace Shop.Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Base Address object used as billing address in a payment or extended for Shipping Address.
    /// </summary>
    public class Address : BaseAddress
    {
        /// <summary>
        /// Phone number in E.123 format. 50 characters max.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Type of address (e.g., HOME_OR_WORK, GIFT etc).
        /// </summary>
        public AddressType Type { get; set; }
    }

    public enum AddressType
    {
        Home = 1,
        Work,
        Gift,
        Other
    }
}
