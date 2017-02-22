using Shop.Infrastructure.Models;

namespace Shop.Common.Models
{
    public class TrackPrice : BaseEntity
    {
        public decimal Amount { get; set; }

        public Currency Currency { get; set; }

        public virtual Track Track { get; set; }
    }
}