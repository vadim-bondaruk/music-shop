// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BasePayment.cs" company="PVT Q1 2017">
//   All rights reserved
// </copyright>
// <summary>
//   Defines the BasePayment type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Shop.Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Infrastructure.Models;

    /// <summary>
    /// Base model for every user payment method
    /// </summary>
    public class BasePayment : BaseEntity
    {
        /// <summary>
        /// Description for payment method
        /// </summary>
        public string Description { get; set; }
    }
}
