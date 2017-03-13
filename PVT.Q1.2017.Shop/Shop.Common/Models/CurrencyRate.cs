// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CurrencyRate.cs" company="PVT.Q1.2017">
//   PVT.Q1.2017
// </copyright>
// <summary>
//   The currency rate.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Shop.Common.Models
{
    #region

    using System;
    using Infrastructure.Models;

    #endregion

    /// <summary>
    ///     The currency rate.
    /// </summary>
    public class CurrencyRate : BaseEntity
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the cross course.
        /// </summary>
        public decimal CrossCourse { get; set; }

        /// <summary>
        /// Date of currency rate
        /// </summary>
        public DateTime Date { get; set; }

        #endregion //Properties

        #region Foreign Keys

        /// <summary>
        /// Currency id
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        /// Target currency id
        /// </summary>
        public int TargetCurrencyId { get; set; }

        #endregion //Foreign Keys

        #region Navigation Properties

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        public virtual Currency Currency { get; set; }

        /// <summary>
        ///     Gets or sets the target currency.
        /// </summary>
        public virtual Currency TargetCurrency { get; set; }

        #endregion //Navigation Properties
    }
}