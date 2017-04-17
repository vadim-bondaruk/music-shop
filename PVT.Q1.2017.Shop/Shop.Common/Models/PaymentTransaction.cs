namespace Shop.Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Shop.Infrastructure.Models;

    /// <summary>
    /// Payment transaction for saving payment info
    /// </summary>
    public class PaymentTransaction : BaseEntity 
    {

        /// <summary>
        /// Total amount for current transaction
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// User ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Currency ID
        /// </summary>
        public int CurrencyId { get; set; }
        
        /// <summary>
        /// Transaction holder
        /// </summary>
        public virtual UserData User { get; set; }

        /// <summary>
        /// Currency for transaction
        /// </summary>
        public virtual Currency Currency { get; set; }

        /// <summary>
        /// Purchased tracks
        /// </summary>
        public virtual ICollection<Track> Tracks { get; set; }

        /// <summary>
        /// Purchased albums
        /// </summary>
        public virtual ICollection<Album> Albums { get; set; }
    }
}
