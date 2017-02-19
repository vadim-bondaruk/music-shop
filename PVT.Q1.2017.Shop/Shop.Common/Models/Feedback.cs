
namespace Shop.Common.Models
{
    public class Feedback
    {
        public int Id { get; set; }

        public string Comments { get; set; }

        public virtual User User { get; set; }

        public virtual Song Song { get; set; }
    }
}
