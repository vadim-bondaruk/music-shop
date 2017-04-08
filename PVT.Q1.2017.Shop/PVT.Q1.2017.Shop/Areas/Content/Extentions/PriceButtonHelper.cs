namespace PVT.Q1._2017.Shop.Areas.Content.Extentions
{
    using System.Web.Mvc;

    /// <summary>
    /// The price buttons helper.
    /// </summary>
    public static class PriceButtonHelper
    {
        private const string DEFAULT_PRICE_BUTTON_CSS_CLASS = "btn btn-sm btn-primary";
        private const string DEFAULT_PRICE_BUTTON_ICON_CSS_CLASS = "glyphicon glyphicon-euro";

        /// <summary>
        /// Renders a button for creating a price for the album.
        /// </summary>
        /// <param name="helper">
        /// The helper.
        /// </param>
        /// <param name="albumId">
        /// The album id.
        /// </param>
        /// <param name="text">
        /// The button text.
        /// </param>
        /// <param name="class">
        /// The button CSS class.
        /// </param>
        /// <param name="iconClass">
        /// The button icon CSS class.
        /// </param>
        /// <param name="href">
        /// The url to a page for creating a price for the album.
        /// </param>
        /// <returns>
        /// The button html.
        /// </returns>
        public static MvcHtmlString BtnCreateAlbumPrice(
            this HtmlHelper helper,
            int albumId,
            string text = null,
            string @class = null,
            string iconClass = null,
            string href = null)
        {
            if (string.IsNullOrWhiteSpace(@class))
            {
                @class = DEFAULT_PRICE_BUTTON_CSS_CLASS;
            }

            if (string.IsNullOrWhiteSpace(iconClass))
            {
                iconClass = DEFAULT_PRICE_BUTTON_ICON_CSS_CLASS;
            }

            if (string.IsNullOrWhiteSpace(href))
            {
                var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
                href = urlHelper.Action("Create", "AlbumPrice", new { albumId = albumId, area = "Management" });
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                text = "Установить ценник";
            }

            return helper.ButtonWithIcon(@class, iconClass, text, href);
        }

        /// <summary>
        /// Renders a button for editing the album price.
        /// </summary>
        /// <param name="helper">
        /// The helper.
        /// </param>
        /// <param name="albumId">
        /// The album id.
        /// </param>
        /// <param name="text">
        /// The button text.
        /// </param>
        /// <param name="class">
        /// The button CSS class.
        /// </param>
        /// <param name="iconClass">
        /// The button icon CSS class.
        /// </param>
        /// <param name="href">
        /// The url to a page for editing the album price.
        /// </param>
        /// <returns>
        /// The button html.
        /// </returns>
        public static MvcHtmlString BtnEditAlbumPrice(
            this HtmlHelper helper,
            int albumId,
            string text = null,
            string @class = null,
            string iconClass = null,
            string href = null)
        {
            if (string.IsNullOrWhiteSpace(@class))
            {
                @class = DEFAULT_PRICE_BUTTON_CSS_CLASS;
            }

            if (string.IsNullOrWhiteSpace(iconClass))
            {
                iconClass = DEFAULT_PRICE_BUTTON_ICON_CSS_CLASS;
            }

            if (string.IsNullOrWhiteSpace(href))
            {
                var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
                href = urlHelper.Action("Edit", "AlbumPrice", new { albumId = albumId, area = "Management" });
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                text = "Изменить ценник";
            }

            return helper.ButtonWithIcon(@class, iconClass, text, href);
        }

        /// <summary>
        /// Renders a button for creating a price for the track.
        /// </summary>
        /// <param name="helper">
        /// The helper.
        /// </param>
        /// <param name="trackId">
        /// The track id.
        /// </param>
        /// <param name="text">
        /// The button text.
        /// </param>
        /// <param name="class">
        /// The button CSS class.
        /// </param>
        /// <param name="iconClass">
        /// The button icon CSS class.
        /// </param>
        /// <param name="href">
        /// The url to a page for creating a price for the track.
        /// </param>
        /// <returns>
        /// The button html.
        /// </returns>
        public static MvcHtmlString BtnCreateTrackPrice(
            this HtmlHelper helper,
            int trackId,
            string text = null,
            string @class = null,
            string iconClass = null,
            string href = null)
        {
            if (string.IsNullOrWhiteSpace(@class))
            {
                @class = DEFAULT_PRICE_BUTTON_CSS_CLASS;
            }

            if (string.IsNullOrWhiteSpace(iconClass))
            {
                iconClass = DEFAULT_PRICE_BUTTON_ICON_CSS_CLASS;
            }

            if (string.IsNullOrWhiteSpace(href))
            {
                var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
                href = urlHelper.Action("Create", "TrackPrice", new { trackId = trackId, area = "Management" });
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                text = "Установить ценник";
            }

            return helper.ButtonWithIcon(@class, iconClass, text, href);
        }

        /// <summary>
        /// Renders a button for editing the track price.
        /// </summary>
        /// <param name="helper">
        /// The helper.
        /// </param>
        /// <param name="trackId">
        /// The track id.
        /// </param>
        /// <param name="text">
        /// The button text.
        /// </param>
        /// <param name="class">
        /// The button CSS class.
        /// </param>
        /// <param name="iconClass">
        /// The button icon CSS class.
        /// </param>
        /// <param name="href">
        /// The url to a page for editing the track price.
        /// </param>
        /// <returns>
        /// The button html.
        /// </returns>
        public static MvcHtmlString BtnEditTrackPrice(
            this HtmlHelper helper,
            int trackId,
            string text = null,
            string @class = null,
            string iconClass = null,
            string href = null)
        {
            if (string.IsNullOrWhiteSpace(@class))
            {
                @class = DEFAULT_PRICE_BUTTON_CSS_CLASS;
            }

            if (string.IsNullOrWhiteSpace(iconClass))
            {
                iconClass = DEFAULT_PRICE_BUTTON_ICON_CSS_CLASS;
            }

            if (string.IsNullOrWhiteSpace(href))
            {
                var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
                href = urlHelper.Action("Edit", "TrackPrice", new { trackId = trackId, area = "Management" });
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                text = "Изменить ценник";
            }

            return helper.ButtonWithIcon(@class, iconClass, text, href);
        }
    }
}