namespace Shop.BLL.Services
{
    using Common.Models;
    using DAL.Repositories.Infrastruture;
    using Infrastructure;
    using Shop.Infrastructure;
    using Shop.Infrastructure.Validators;

    /// <summary>
    /// The price level service.
    /// </summary>
    public class PriceLevelService : Service<IPriceLevelRepository, PriceLevel>, IPriceLevelService
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PriceLevelService"/> class.
        /// </summary>
        /// <param name="repositoryFactory">
        /// The repository factory.
        /// </param>
        /// <param name="validator">
        /// The validator.
        /// </param>
        public PriceLevelService(IFactory repositoryFactory, IValidator<PriceLevel> validator) : base(repositoryFactory, validator)
        {
        }

        #endregion //Constructors

        #region IPriceLevelService Members

        /// <summary>
        /// Adds the price level with the specified <paramref name="name"/>.
        /// </summary>
        /// <param name="name">
        /// The price level name.
        /// </param>
        public void AddPriceLevel(string name)
        {
            var priceLevel = new PriceLevel
            {
                Name = name
            };

            this.Register(priceLevel);
        }

        /// <summary>
        /// Returns the price level with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">
        /// The price level id.
        /// </param>
        /// <returns>
        /// The price level with the specified <paramref name="id"/> or <b>null</b> if price level doesn't exist.
        /// </returns>
        public PriceLevel GetPriceLevelInfo(int id)
        {
            using (var repository = this.CreateRepository())
            {
                return repository.GetById(id);
            }
        }

        #endregion //IPriceLevelService
    }
}