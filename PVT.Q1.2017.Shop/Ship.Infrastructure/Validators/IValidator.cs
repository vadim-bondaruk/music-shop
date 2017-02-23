namespace Shop.Infrastructure.Validators
{
    using Models;

    /// <summary>
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public interface IValidator<in T> where T : BaseEntity
    {
        /// <summary>
        /// Validates the specified <paramref name="model"/>
        /// </summary>
        /// <param name="model">
        /// The model to validate.
        /// </param>
        void Validate(T model);

        /// <summary>
        /// Determines whether the specified <paramref name="model"/> is valid.
        /// </summary>
        /// <param name="model">
        /// The model to validate.
        /// </param>
        /// <returns>
        /// <b>true</b> if the specified <paramref name="model"/> is valid; otherwise <b>false</b>.
        /// </returns>
        bool IsValid(T model);
    }
}