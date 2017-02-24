// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserPaymentMethod.cs" company="PVT Q1 2017">
//   All rights reserved
// </copyright>
// <summary>
//   Defines the UserPaymentMethod type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Shop.Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Define settings for user payment methods
    /// </summary>
    public class UserPaymentMethod : BasePayment
    {
        /// <summary>
        /// Gets or sets User for payment method settings
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets payment method
        /// </summary>
        public PaymentMethod PaymentMethod { get; set; }

        /// <summary>
        /// Gets or sets alias for this user payment method settings
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Gets or sets default currency 
        /// </summary>
        public Currency Currency { get; set; }
    }
}
