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
    /// The vote service.
    /// </summary>
    public class VoteService : Service<IVoteRepository, Vote>, IVoteService
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VoteService"/> class.
        /// </summary>
        /// <param name="repositoryFactory">
        /// The repository factory.
        /// </param>
        /// <param name="validator">
        /// The validator.
        /// </param>
        public VoteService(IFactory repositoryFactory, IValidator<Vote> validator) : base(repositoryFactory, validator)
        {
        }

        #endregion //Constructors

        #region IVoteService Members

        /// <summary>
        /// Adds the <paramref name="user"/> mark for the <paramref name="track"/>.
        /// </summary>
        /// <param name="track">
        /// The track.
        /// </param>
        /// <param name="mark">
        /// The mark.
        /// </param>
        /// <param name="user">
        /// The user.
        /// </param>
        public void AddVote(Track track, Mark mark, User user)
        {
            ServiceHelper.CheckTrack(track);
            ServiceHelper.CheckUser(user);

            var vote = new Vote
            {
                Mark = mark,
                TrackId = track.Id,
                UserId = user.Id
            };

            this.Validator.Validate(vote);

            using (var repository = this.Factory.Create<IVoteRepository>())
            {
                repository.AddOrUpdate(vote);
            }
        }

        /// <summary>
        /// Returns the vote which have made the specified <paramref name="user"/> for the <paramref name="track"/>.
        /// </summary>
        /// <param name="track">
        /// The track.
        /// </param>
        /// <param name="user">
        /// The user who have rated the <paramref name="track"/>.
        /// </param>
        /// <returns>
        /// The vote for the <paramref name="track"/> which have made the specified <paramref name="user"/>
        /// or <b>null</b> in case if vote doesn't exist.
        /// </returns>
        public Vote GetTrackVote(Track track, User user)
        {
            ServiceHelper.CheckTrack(track);
            ServiceHelper.CheckUser(user);

            using (var repository = this.Factory.Create<IVoteRepository>())
            {
                return repository.GetAll(v => v.TrackId == track.Id && v.UserId == user.Id).FirstOrDefault();
            }
        }

        /// <summary>
        /// Returns the vote with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The vote id.</param>
        /// <returns>
        /// The vote with the specified <paramref name="id"/> or <b>null</b> if vote doesn't exist.
        /// </returns>
        public Vote GetVoteInfo(int id)
        {
            using (var repository = this.Factory.Create<IVoteRepository>())
            {
                return repository.GetById(id, v => v.Track, v => v.User);
            }
        }

        #endregion //IVoteService Members
    }
}