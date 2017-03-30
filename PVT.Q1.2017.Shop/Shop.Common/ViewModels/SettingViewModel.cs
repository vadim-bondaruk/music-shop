using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common.ViewModels
{
    /// <summary>
    /// </summary>
    public class SettingViewModel
    {
        /// <summary>
        /// Gets or sets the default currency id.
        /// </summary>
        public int DefaultCurrencyId { get; set; }

        /// <summary>
        /// Gets or sets the default currency full name.
        /// </summary>
        public string DefaultCurrencyFullName { get; set; }

        /// <summary>
        /// The default priceId
        /// </summary>
        public int DefaultPriceLevelId { get; set; }

        /// <summary>
        /// Gets or sets the price level name.
        /// </summary>
        public string DefaultPriceLevelName { get; set; }

        /// <summary>
        /// for drop down lists
        /// </summary>
        public ICollection<CurrencyViewModel> DefaultCurrencyViewModelList { get; set; }

        /// <summary>
        /// for drop down lists
        /// </summary>
        public ICollection<DefaultPriceLevelViewModel> DefaultPriceLevelViewModelList { get; set; }
    }

    /// <summary>
    /// </summary>
    public class DefaultPriceLevelViewModel
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}
