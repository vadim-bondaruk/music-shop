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
    ///     Base service contract (dummy)
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IService<TEntity>
        where TEntity : BaseEntity, new()
    {
        /// <summary>
        /// Registers the specified <paramref name="model"/> in the system.
        /// </summary>
        /// <param name="model">
        /// A model to register.
        /// </param>
        void Register(TEntity model);

        /// <summary>
        /// Updates the specified <paramref name="model"/> in the system.
        /// </summary>
        /// <param name="model">
        /// A model to update.
        /// </param>
        void Update(TEntity model);

        /// <summary>
        /// Unregisters the specified <paramref name="model"/> from the system.
        /// </summary>
        /// <param name="model">
        /// A model to unregister.
        /// </param>
        /// <returns>
        /// <b>true</b> if the specified <paramref name="model"/> was unregistered successfully; otherwise <b>false</b>.
        /// </returns>
        bool Unregister(TEntity model);

        /// <summary>
        /// Determines whether the specified <paramref name="model"/> is valid.
        /// </summary>
        /// <param name="model">
        /// A model.
        /// </param>
        /// <returns>
        /// <b>true</b> if the specified <paramref name="model"/> is valid; otherwise <b>false</b>.
        /// </returns>
        bool IsValid(TEntity model);

        /// <summary>
        /// Determines whether the specified <paramref name="model"/> is registered.
        /// </summary>
        /// <param name="model">A model.</param>
        /// <returns>
        /// <b>true</b> if the specified <paramref name="model"/> is registered; otherwise <b>false</b>.
        /// </returns>
        bool IsRegistered(TEntity model);
    }
}