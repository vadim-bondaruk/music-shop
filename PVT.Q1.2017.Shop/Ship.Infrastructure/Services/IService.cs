// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IService.cs" company="PVT.Q1.2017">
//   PVT.Q1.2017
// </copyright>
// <summary>
//   Base service contract (dummy)
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Shop.Infrastructure.Services
{
    #region

    using Shop.Infrastructure.Models;

    #endregion

    /// <summary>
    /// Base service contract (dummy)
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IService<TEntity>
        where TEntity : BaseEntity, new()
    {
    }
}