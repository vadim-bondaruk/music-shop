// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Track.cs" company="PVT Q1 2017">
//   All rights reserved
// </copyright>
// <summary>
//   The track.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Shop.Common.Models
{
    using System;
    using System.Collections.Generic;
    using Infrastructure.Models;

    /// <summary>
    /// The track.
    /// </summary>
    public class Track : BaseEntity
    {
        #region Fields

        /// <summary>
        /// The album.
        /// </summary>
        private Album _album;

        /// <summary>
        /// The artist.
        /// </summary>
        private Artist _artist;

        /// <summary>
        /// The track genre.
        /// </summary>
        private Genre _genre;

        #endregion //Fields

        #region Events

        /// <summary>
        /// Occurs when the track has not empty <see cref="ArtistId"/> and tries to return requested <see cref="Artist"/> which is not filled yet.
        /// </summary>
        public event EventHandler<Artist> NeedArtistInfo;

        /// <summary>
        /// Occurs when the track has not empty <see cref="AlbumId"/> and tries to return requested <see cref="Album"/> which is not filled yet.
        /// </summary>
        public event EventHandler<Album> NeedAlbumInfo;

        /// <summary>
        /// Occurs when the track has not empty <see cref="GenreId"/> and tries to return requested <see cref="Genre"/> which is not filled yet.
        /// </summary>
        public event EventHandler<Genre> NeedGenreInfo;

        #endregion //Events

        #region Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        public byte[] Image { get; set; }

        /// <summary>
        /// Gets or sets the track file.
        /// </summary>
        public byte[] TrackFile { get; set; }

        /// <summary>
        /// Gets or sets the track duration.
        /// </summary>
        public TimeSpan? Duration { get; set; }

        #endregion //Properties

        #region Navigation Properties

        /// <summary>
        /// Gets or sets the album id.
        /// </summary>
        public int? AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the album.
        /// </summary>
        public Album Album
        {
            get
            {
                if (this.AlbumId != null && this._album == null)
                {
                    this.OnNeedAlbumInfo(this._album);
                }

                return this._album;
            }

            set
            {
                this._album = value;
            }
        }

        /// <summary>
        /// Gets or sets the artist id.
        /// </summary>
        public int? ArtistId { get; set; }

        /// <summary>
        /// Gets or sets the artist.
        /// </summary>
        public Artist Artist
        {
            get
            {
                if (this.ArtistId != null && this._artist == null)
                {
                    this.OnNeedArtistInfo(this._artist);
                }

                return this._artist;
            }

            set
            {
                this._artist = value;
            }
        }

        /// <summary>
        /// Gets or sets the genre id.
        /// </summary>
        public int? GenreId { get; set; }

        /// <summary>
        /// Gets or sets the genre.
        /// </summary>
        public Genre Genre
        {
            get
            {
                if (this.GenreId != null && this._genre == null)
                {
                    this.OnNeedGenreInfo(this._genre);
                }

                return this._genre;
            }

            set
            {
                this._genre = value;
            }
        }

        /// <summary>
        /// Gets or sets the track prices.
        /// </summary>
        public ICollection<TrackPrice> TrackPrices { get; set; }

        /// <summary>
        /// Gets or sets all user votes for the current track.
        /// </summary>
        public ICollection<Vote> Votes { get; set; }

        /// <summary>
        /// Gets or sets all user feedbacks the current track.
        /// </summary>
        public ICollection<Feedback> Feedbacks { get; set; }

        #endregion //Navigation Properties

        #region Protected Methods

        /// <summary>
        /// Raises the <see cref="NeedArtistInfo"/> event.
        /// </summary>
        protected virtual void OnNeedArtistInfo(Artist artist)
        {
            this.NeedArtistInfo?.Invoke(this, artist);
        }

        /// <summary>
        /// Raises the <see cref="NeedAlbumInfo"/> event.
        /// </summary>
        protected virtual void OnNeedAlbumInfo(Album album)
        {
            this.NeedAlbumInfo?.Invoke(this, album);
        }

        /// <summary>
        /// Raises the <see cref="NeedGenreInfo"/> event.
        /// </summary>
        protected virtual void OnNeedGenreInfo(Genre genre)
        {
            this.NeedGenreInfo?.Invoke(this, genre);
        }

        #endregion //Protected Methods
    }
}