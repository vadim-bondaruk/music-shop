namespace Shop.BLL.Validators
{
    using Common.Models;
    using Infrastructure.Validators;

    /// <summary>
    /// The album validator.
    /// </summary>
    public class AlbumValidator : IValidator<Album>
    {
        /// <summary>
        /// Validates the specified <paramref name="album"/>.
        /// </summary>
        /// <param name="album">
        /// The album to validate.
        /// </param>
        public void Validate(Album album)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Deterines whether the <paramref name="album"/> is valid.
        /// </summary>
        /// <param name="album">
        /// The album to verify.
        /// </param>
        /// <returns>
        /// <b>true</b> if the <paramref name="album"/> is valid; otherwise <b>false</b>.
        /// </returns>
        public bool IsValid(Album album)
        {
            throw new System.NotImplementedException();
        }
    }
}