// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TrackController.cs" company="PVT.Q1.2017">
//   PVT.Q1.2017
// </copyright>
// <summary>
//   Defines the TrackController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PVT.Q1._2017.Shop.Controllers
{
    #region

    using System;
    using System.Web.Mvc;

    using global::Shop.DAL.Repositories;

    #endregion

    /// <summary>
    /// </summary>
    public class TrackController : Controller
    {
        /// <summary>
        /// </summary>
        private TrackRepository _trackRepository;

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public ActionResult AlbumList()
        {
            return this.View(this._trackRepository.GetAlbumList());
        }

        /// <summary>
        /// </summary>
        /// <param name="artistId">The artist id.</param>
        /// <returns>
        /// </returns>
        public ActionResult AlbumList(int artistId)
        {
            return this.View(this._trackRepository.GetAlbumList(artistId));
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public ActionResult ArtistList()
        {
            return this.View(this._trackRepository.GetArtistList());
        }

        /// <summary>
        /// </summary>
        /// <exception cref="System.NotImplementedException">
        ///     NotImplementedException
        /// </exception>
        /// <returns>
        /// </returns>
        public ActionResult TrackList()
        {
            return this.View(this._trackRepository.GetTrackList());
        }

        /// <summary>
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <param name="artistId">The artist id.</param>
        /// <returns>
        /// </returns>
        public ActionResult TrackList(int? albumId, int? artistId)
        {
            if (albumId == null && artistId != null)
            {
                return this.View(this._trackRepository.GetTrackList(null, artistId));
            }

            if (artistId == null && albumId != null)
            {
                return this.View(this._trackRepository.GetTrackList(albumId, null));
            }

            throw new Exception("No parameters!");
        }

        /// <summary>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// </returns>
        private ActionResult Details(int id)
        {
            return this.View(this._trackRepository.GetTrack(id));
        }
    }
}