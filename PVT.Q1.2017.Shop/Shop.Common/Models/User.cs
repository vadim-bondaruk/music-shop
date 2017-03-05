// // --------------------------------------------------------------------------------------------------------------------
// // <copyright file="User.cs" company="PVT Q1 2017">
// //   All rights reserved
// // </copyright>
// // <summary>
// //   Defines the User.cs type.
// // </summary>
// // --------------------------------------------------------------------------------------------------------------------

namespace Shop.Common.Models
{
    using System.Collections.Generic;
    using Infrastructure.Models;

    /// <summary>
    ///     The temporary user model (have to be extended by UserMenagement team).
    /// </summary>
    public class User : BaseEntity
    {
        #region Properties

        /// <summary>
        /// Identity key
        /// </summary>
        public string IdentityKey { get; set; }

        /// <summary>
        /// Additional discount
        /// </summary>
        public double? Dicount { get; set; }

        #endregion //Navigation Properties

        #region Foreign Keys

        /// <summary>
        /// User currency id
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        /// ID for relation with <see cref="PriceLevel"/> 
        /// </summary>
        public int PriceLevelId { get; set; }

        #endregion //Foreign Keys

        #region Navigation Properties

        /// <summary>
        /// User currency
        /// </summary>
        public virtual Currency UserCurrency { get; set; }

        /// <summary>
        /// Get or set price level for user
        /// </summary>
        public virtual PriceLevel PriceLevel { get; set; }

        /// <summary>
        /// All feedbacks
        /// </summary>
        public virtual ICollection<Feedback> Feedbacks { get; set; }

        /// <summary>
        /// All votes
        /// </summary>
        public virtual ICollection<Vote> Votes { get; set; }

        #endregion //Navigation Properties
    }
}