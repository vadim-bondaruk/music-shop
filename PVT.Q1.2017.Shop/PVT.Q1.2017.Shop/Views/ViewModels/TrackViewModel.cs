// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TrackViewModel.cs" company="PVT.Q1.2017">
//   PVT.Q1.2017
// </copyright>
// <summary>
//   Defines the TrackViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PVT.Q1._2017.Shop.Views.ViewModels
{
    using System;

    using global::Shop.Common.Models;
    using global::Shop.DAL.Context;
    using global::Shop.DAL.Repositories;

    /// <summary>
    /// </summary>
    public class TrackViewModel
    {
        /// <summary>
        /// </summary>
        private readonly TrackViewModel _track;

        /// <summary>
        /// </summary>
        private Repository<Track> _repo;

        /// <summary>
        ///     <para>
        ///         Initializes a new instance of the <see cref="TrackViewModel" />
        ///     </para>
        ///     <para>class.</para>
        /// </summary>
        /// <param name="track">The track.</param>
        public TrackViewModel()
        {
            this._repo = new Repository<Track>(new ShopContext());
        }

        /// <summary>
        /// Gets or sets the album.
        /// </summary>
        public string Album { get; set; }

        /// <summary>
        /// Gets or sets the artist.
        /// </summary>
        public string Artist { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        public TimeSpan? Duration { get; set; }

        /// <summary>
        /// Gets or sets the genre.
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the release date.
        /// </summary>
        public string ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        public string Year { get; set; }
    }
}