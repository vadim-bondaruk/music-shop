namespace Shop.BLL.Services
{
    using System.Linq;
    using Common.Models;
    using DAL.Infrastruture;
    using Infrastructure;

    /// <summary>
    /// The feedback service
    /// </summary>
    public class FeedbackService : BaseService, IFeedbackService
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackService"/> class.
        /// </summary>
        /// <param name="repositoryFactory">
        /// The repository factory.
        /// </param>
        public FeedbackService(IRepositoryFactory repositoryFactory) : base(repositoryFactory)
        {
        }

        #endregion //Constructors

        #region IFeedbackService Members

        /// <summary>
        /// Returns the feedback which have made the specified <paramref name="user"/> for the <paramref name="track"/>.
        /// </summary>
        /// <param name="track">
        /// The track.
        /// </param>
        /// <param name="user">
        /// The user who have left opinion for the <paramref name="track"/>.
        /// </param>
        /// <returns>
        /// The feedback for the <paramref name="track"/> which have made the specified <paramref name="user"/>
        /// or <b>null</b> in case if feedback doesn't exist.
        /// </returns>
        public Feedback GetTrackFeedback(Track track, User user)
        {
            using (var repository = this.Factory.CreateFeedbackRepository())
            {
                return repository.GetAll(
                                         f => f.TrackId == track.Id && f.UserId == user.Id,
                                         f => f.Track,
                                         f => f.User).FirstOrDefault();
            }
        }

        /// <summary>
        /// Determines whether the specified <paramref name="user"/> have made a feedback for the <paramref name="track"/>.
        /// </summary>
        /// <param name="track">
        /// The track.
        /// </param>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <returns>
        /// <b>true</b> if the specified <paramref name="user"/> have made a feedback for the <paramref name="track"/>;
        /// otherwise <b>false</b>.
        /// </returns>
        public bool FeedbackExists(Track track, User user)
        {
            using (var repository = this.Factory.CreateFeedbackRepository())
            {
                return repository.GetAll(f => f.TrackId == track.Id && f.UserId == user.Id).Any();
            }
        }

        /// <summary>
        /// Returns the feedback with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The feedback id.</param>
        /// <returns>
        /// The feedback with the specified <paramref name="id"/> or <b>null</b> if feedback doesn't exist.
        /// </returns>
        public Feedback GetFeedbackInfo(int id)
        {
            using (var repository = this.Factory.CreateFeedbackRepository())
            {
                return repository.GetById(id, v => v.Track, v => v.User);
            }
        }

        #endregion //IFeedbackService Members
    }
}