namespace Shop.Common.Models.ViewModels
{
    /// <summary>
    /// The feedback view model.
    /// </summary>
    public class FeedbackViewModel
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public int UserDataId { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets the mark.
        /// </summary>
        public int Mark { get; set; }
    }
}