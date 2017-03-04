namespace Shop.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common.Models;
    using DAL.Repositories.Infrastruture;
    using Infrastructure;
    using Shop.Infrastructure;
    using Shop.Infrastructure.Validators;

    /// <summary>
    /// 
    /// </summary>
    class CurrencyService : Service<ICurrencyRepository,Currency>
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public CurrencyService(IFactory repositoryFactory, IValidator<Currency> validator) : base(repositoryFactory, validator)
        {

        }
        #endregion
    }
}
