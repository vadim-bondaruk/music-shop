// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Feedback.cs" company="PVT.Q1.2017">
//   PVT.Q1.2017
// </copyright>
// <summary>
//   The feedback.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Shop.Common.Models
{
    #region

    using Shop.Infrastructure.Models;

    #endregion

    /// <summary>
    ///     The feedback.
    /// </summary>
    public class Feedback : BaseEntity
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the user's comments.
        /// </summary>
        public string Comments { get; set; }

        #endregion //Properties

        #region Foreign Keys

        /// <summary>
        ///     Gets or sets the track for which the user have written the
        ///     <see cref="Shop.Common.Models.Feedback.Comments" /> .
        /// </summary>
        public int TrackId { get; set; }

        /// <summary>
        ///     Gets or sets the user who wrote the
        ///     <see cref="Shop.Common.Models.Feedback.Comments" /> .
        /// </summary>
        public int UserId { get; set; }

        #endregion //Foreign Keys

        #region Navigation Properties

        /// <summary>
        ///     Gets or sets the track for which the
        ///     <see cref="Shop.Common.Models.Feedback.User" /> have written the
        ///     <see cref="Shop.Common.Models.Feedback.Comments" /> .
        /// </summary>
        public virtual Track Track
        {
            get; set;
        }

        /// <summary>
        ///     Gets or sets the user who wrote the
        ///     <see cref="Shop.Common.Models.Feedback.Comments" /> .
        /// </summary>
        public virtual User User
        {
            get; set;
        }

        #endregion //Navigation Properties
    }
}