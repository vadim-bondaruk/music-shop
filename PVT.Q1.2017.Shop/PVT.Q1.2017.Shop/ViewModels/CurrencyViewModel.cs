namespace PVT.Q1._2017.Shop.ViewModels
{
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public class CurrencyViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<CrossCurrencyViewModel> CrossCurce { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public class CrossCurrencyViewModel
        {
            /// <summary>
            /// 
            /// </summary>
            public string FullName { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string ShortName { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public decimal CrossCourse { get; set; }
        }
    }
}