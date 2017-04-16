namespace Shop.Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Shop.Infrastructure.Models;

    /// <summary>
    /// Payment transaction for saving payment info
    /// </summary>
    public class PaymentTransaction : BaseEntity 
    {

        /// <summary>
        /// Total amount for current transaction
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Transaction holder
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Currency for transaction
        /// </summary>
        public Currency Currency { get; set; }

        /// <summary>
        /// The order ID for which the transaction was created
        /// </summary>
        public int OrderId { get; set; }

    }
}
