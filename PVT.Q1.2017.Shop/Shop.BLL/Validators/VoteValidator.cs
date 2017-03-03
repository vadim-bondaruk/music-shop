namespace Shop.BLL.Validators
{
    using System;
    using Common.Models;
    using Exceptions;
    using Infrastructure.Validators;

    /// <summary>
    /// </summary>
    public class VoteValidator : IValidator<Vote>
    {
        /// <summary>
        /// Determines whether the <paramref name="mark"/> is valid.
        /// </summary>
        /// <param name="mark">
        /// The mark.
        /// </param>
        /// <returns>
        /// <b>true</b> if the <paramref name="mark"/> is valid; otherwise <b>false</b>.
        /// </returns>
        public static bool IsMarkValid(Mark mark)
        {
            return Enum.IsDefined(typeof(Mark), mark);
        }

        /// <summary>
        /// Validates the specified <paramref name="vote"/>.
        /// </summary>
        /// <param name="vote">
        /// The vote.
        /// </param>
        public virtual void Validate(Vote vote)
        {
            if (vote == null)
            {
                throw new ArgumentNullException(nameof(vote));
            }

            if (!IsMarkValid(vote.Mark))
            {
                throw new InvalidEntityException("Invalid comment specified.");
            }
        }

        /// <summary>
        /// Determines whether the <paramref name="vote"/> is valid.
        /// </summary>
        /// <param name="vote">
        /// The feedback to verify.
        /// </param>
        /// <returns>
        /// <b>true</b> if the <paramref name="vote"/> is valid; otherwise <b>false</b>.
        /// </returns>
        public virtual bool IsValid(Vote vote)
        {
            if (vote == null)
            {
                return false;
            }

            if (!IsMarkValid(vote.Mark))
            {
                return false;
            }

            return true;
        }
    }
}