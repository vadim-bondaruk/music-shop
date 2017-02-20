using System.Collections.Generic;

using Ship.Infrastructure.Models;

namespace Shop.Common.Models
{
    public class AlbumPrice : BaseEntity
    {
        public decimal Amount { get; set; }

        public Currency Currency { get; set; }

        public virtual Album Album { get; set; }
    }
}