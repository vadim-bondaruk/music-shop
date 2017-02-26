namespace PVT.Q1._2017.Shop.Models
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
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the album.
        /// </summary>
        public Album Album { get; set; }

        /// <summary>
        /// Gets or sets the artist.
        /// </summary>
        public Artist Artist { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        public TimeSpan? Duration { get; set; }

        /// <summary>
        /// Gets or sets the genre.
        /// </summary>
        public Genre Genre { get; set; }
    }
}