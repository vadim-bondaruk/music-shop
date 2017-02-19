
namespace Shop.Common.Models
{
    public class Vote
    {
        public int Id { get; set; }

        public Mark Mark { get; set; }

        public virtual User User { get; set; }

        public virtual Song Song { get; set; }
    }
}
