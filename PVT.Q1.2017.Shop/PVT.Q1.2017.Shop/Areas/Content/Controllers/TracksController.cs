namespace PVT.Q1._2017.Shop.Areas.Content.Controllers
{
    using System;
    using System.IO;
    using System.Net;
    using System.Web.Mvc;

    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.ViewModels;
    using global::Shop.DAL.Infrastruture;
    using Shop.Controllers;

    /// <summary>
    ///     The track controller
    /// </summary>
    public class TracksController : BaseController
    {
        private readonly ITrackService _trackService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TracksController" /> class.
        /// </summary>
        /// <param name="trackService">
        ///     The track service.
        /// </param>
        public TracksController(ITrackService trackService)
        {
            _trackService = trackService;
        }

        /// <summary>
        ///     Shows all albums where the specified track is exist.
        /// </summary>
        /// <param name="id">The track id.</param>
        /// <returns>
        ///     All albums where the specified track is exist.
        /// </returns>
        public virtual ActionResult AlbumsList(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("List");
            }

            TrackAlbumsListViewModel trackAlbumsViewModel;
            if (CurrentUser != null)
            {
                var currency = GetCurrentUserCurrency();
                var priceLevel = GetCurrentUserPriceLevel();
                trackAlbumsViewModel = _trackService.GetAlbumsList(id.Value, currency.Code, priceLevel, GetUserDataId());
            }
            else
            {
                trackAlbumsViewModel = _trackService.GetAlbumsList(id.Value);
            }

            if (trackAlbumsViewModel == null)
            {
                return HttpNotFound($"Трек с id = { id.Value } не найден");
            }

            return this.View(trackAlbumsViewModel);
        }

        /// <summary>
        ///     Shows all tracks.
        /// </summary>
        /// <returns>
        ///     All tracks view.
        /// </returns>
        public ActionResult List()
        {
            if (CurrentUser != null)
            {
                var currency = GetCurrentUserCurrency();
                var priceLevel = GetCurrentUserPriceLevel();
                return this.View(this._trackService.GetDetailedTracksList(currency.Code, priceLevel, GetUserDataId()));
            }

            return this.View(this._trackService.GetDetailedTracksList());
        }

        /// <summary>
        ///     Shows track info.
        /// </summary>
        /// <param name="id">The track id.</param>
        /// <returns>
        ///     Track info view.
        /// </returns>
        public virtual ActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("List");
            }

            TrackDetailsViewModel trackViewModel;
            if (CurrentUser != null)
            {
                var currency = GetCurrentUserCurrency();
                var priceLevel = GetCurrentUserPriceLevel();
                trackViewModel = _trackService.GetTrackDetails(id.Value, currency.Code, priceLevel, GetUserDataId());
            }
            else
            {
                trackViewModel = _trackService.GetTrackDetails(id.Value);
            }

            if (trackViewModel == null)
            {
                return HttpNotFound($"Трек с id = { id.Value } не найден");
            }

            return this.View(trackViewModel);
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <returns>
        /// </returns>
        public FileResult Download(int id)
        {
            string mp3Url = null;

            var factory = DependencyResolver.Current.GetService<IRepositoryFactory>();
            using (var trackRepository = factory.GetTrackRepository())
            {
                // string mp3Url = Track.GetMp3UrlByID(id);
                var track = trackRepository.GetById(id);

                byte[] data;
                using (var urlGrabber = new WebClient())
                {
                    data = urlGrabber.DownloadData(mp3Url);
                }

                using (var fileStream = new FileStream(string.Format("{0} - {1}.mp3", track.Artist, track.Name),
                                                       FileMode.Open))
                {

                    fileStream.Write(data, 0, data.Length);
                    fileStream.Seek(0, SeekOrigin.Begin);

                    return new FileStreamResult(fileStream, "audio/mpeg");
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <returns>
        /// </returns>
        public ActionResult GetAudio(int id)
        {
            var factory = DependencyResolver.Current.GetService<IRepositoryFactory>();
            using (var trackRepository = factory.GetTrackRepository())
            {
                var audioBytes = trackRepository.GetById(id).TrackFile;
                return this.File(audioBytes, "audio/mp3");
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <returns>
        /// </returns>
        public FileStreamResult GetTrackAsStream(int id)
        {
            var factory = DependencyResolver.Current.GetService<IRepositoryFactory>();
            byte[] song;
            string trackArtistName;
            string trackName;
            using (var trackRepository = factory.GetTrackRepository())
            {
                var track = trackRepository.GetById(id, p => p.Artist);
                song = track.TrackFile;
                trackArtistName = track.Artist.Name;
                trackName = track.Name;
            }

            if (song == null)
            {
                return null;
            }

            long fSize = song.Length;
            long startbyte = 0;
            var endbyte = fSize - 1;
            var statusCode = 200;
            if (this.Request.Headers["Range"] != null)
            {
                var range = this.Request.Headers["Range"].Split('=', '-');
                startbyte = Convert.ToInt64(range[1]);
                if (range.Length > 2 && range[2] != string.Empty)
                {
                    endbyte = Convert.ToInt64(range[2]);
                }

                if (startbyte != 0 || endbyte != fSize - 1 || range.Length > 2 && range[2] == string.Empty)
                {
                    statusCode = 206;
                }
            }

            var desSize = endbyte - startbyte + 1;
            this.Response.StatusCode = statusCode;
            this.Response.AddHeader("TrackArtist", trackArtistName);
            this.Response.AddHeader("TrackTitle", trackName);
            this.Response.AddHeader("Content-Accept", this.Response.ContentType);
            this.Response.AddHeader("Content-Length", desSize.ToString());
            this.Response.AddHeader("Content-Range", string.Format("bytes {0}-{1}/{2}", startbyte, endbyte, fSize));

            var stream = new MemoryStream(song, (int)startbyte, (int)desSize);
            return new FileStreamResult(stream, this.Response.ContentType);
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <returns>
        /// </returns>
        public FileResult Stream(int id)
        {
            var factory = DependencyResolver.Current.GetService<IRepositoryFactory>();
            using (var trackRepository = factory.GetTrackRepository())
            {
                var trackFile = trackRepository.GetById(id).TrackFile;
                return this.File(trackFile, "audio/mpeg", "test.mp3");
            }
        }
    }
}