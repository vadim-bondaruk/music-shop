namespace PVT.Q1._2017.Shop.ViewModels
{
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public class CurrencyViewModel
    {
        #region Properties

        /// <summary>
        /// Full name of currency
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// ISO 4217 code of currency 
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets the graphic currency symbol.
        /// </summary>
        public string Symbol { get; set; }
        #endregion //Properties
    }
}