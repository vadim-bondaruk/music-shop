namespace Shop.BLL.Services.Infrastructure
{
    using Common.Models;

    /// <summary>
    /// The price level service
    /// </summary>
    public interface IPriceLevelService
    {
        /// <summary>
        /// Adds the price level with the specified <paramref name="name"/>
        /// </summary>
        /// <param name="name">
        /// The price level name.
        /// </param>
        void AddPriceLevel(string name);

        /// <summary>
        /// Returns the price level with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">
        /// The price level id.
        /// </param>
        /// <returns>
        /// The price level with the specified <paramref name="id"/> or <b>null</b> if price level doesn't exist.
        /// </returns>
        PriceLevel GetPriceLevelInfo(int id);
    }
}