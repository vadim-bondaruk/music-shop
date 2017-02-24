// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IValidator.cs" company="PVT.Q1.2017">
//   PVT.Q1.2017
// </copyright>
// <summary>
//   Defines the IValidator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Shop.Infrastructure.Validators
{
    #region

    using Shop.Infrastructure.Models;

    #endregion

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidator<in T>
        where T : BaseEntity
    {
        /// <summary>
        ///     Determines whether the specified <paramref name="model" /> is valid.
        /// </summary>
        /// <param name="model">The model to validate.</param>
        /// <returns>
        ///     <b>
        ///         <see langword="true" />
        ///     </b>
        ///     if the specified <paramref name="model" /> is valid; otherwise
        ///     <b>
        ///         <see langword="false" />
        ///     </b>
        ///     .
        /// </returns>
        bool IsValid(T model);

        /// <summary>
        ///     Validates the specified <paramref name="model" />
        /// </summary>
        /// <param name="model">The model to validate.</param>
        void Validate(T model);
    }
}