namespace Shop.Common.Models
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class PageInfo
    {
        /// <summary>
        /// Current page number
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Total Items
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// Count items per page
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Total pages
        /// </summary>
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)this.TotalItems / this.PageSize); }
        }
    }
}
