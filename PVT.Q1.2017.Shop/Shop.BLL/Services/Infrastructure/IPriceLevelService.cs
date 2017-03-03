namespace Shop.BLL.Services.Infrastructure
{
    using Common.Models;
    using Shop.Infrastructure.Services;

    /// <summary>
    /// The price level service
    /// </summary>
    public interface IPriceLevelService : IService<PriceLevel>
    {
        /// <summary>
        /// Adds the price level with the specified <paramref name="name"/>
        /// </summary>
        /// <param name="name">
        /// The price level name.
        /// </param>
        void AddPriceLevel(string name);
    }
}