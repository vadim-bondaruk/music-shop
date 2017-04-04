namespace Shop.Common.Models
{
    /// <summary>
    /// The rating view model.
    /// </summary>
    public class RatingViewModel
    {
        /// <summary>
        /// Rating.
        /// </summary>
        public double Rating { get; set; }

        /// <summary>
        /// The number of votes.
        /// </summary>
        public int VotesCount { get; set; }

        /// <summary>
        /// The number of comments.
        /// </summary>
        public int CommentsCount { get; set; }
    }
}