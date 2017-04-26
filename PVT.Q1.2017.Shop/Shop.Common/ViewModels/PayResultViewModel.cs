namespace Shop.Common.ViewModels
{
    using System;
    using Models;
    
    /// <summary>
    /// Pay result for certain currency
    /// </summary>
    public class PayResultViewModel
    {
        /// <summary>
        /// Currency id
        /// </summary>
        public int CurrencyID { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        public Currency Currency { get; set; }

        /// <summary>
        /// Currency amount result
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Cross course to current currency
        /// </summary>
        public decimal CrossCourse { get; set; }

    }
}
