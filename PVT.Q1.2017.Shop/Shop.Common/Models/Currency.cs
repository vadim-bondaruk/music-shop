// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Currency.cs" company="PVT Q1 2017">
//   All rights reserved
// </copyright>
// <summary>
//   Defines the Currency type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Shop.Common.Models
{
    using Infrastructure.Models;

    /// <summary>
    /// The currency model.
    /// </summary>
    public class Currency : BaseEntity
    {
        /// <summary>
        /// Short name of currency
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// Full name of currency
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// ISO 4217 code of currency 
        /// </summary>
        public int Code { get; set; }
    }
}