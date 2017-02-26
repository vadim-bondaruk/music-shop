// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlbumValidator.cs" company="PVT.Q1.2017">
//   PVT.Q1.2017
// </copyright>
// <summary>
//   The album validator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Shop.BLL.Validators
{
    #region

    using Shop.Common.Models;
    using Shop.Infrastructure.Validators;

    #endregion

    /// <summary>
    ///     The album validator.
    /// </summary>
    public class AlbumValidator : IValidator<Album>
    {
        /// <summary>
        ///     Deterines whether the <paramref name="album" /> is valid.
        /// </summary>
        /// <param name="album">The album to verify.</param>
        /// <returns>
        ///     <b>
        ///         <see langword="true" />
        ///     </b>
        ///     if the <paramref name="album" /> is valid; otherwise
        ///     <b>
        ///         <see langword="false" />
        ///     </b>
        ///     .
        /// </returns>
        public bool IsValid(Album album)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        ///     Validates the specified <paramref name="album" /> .
        /// </summary>
        /// <param name="album">The album to validate.</param>
        public void Validate(Album album)
        {
            throw new System.NotImplementedException();
        }
    }
}