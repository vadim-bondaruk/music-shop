using Shop.DAL.Infrastruture;

namespace Shop.BLL.Services
{
    using Common.Models;
    using Infrastructure;

    /// <summary>
    /// The feedback service
    /// </summary>
    public class FeedbackService : Service<IFeedbackRepository, Feedback>, IFeedbackService
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackService"/> class.
        /// </summary>
        /// <param name="repositoryFactory">
        /// The repository factory.
        /// </param>
        /// <param name="validator">
        /// The validator.
        /// </param>
        public FeedbackService(IFactory repositoryFactory, IValidator<Feedback> validator) : base(repositoryFactory, validator)
        {
        }

        #endregion //Constructors

        #region IFeedbackService Members

        /// <summary>
        /// Adds the <paramref name="user"/> comments for the <paramref name="track"/>.
        /// </summary>
        /// <param name="track">
        /// The track.
        /// </param>
        /// <param name="comments">
        /// The comments.
        /// </param>
        /// <param name="user">
        /// The user.
        /// </param>
        public void AddFeedback(Track track, string comments, User user)
        {
            ValidatorHelper.CheckTrackForNull(track);
            ValidatorHelper.CheckUserForNull(user);

            var feedback = new Feedback
            {
                Comments = comments,
                TrackId = track.Id,
                UserId = user.Id
            };

            if (this.IsValid(feedback))
            {
                // if a feedback by the user already exits for the track than removing old one to register new one 
                var currentFeedback = this.GetTrackFeedback(track, user);
                if (currentFeedback != null)
                {
                    this.Unregister(currentFeedback);
                }
            }

            this.Register(feedback);
        }

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
            ValidatorHelper.CheckTrackForNull(track);
            ValidatorHelper.CheckUserForNull(user);

            using (var repository = this.CreateRepository())
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
            ValidatorHelper.CheckTrackForNull(track);
            ValidatorHelper.CheckUserForNull(user);

            using (var repository = this.CreateRepository())
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
            using (var repository = this.CreateRepository())
            {
                return repository.GetById(id, v => v.Track, v => v.User);
            }
        }

        #endregion //IFeedbackService Members
    }
}