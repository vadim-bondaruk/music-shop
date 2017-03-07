namespace Shop.BLL.Services
{
    using System.Linq;
    using Common.Models;
    using DAL.Infrastruture;
    using Infrastructure;

    /// <summary>
    /// The vote service.
    /// </summary>
    public class VoteService : BaseService, IVoteService
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VoteService"/> class.
        /// </summary>
        /// <param name="repositoryFactory">
        /// The repository factory.
        /// </param>
        public VoteService(IRepositoryFactory repositoryFactory) : base(repositoryFactory)
        {
        }

        #endregion //Constructors

        #region IVoteService Members

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
            using (var repository = this.Factory.CreateVoteRepository())
            {
                return repository.GetAll(
                                         v => v.TrackId == track.Id && v.UserId == user.Id,
                                         v => v.Track,
                                         v => v.User).FirstOrDefault();
            }
        }

        /// <summary>
        /// Determines whether the specified <paramref name="user"/> have made a vote for the <paramref name="track"/>.
        /// </summary>
        /// <param name="track">
        /// The track.
        /// </param>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <returns>
        /// <b>true</b> if the specified <paramref name="user"/> have made a vote for the <paramref name="track"/>;
        /// otherwise <b>false</b>.
        /// </returns>
        public bool VoteExists(Track track, User user)
        {
            using (var repository = this.Factory.CreateVoteRepository())
            {
                return repository.GetAll(f => f.TrackId == track.Id && f.UserId == user.Id).Any();
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
            using (var repository = this.Factory.CreateVoteRepository())
            {
                return repository.GetById(id, v => v.Track, v => v.User);
            }
        }

        #endregion //IVoteService Members
    }
}