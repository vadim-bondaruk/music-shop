// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TracksController.cs" company="PVT.Q1.2017">
//   PVT.Q1.2017
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

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public ActionResult AlbumList()
        {
            return View(_trackRepository.GetAlbumList());
        }

        /// <summary>
        /// </summary>
        /// <param name="artistId">
        ///     The artist id.
        /// </param>
        /// <returns>
        /// </returns>
        public ActionResult AlbumList(int artistId)
        {
            return View(_trackRepository.GetAlbumList(artistId));
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public ActionResult ArtistList()
        {
            return View(_trackRepository.GetArtistList());
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// NotImplementedException
        /// </exception>
        public ActionResult TrackList()
        {
            return View(_trackRepository.GetTrackList());
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
        public ActionResult TrackList(int? albumId, int? artistId)
        {
            if (albumId == null && artistId != null)
            {
                return View(_trackRepository.GetTrackList(null, artistId));
            }

            if (artistId == null && albumId != null)
            {
                return View(_trackRepository.GetTrackList(albumId, null));
            }

            throw new Exception("No parameters!");
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
            return View(_trackRepository.GetTrack(id));
        }
    }
}