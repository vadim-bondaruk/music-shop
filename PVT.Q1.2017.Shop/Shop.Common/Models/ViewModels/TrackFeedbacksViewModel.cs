namespace Shop.Common.Models.ViewModels
{
    using System.Collections.Generic;

    /// <summary>
    /// The track feedbacks view model
    /// </summary>
    public class TrackFeedbacksViewModel
    {
        /// <summary>
        /// Gets or sets the track id.
        /// </summary>
        public int TrackId { get; set; }

        /// <summary>
        /// Gets or sets the track name.
        /// </summary>
        public string TrackName { get; set; }
        
        /// <summary>
        /// Gets or sets the artist id.
        /// </summary>
        public int ArtistId { get; set; }

        /// <summary>
        /// Gets or sets the artist name.
        /// </summary>
        public string ArtistName { get; set; }

        /// <summary>
        /// Track rating.
        /// </summary>
        public RatingViewModel Rating { get; set; }

        /// <summary>
        /// Gets or sets the users feedbacks.
        /// </summary>
        public ICollection<FeedbackViewModel> UsersFeedbacks { get; set; }
    }
}