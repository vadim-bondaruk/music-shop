namespace Shop.Common.Models
{
    public class SongPrice
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public Currency Currency { get; set; }

        public virtual Song Song { get; set; }
    }
}