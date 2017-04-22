using Shop.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common.ViewModels
{
    public class PaymentTransactionViewModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Total amount for current transaction
        /// </summary>
        public decimal Amount { get; set; }

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
        public string UserName { get; set; }

        /// <summary>
        /// Currency for transaction
        /// </summary>
        public string CurrencyShortName { get; set; }

        ///// <summary>
        ///// Purchased tracks
        ///// </summary>
        //public ICollection<Track> PurchasedTrack { get; set; }

        ///// <summary>
        ///// Purchased albums
        ///// </summary>
        //public ICollection<Album> PurchasedAlbum { get; set; }



    }
}
