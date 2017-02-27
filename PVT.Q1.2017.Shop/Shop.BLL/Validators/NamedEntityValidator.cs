namespace Shop.BLL.Validators
{
    using System;
    using Common.Models;
    using Exceptions;
    using Infrastructure.Validators;

    /// <summary>
    /// The named entity validator.
    /// </summary>
    public class NamedEntityValidator<TNamedEntity> : IValidator<TNamedEntity> where TNamedEntity : BaseNamedEntity, new()
    {
        /// <summary>
        /// Determines whether the item <paramref name="name"/> is valid.
        /// </summary>
        /// <param name="name">
        /// The item name.
        /// </param>
        /// <returns>
        /// <b>true</b> if the item <paramref name="name"/> is valid; otherwise <b>false</b>.
        /// </returns>
        public static bool IsNameValid(string name)
        {
            return !string.IsNullOrWhiteSpace(name);
        }

        /// <summary>
        /// Validates the specified <paramref name="item"/>.
        /// </summary>
        /// <param name="item">
        /// The item to validate.
        /// </param>
        public virtual void Validate(TNamedEntity item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (!IsNameValid(item.Name))
            {
                throw new InvalidEntityException("Invalid entity name specified.");
            }
        }

        /// <summary>
        /// Determines whether the <paramref name="item"/> is valid.
        /// </summary>
        /// <param name="item">
        /// The item to verify.
        /// </param>
        /// <returns>
        /// <b>true</b> if the <paramref name="item"/> is valid; otherwise <b>false</b>.
        /// </returns>
        public virtual bool IsValid(TNamedEntity item)
        {
            if (item == null)
            {
                return false;
            }

            if (!IsNameValid(item.Name))
            {
                return false;
            }

            return true;
        }
    }
}