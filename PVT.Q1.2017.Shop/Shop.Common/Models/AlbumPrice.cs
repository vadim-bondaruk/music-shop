using System.Collections.Generic;

namespace Shop.Common.Models
{
    public class AlbumPrice
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public Currency Currency { get; set; }

        public virtual Album Album { get; set; }
    }
}