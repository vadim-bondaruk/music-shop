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

    using System.Web.Mvc;

    using global::Shop.Common.Models;
    using global::Shop.DAL.Context;
    using global::Shop.DAL.Repositories;

    #endregion

    /// <summary>
    /// </summary>
    public class TrackController : Controller
    {
        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public ActionResult AlbumList()
        {
            using (var context = new ShopContext())
            {
                var repository = new Repository<Album>(context);
                return this.View(repository.GetAll());
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="artistId">The artist id.</param>
        /// <returns>
        /// </returns>
        public ActionResult AlbumList(int artistId)
        {
            using (var context = new ShopContext())
            {
                var repository = new Repository<Album>(context);
                return this.View(repository.GetAll(a => a.Artist.Id.Equals(artistId)));
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// </returns>
        public ActionResult AlbumTracks(int id)
        {
            {
                using (var context = new ShopContext())
                {
                    var repository = new Repository<Track>(context);
                    return this.View(repository.GetAll(a => a.Album.Id.Equals(id)));
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public ActionResult ArtistList()
        {
            using (var context = new ShopContext())
            {
                var repository = new Repository<Artist>(context);
                return this.View(repository.GetAll());
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// </returns>
        public ActionResult ArtistTracks(int id)
        {
            {
                using (var context = new ShopContext())
                {
                    var repository = new Repository<Track>(context);
                    return this.View(repository.GetAll(a => a.Album.Id.Equals(id)));
                }
            }
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
            using (var context = new ShopContext())
            {
                var repository = new Repository<Track>(context);
                return this.View(repository.GetAll());
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// </returns>
        public ActionResult Details(int id)
        {
            using (var context = new ShopContext())
            {
                var repository = new Repository<Track>(context);
                return this.View(repository.GetAll(t => t.Id.Equals(id)));
            }
        }
    }
}