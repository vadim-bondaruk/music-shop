namespace Shop.BLL.Services
{
    using System.Linq;
    using Common.Models;
    using DAL.Repositories.Infrastruture;
    using Helpers;
    using Infrastructure;
    using Shop.Infrastructure;
    using Shop.Infrastructure.Validators;

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
            ServiceHelper.CheckTrack(track);
            ServiceHelper.CheckUser(user);

            var feedback = new Feedback
            {
                Comments = comments,
                TrackId = track.Id,
                UserId = user.Id
            };

            this.Validator.Validate(feedback);

            using (var repository = this.Factory.Create<IFeedbackRepository>())
            {
                repository.AddOrUpdate(feedback);
            }
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
            ServiceHelper.CheckTrack(track);
            ServiceHelper.CheckUser(user);

            using (var repository = this.Factory.Create<IFeedbackRepository>())
            {
                return repository.GetAll(f => f.TrackId == track.Id && f.UserId == user.Id).FirstOrDefault();
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
            using (var repository = this.Factory.Create<IFeedbackRepository>())
            {
                return repository.GetById(id, v => v.Track, v => v.User);
            }
        }

        #endregion //IFeedbackService Members
    }
}