namespace Shop.Common.Models
{
    public class CurrencyRate
    {
        public int Id { get; set; }

        public Currency Currency { get; set; }

        public Currency TargetCurrency { get; set; }

        public double CrossCourse { get; set; }
    }
}