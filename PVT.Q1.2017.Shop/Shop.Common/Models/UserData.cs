﻿namespace Shop.Common.Models
{
    using System.Collections.Generic;
    using FluentValidation.Attributes;
    using Infrastructure.Models;
    using Validators;

    /// <summary>
    /// The user finance data.
    /// </summary>
    [Validator(typeof(UserDataValidator))]
    public class UserData : BaseEntity
    {
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
        /// The user id.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// User currency
        /// </summary>
        public virtual Currency UserCurrency { get; set; }

        /// <summary>
        /// Price level for user
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

        /// <summary>
        /// The user.
        /// </summary>
        public virtual User User { get; set; }
    }
}