// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArtistValidator.cs" company="PVT.Q1.2017">
//   PVT.Q1.2017
// </copyright>
// <summary>
//   Defines the ArtistValidator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Shop.BLL.Validators
{
    #region

    using System;

    using Shop.Common.Models;
    using Shop.Infrastructure.Validators;

    #endregion

    /// <summary>
    /// </summary>
    public class ArtistValidator : IValidator<Artist>
    {
        /// <summary>
        /// Deterines whether the <paramref name="artist"/> is valid.
        /// </summary>
        /// <param name="artist">
        /// The artist to verify.
        /// </param>
        /// <returns>
        /// <b>true</b> if the <paramref name="artist"/> is valid; otherwise <b>false</b>.
        /// </returns>
        public bool IsValid(Artist artist)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Validates the specified <paramref name="artist"/>.
        /// </summary>
        /// <param name="artist">
        /// The artist to validate.
        /// </param>
        public void Validate(Artist artist)
        {
            throw new NotImplementedException();
        }
    }
}