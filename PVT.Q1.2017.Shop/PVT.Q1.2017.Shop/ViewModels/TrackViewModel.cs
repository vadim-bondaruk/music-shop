namespace PVT.Q1._2017.Shop.ViewModels
{
    using System;
    using System.Collections.Generic;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Repositories;
    using Ninject;

    /// <summary>
    /// </summary>
    public class TrackViewModel
    {
        /// <summary>
        /// </summary>
        private readonly TrackViewModel _track;

        /// <summary>
        /// </summary>
        private BaseRepository<Track> _repo;

        /// <summary>
        ///     <para>
        ///         Initializes a new instance of the <see cref="TrackViewModel" />
        ///     </para>
        ///     <para>class.</para>
        /// </summary>
        /// <param name="track">The track.</param>
        public TrackViewModel()
        {
            var kernel = new StandardKernel();
            this._repo = kernel.Get<BaseRepository<Track>>();
        }

        /// <summary>
        ///     Gets or sets the album.
        /// </summary>
        public ICollection<Album> Album { get; set; }

        /// <summary>
        ///     Gets or sets the artist.
        /// </summary>
        public Artist Artist { get; set; }

        /// <summary>
        ///     Gets or sets the duration.
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        ///     Gets or sets the track.
        /// </summary>
        public string Name { get; set; }
    }
}