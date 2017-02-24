// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PaymentMethod.cs" company="PVT Q1 2017">
//   All rights reserved
// </copyright>
// <summary>
//   Defines the PaymentMethod type.
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
    /// Defines props for definite common payment method
    /// </summary>
    public class PaymentMethod : BasePayment
    {
        /// <summary>
        /// Gets or sets Name for type of payment method
        /// </summary>
        public string Name { get; set; }
    }
}
