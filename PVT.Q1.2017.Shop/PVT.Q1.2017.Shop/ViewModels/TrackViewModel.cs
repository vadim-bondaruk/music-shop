namespace PVT.Q1._2017.Shop.ViewModels
{
    #region using

    using global::Shop.Common.Models;
    using global::Shop.DAL.Context;
    using global::Shop.DAL.Repositories;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    public class TrackViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly TrackViewModel _track;

        /// <summary>
        /// 
        /// </summary>
        private BaseRepository<Track> _repo;

        /// <summary>
        /// <para>
        /// Initializes a new instance of the <see cref="TrackViewModel" />
        /// </para>
        /// <para>class.</para>
        /// </summary>
        /// <param name="track">The track.</param>
        public TrackViewModel()
        {
            this._repo = new TrackBaseRepository(new ShopContext());
        }

        /// <summary>
        /// Gets or sets the album.
        /// </summary>
        public Album Album { get; set; }

        /// <summary>
        /// Gets or sets the artist.
        /// </summary>
        public Artist Artist { get; set; }

        /// <summary>
        /// Gets or sets the track.
        /// </summary>
        public Track Track { get; set; }
    }
}