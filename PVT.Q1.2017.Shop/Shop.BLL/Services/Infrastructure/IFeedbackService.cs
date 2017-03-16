namespace Shop.BLL.Services.Infrastructure
{
    using Common.Models;

    /// <summary>
    /// The feedback service.
    /// </summary>
    public interface IFeedbackService
    {
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
        Feedback GetTrackFeedback(Track track, UserData user);

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
        bool FeedbackExists(Track track, UserData user);

        /// <summary>
        /// Returns the feedback with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The feedback id.</param>
        /// <returns>
        /// The feedback with the specified <paramref name="id"/> or <b>null</b> if feedback doesn't exist.
        /// </returns>
        Feedback GetFeedback(int id);
    }
}