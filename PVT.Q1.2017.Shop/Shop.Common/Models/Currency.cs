// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Currency.cs" company="PVT Q1 2017">
//   All rights reserved
// </copyright>
// <summary>
//   Defines the Currency type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Shop.Infrastructure.Models;

namespace Shop.Common.Models
{
    /// <summary>
    /// The currency.
    /// </summary>
    public class Currency : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}