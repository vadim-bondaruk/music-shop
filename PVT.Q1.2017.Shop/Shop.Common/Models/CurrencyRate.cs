using Shop.Infrastructure.Models;

namespace Shop.Common.Models
{
    public class CurrencyRate : BaseEntity
    {
        public Currency Currency { get; set; }

        public Currency TargetCurrency { get; set; }

        public double CrossCourse { get; set; }
    }
}