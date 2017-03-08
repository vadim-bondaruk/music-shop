//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Track.cs" company="PVT.Q1.2017">
//    PVT.Q1.2017
//  </copyright>
//  <summary>
//    The track.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------

namespace Shop.DAL.Repositories
{
    using System.Data.Entity;

    using Shop.Common.Models;
    using Shop.DAL.Infrastruture;

    /// <summary>
    ///     The feedback repository.
    /// </summary>
    public class FeedbackBaseRepository : BaseRepository<Feedback>, IFeedbackRepository
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="FeedbackBaseRepository" /> class.
        /// </summary>
        /// <param name="dbContext">
        ///     The db context.
        /// </param>
        public FeedbackBaseRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        #endregion //Constructors

        #region Protected Methods

        /// <summary>
        ///     Adds the specified <paramref name="feedback" /> into Db.
        /// </summary>
        /// <param name="feedback">
        ///     The feedback.
        /// </param>
        protected override void Add(Feedback feedback)
        {
            EntityState trackEntryState;
            EntityState userEntryState;

            // Detaching the navigation properties in case if they are attached to prevent unexpected behaviour of the DbContext.
            // The FeedbackBaseRepository should be SOLID, should only add information about feedbacks! Not about tracks or users!
            this.DetachNavigationProperty(feedback.Track, out trackEntryState);
            this.DetachNavigationProperty(feedback.User, out userEntryState);

            feedback.Track = null;
            feedback.User = null;

            // adding the feedback into Db.
            base.Add(feedback);
        }

        #endregion //Protected Methods
    }
}