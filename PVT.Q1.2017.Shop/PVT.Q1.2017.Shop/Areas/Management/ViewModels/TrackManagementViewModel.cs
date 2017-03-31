namespace PVT.Q1._2017.Shop.Areas.Management.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    /// <summary>
    /// </summary>
    public class TrackManagementViewModel
    {
        /// <summary>
        ///     Gets or sets the album id.
        /// </summary>
        public int AlbumId { get; set; }

        /// <summary>
        ///     Gets or sets the album name.
        /// </summary>
        [Display(Name = "Альбом")]
        public string AlbumName { get; set; }

        /// <summary>
        ///     Gets or sets the artist id.
        /// </summary>
        public int ArtistId { get; set; }

        /// <summary>
        ///     Gets or sets the artist name.
        /// </summary>
        [Display(Name = "Исполнитель")]
        public string ArtistName { get; set; }

        /// <summary>
        ///     Gets or sets the duration.
        /// </summary>
        [Display(Name = "Продолжительность")]
        public TimeSpan? Duration { get; set; }

        /// <summary>
        /// Gets or sets the genre id.
        /// </summary>
        public int GenreId { get; set; }

        /// <summary>
        ///     Gets or sets the genre name.
        /// </summary>
        [Display(Name = "Жанр")]
        public string GenreName { get; set; }

        /// <summary>
        ///     Gets or sets the image.
        /// </summary>
        public byte[] Image { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        [DisplayName("Название")]
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the posted image.
        /// </summary>
        public HttpPostedFileBase PostedImage { get; set; }

        /// <summary>
        ///     Gets or sets the posted track file.
        /// </summary>
        [DisplayName("Файл")]
        public HttpPostedFileBase PostedTrackFile { get; set; }

        /// <summary>
        ///     Gets or sets the price.
        /// </summary>
        [Display(Name = "Стоимость")]
        public double Price { get; set; }

        /// <summary>
        ///     Gets or sets the release date.
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy} г.", ApplyFormatInEditMode = true)]
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        ///     Gets or sets the track file.
        /// </summary>
        public byte[] TrackFile { get; set; }
    }
}