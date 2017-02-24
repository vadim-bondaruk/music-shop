// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITrack.cs" company="PVT.Q1.2017">
//   PVT.Q1.2017
// </copyright>
// <summary>
//   Defines the ITrack type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Shop.Infrastructure.Models
{
    /// <summary>
    /// </summary>
    public interface ITrack
    {
        /// <summary>
        ///     Gets or sets the album.
        /// </summary>
        string Album { get; set; }

        /// <summary>
        ///     Gets or sets the artist.
        /// </summary>
        string Artist { get; set; }

        /// <summary>
        ///     Gets or sets the duration.
        /// </summary>
        string Duration { get; set; }

        /// <summary>
        ///     Gets or sets the genre.
        /// </summary>
        string Genre { get; set; }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the release date.
        /// </summary>
        string ReleaseDate { get; set; }

        /// <summary>
        ///     Gets or sets the year.
        /// </summary>
        string Year { get; set; }
    }
}