namespace PVT.Q1._2017.Shop.Areas.Content.Controllers
{
    using System;
    using System.IO;
    using System.Net;
    using System.Web.Mvc;

    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.DAL.Infrastruture;

    /// <summary>
    ///     The track controller
    /// </summary>
    public class TracksController : Controller
    {
        /// <summary>
        ///     The track service.
        /// </summary>
        private readonly ITrackRepository trackRepository;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TracksController" /> class.
        /// </summary>
        /// <param name="trackService">
        ///     The track service.
        /// </param>
        /// <param name="trackRepository"></param>
        public TracksController(ITrackService trackService, ITrackRepository trackRepository)
        {
            this.trackRepository = trackRepository;
            this.TrackService = trackService;
        }

        /// <summary>
        ///     Gets or sets the _track service.
        /// </summary>
        public ITrackService TrackService { get; set; }

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

            return null; // this.View(this._trackService.GetAlbumsList(new Track { Id = id }));
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

            return this.View(this.TrackService.GetTrackDetails(id.Value));
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

            // string mp3Url = Track.GetMp3UrlByID(id);
            var track = this.trackRepository.GetById(id);
            var urlGrabber = new WebClient();
            var data = urlGrabber.DownloadData(mp3Url);
            var fileStream = new FileStream(string.Format("{0} - {1}.mp3", track.Artist, track.Name), FileMode.Open);

            fileStream.Write(data, 0, data.Length);
            fileStream.Seek(0, SeekOrigin.Begin);

            return new FileStreamResult(fileStream, "audio/mpeg");

            // return (new FileContentResult(data, "audio/mpeg"));
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
            var audioBytes = this.trackRepository.GetById(id).TrackFile;
            return this.File(audioBytes, "audio/mp3");
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
            var track = this.trackRepository.GetById(id, p => p.Artist);
            var song = track.TrackFile;
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
            this.Response.AddHeader("TrackArtist", track.Artist.Name);
            this.Response.AddHeader("TrackTitle", track.Name);
            this.Response.AddHeader("Content-Accept", this.Response.ContentType);
            this.Response.AddHeader("Content-Length", desSize.ToString());
            this.Response.AddHeader("Content-Range", string.Format("bytes {0}-{1}/{2}", startbyte, endbyte, fSize));

            var stream = new MemoryStream(song, (int)startbyte, (int)desSize);
            return new FileStreamResult(stream, this.Response.ContentType);
        }

        /// <summary>
        ///     Shows all tracks.
        /// </summary>
        /// <returns>
        ///     All tracks view.
        /// </returns>
        public ActionResult List()
        {
            var tracks = this.TrackService.GetTrackDetailsViewModels();
            return this.View(tracks);
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public ActionResult TrackList(int id)
        {
            var tracks = this.TrackService.GetArtistTracksList(id);
            return this.View("List", tracks);
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
            var trackFile = this.trackRepository.GetById(id).TrackFile;
            return this.File(trackFile, "audio/mpeg", "test.mp3");
        }
    }
}