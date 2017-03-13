namespace Shop.Common.Models
{
    using System.Collections.Generic;
    using FluentValidation.Attributes;
    using Infrastructure.Models;
    using Validators;

    /// <summary>
    ///     The temporary user model (have to be extended by UserMenagement team).
    /// </summary>
    [Validator(typeof(UserValidator))]
    public class User : BaseEntity
    {
        /// <summary>
        /// Identity key
        /// </summary>
        public string IdentityKey { get; set; }

        /// <summary>
        /// Additional discount
        /// </summary>
        public double? Dicount { get; set; }

        /// <summary>
        /// User currency id
        /// </summary>
        public int CurrencyId { get; set; }
        
        /// <summary>
        /// ID for relation with <see cref="PriceLevel"/> 
        /// </summary>
        public int PriceLevelId { get; set; }

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
    }
}