namespace Shop.Common.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Price level
    /// </summary>
    public class PriceLevel : BaseNamedEntity
    {
        #region Navigation Properties

        /// <summary>
        /// Users 
        /// </summary>
        public virtual ICollection<User> Users { get; set; }

        /// <summary>
        /// All track prices related to this price level
        /// </summary>
        public virtual ICollection<TrackPrice> TrackPriceLevels { get; set; }

        /// <summary>
        /// All album prices related to this price level
        /// </summary>
        public virtual ICollection<AlbumPrice> AlbumPriceLevels { get; set; }

        #endregion //Navigation Properties
    }
}
