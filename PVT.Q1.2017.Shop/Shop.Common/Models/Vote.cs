
using Shop.Infrastructure.Models;

namespace Shop.Common.Models
{
    public class Vote : BaseEntity
    {
        public Mark Mark { get; set; }

        public virtual User User { get; set; }

        public virtual Track Track { get; set; }
    }
}
