using Ship.Infrastructure.Models;

namespace Shop.Common.Models
{
    public class SongPrice : BaseEntity
    {
        public decimal Amount { get; set; }

        public Currency Currency { get; set; }

        public virtual Song Song { get; set; }
    }
}