namespace PVT.Q1._2017.Shop.Areas.Content.Controllers
{
    using System.Web.Mvc;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.ViewModels;
    using Shop.Controllers;

    /// <summary>
    /// The artist controller.
    /// </summary>
    public class ArtistController : BaseController
    {
        private readonly IArtistService _artistService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistController"/> class.
        /// </summary>
        /// <param name="artistService">
        /// The artist service.
        /// </param>
        public ArtistController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        /// <summary>
        /// Shows all artists.
        /// </summary>
        /// <returns>
        /// All artists view.
        /// </returns>
        public ActionResult List()
        {
            return this.View(_artistService.GetArtistsList());
        }

        /// <summary>
        /// Show artist info.
        /// </summary>
        /// <param name="id">
        /// The artist id.
        /// </param>
        /// <returns>
        /// Artist view.
        /// </returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("List", "Artist", new { area = "Content" });
            }

            var currency = GetCurrentUserCurrency();
            ArtistContentViewModel artistViewModel;

            if (currency != null)
            {
                var priceLevel = GetCurrentUserPriceLevel();
                artistViewModel = _artistService.GetContent(id.Value, currency.Code, priceLevel, GetUserDataId());
            }
            else
            {
                artistViewModel = _artistService.GetContent(id.Value);
            }

            if (artistViewModel == null)
            {
                return HttpNotFound($"Артист с id = { id.Value } не найден");
            }

            return this.View(artistViewModel);
        }
    }
}