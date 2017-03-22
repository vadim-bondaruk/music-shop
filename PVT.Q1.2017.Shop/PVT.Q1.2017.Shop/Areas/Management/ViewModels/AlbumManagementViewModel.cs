namespace PVT.Q1._2017.Shop.Areas.Management.ViewModels
{
    using System;
    using System.Web;
    using global::Shop.Common.Models.ViewModels;

    /// <summary>
    /// The album management view model.
    /// </summary>
    public class AlbumManagementViewModel
    {
        /// <summary>
        /// Album id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Album name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Album cover.
        /// </summary>
        public HttpPostedFileBase Cover { get; set; }

        /// <summary>
        /// Album release date.
        /// </summary>
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        /// The Artist. Optional.
        /// </summary>
        public ArtistViewModel Artist { get; set; }
    }
}