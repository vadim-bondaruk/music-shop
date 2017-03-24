﻿namespace PVT.Q1._2017.Shop.Areas.Management.Controllers
{
    using System.Web.Mvc;

    using AutoMapper;

    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.Common.Models.ViewModels;
    using global::Shop.DAL.Infrastruture;

    /// <summary>
    /// </summary>
    public class ArtistsController : Controller
    {
        /// <summary>
        /// </summary>
        private readonly IArtistService artistService;

        /// <summary>
        /// </summary>
        private readonly IRepositoryFactory repositoryFactory;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ArtistsController" /> class.
        /// </summary>
        /// <param name="artistService">
        ///     The artist service.
        /// </param>
        /// <param name="repositoryFactory"></param>
        public ArtistsController(IArtistService artistService, IRepositoryFactory repositoryFactory)
        {
            this.artistService = artistService;
            this.repositoryFactory = repositoryFactory;
        }

        /// <summary>
        /// </summary>
        /// <param name="viewModel">
        /// The view model.
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Delete([Bind(Include = "Id")]ArtistManageViewModel viewModel)
        {
            var artist = Mapper.Map<Artist>(viewModel);
            this.artistService.Delete(artist);
            return this.RedirectToAction("New");
        }

        /// <summary>
        /// </summary>
        /// <param name="artistId">
        ///     The artist id.
        /// </param>
        /// <returns>
        /// </returns>
        public virtual ActionResult Details(int artistId = 0)
        {
            var artist = this.artistService.GetById(artistId);
            return this.View(artist);
        }

        /// <summary>
        /// </summary>
        /// <param name="artistId">
        ///     The artist id.
        /// </param>
        /// <returns>
        /// </returns>
        public virtual ActionResult Edit(int artistId)
        {
            var viewModel = this.artistService.GetById(artistId);
            return this.View(viewModel);
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public virtual ActionResult New()
        {
            return this.View();
        }

        /// <summary>
        /// </summary>
        /// <param name="viewModel">
        ///     The view model.
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult New(
            [Bind(Include = "Name, Birthday, Biography, UploadedImage")] ArtistManageViewModel viewModel)
        {
            var id = this.artistService.Save(viewModel);
            return this.RedirectToAction("Details", new { artistId = id });
        }

        /// <summary>
        /// </summary>
        /// <param name="viewModel">
        ///     The view model.
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Update(
            [Bind(Include = "Id, Name, Birthday, Biography, UploadedImage, Photo")] ArtistManageViewModel viewModel)
        {
            this.artistService.Save(viewModel);
            return this.RedirectToAction("Details", new { artistId = viewModel.Id });
        }
    }
}