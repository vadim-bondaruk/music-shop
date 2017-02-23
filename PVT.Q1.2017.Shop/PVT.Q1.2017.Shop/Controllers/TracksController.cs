// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TracksController.cs" company="PurpleTeam">
//   PurpleTeam
// </copyright>
// <summary>
//   Defines the TracksController type.
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
    public class TracksController : Controller
    {
        /// <summary>
        /// </summary>
        private TrackRepository _trackRepository;

        public ActionResult ArtistList()
        {
            return this.View(this._trackRepository.GetArtistList());
        }


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
        /// <param name="artistId">
        /// The artist id.
        /// </param>
        /// <returns>
        /// </returns>
        public ActionResult AlbumList(int artistId)
        {
            return this.View(this._trackRepository.GetAlbumList(id));
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public ActionResult TrackList()
        {
            return this.View(this._trackRepository.GetTrackList());
        }

        /// <summary>
        /// </summary>
        /// <param name="albumId">
        ///     The album id.
        /// </param>
        /// <param name="artistId">
        ///     The artist id.
        /// </param>
        /// <returns>
        /// </returns>
        public ActionResult TrackList(int albumId, int artistId)
        {
            return this.View(this._trackRepository.GetTrackList(artistId));
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <returns>
        /// </returns>
        private ActionResult Details(int id)
        {
            return this.View(this._trackRepository.GetTrack(id));
        }
    }
}