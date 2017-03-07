namespace Shop.BLL.Services.Infrastructure
{
    using Common.Models;

    /// <summary>
    /// The vote service.
    /// </summary>
    public interface IVoteService
    {
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
        void AddVote(Track track, Mark mark, User user);

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
        Vote GetTrackVote(Track track, User user);

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
        bool VoteExists(Track track, User user);

        /// <summary>
        /// Returns the vote with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The vote id.</param>
        /// <returns>
        /// The vote with the specified <paramref name="id"/> or <b>null</b> if vote doesn't exist.
        /// </returns>
        Vote GetVoteInfo(int id);
    }
}