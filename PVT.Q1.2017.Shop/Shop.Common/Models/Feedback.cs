
using Shop.Infrastructure.Models;

namespace Shop.Common.Models
{
    public class Feedback : BaseEntity
    {
        public string Comments { get; set; }

        public virtual User User { get; set; }

        public virtual Track Track { get; set; }
    }
}
