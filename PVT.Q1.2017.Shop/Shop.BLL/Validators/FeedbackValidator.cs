namespace Shop.BLL.Validators
{
    using System;
    using Common.Models;
    using Exceptions;
    using Infrastructure.Validators;

    /// <summary>
    /// </summary>
    public class FeedbackValidator : IValidator<Feedback>
    {
        /// <summary>
        /// Determines whether the <paramref name="comment"/> is valid.
        /// </summary>
        /// <param name="comment">
        /// The comment.
        /// </param>
        /// <returns>
        /// <b>true</b> if the <paramref name="comment"/> is valid; otherwise <b>false</b>.
        /// </returns>
        public static bool IsCommentValid(string comment)
        {
            return !string.IsNullOrWhiteSpace(comment);
        }

        /// <summary>
        /// Validates the specified <paramref name="feedback"/>.
        /// </summary>
        /// <param name="feedback">
        /// The item to validate.
        /// </param>
        public virtual void Validate(Feedback feedback)
        {
            if (feedback == null)
            {
                throw new ArgumentNullException(nameof(feedback));
            }

            if (!IsCommentValid(feedback.Comments))
            {
                throw new InvalidEntityException("Invalid comment specified.");
            }
        }

        /// <summary>
        /// Determines whether the <paramref name="feedback"/> is valid.
        /// </summary>
        /// <param name="feedback">
        /// The feedback to verify.
        /// </param>
        /// <returns>
        /// <b>true</b> if the <paramref name="feedback"/> is valid; otherwise <b>false</b>.
        /// </returns>
        public virtual bool IsValid(Feedback feedback)
        {
            if (feedback == null)
            {
                return false;
            }

            if (!IsCommentValid(feedback.Comments))
            {
                return false;
            }

            return true;
        }
    }
}