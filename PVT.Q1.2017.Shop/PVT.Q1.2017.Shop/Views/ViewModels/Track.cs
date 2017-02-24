// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Track.cs" company="PVT.Q1.2017">
//   PVT.Q1.2017
// </copyright>
// <summary>
//   Defines the Track type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PVT.Q1._2017.Shop.ViewModels
{
    using global::Shop.Infrastructure.Models;

    /// <summary>
    /// </summary>
    public class Track
    {
        /// <summary>
        /// </summary>
        private readonly ITrack _track;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Track" /> class.
        /// </summary>
        /// <param name="track">The track.</param>
        public Track(ITrack track)
        {
            this._track = track;
        }

        /// <summary>
        ///     Gets the album.
        /// </summary>
        public string Album
        {
            get
            {
                return this._track.Album;
            }
        }

        /// <summary>
        ///     Gets the artist.
        /// </summary>
        public string Artist
        {
            get
            {
                return this._track.Artist;
            }
        }

        /// <summary>
        ///     Gets the genre.
        /// </summary>
        public string Genre
        {
            get
            {
                return this._track.Genre;
            }
        }

        /// <summary>
        ///     Gets the id.
        /// </summary>
        public int Id
        {
            get
            {
                return this._track.Id;
            }
        }

        /// <summary>
        /// Gets the image url.
        /// </summary>
        public string ImageUrl
        {
            get
            {
                return this._track.Year;
            }
        }

        /// <summary>
        ///     Gets the name.
        /// </summary>
        public string Name
        {
            get
            {
                return this._track.Name;
            }
        }

        /// <summary>
        ///     Gets the release date.
        /// </summary>
        public string ReleaseDate
        {
            get
            {
                return this._track.ReleaseDate;
            }
        }

        /// <summary>
        ///     Gets the year.
        /// </summary>
        public string Year
        {
            get
            {
                return this._track.Year;
            }
        }
    }
}