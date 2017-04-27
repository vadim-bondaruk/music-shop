namespace PVT.Q1._2017.Shop.Areas.Admin.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using global::Shop.Common.Models;

    /// <summary>
    /// Rate view model
    /// </summary>
    public class RateViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        [Display(Name = "Currency")]

        /// <summary>
        /// List of currency to select 
        /// </summary>
        public IEnumerable<Currency> From { get; set; }

        [Required]

        /// <summary>
        /// From id
        /// </summary>
        public int FromId { get; set; }

        [Display(Name = "Target currency")]

        /// <summary>
        /// List of currency to select 
        /// </summary>
        public IEnumerable<Currency> To { get; set; }

        [Required]

        /// <summary>
        /// To id
        /// </summary>
        public int ToId { get; set; }

        [Required]

        /// <summary>
        /// Cross course
        /// </summary>
        public decimal Value { get; set; }

        [Display(Name = "Date"), Required]

        /// <summary>
        /// Date of currencies rate
        /// </summary>
        public DateTime Date { get; set; }
    }
}