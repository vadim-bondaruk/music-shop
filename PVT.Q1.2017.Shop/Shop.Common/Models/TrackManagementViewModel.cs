namespace Shop.Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    /// <summary>
    /// </summary>
    public class TrackManagementViewModel
    {
        /// <summary>
        ///     Gets or sets the artist.
        /// </summary>
        [DisplayName("Исполнитель")]
        public Artist Artist { get; set; }

        /// <summary>
        ///     Gets or sets the duration.
        /// </summary>
        [DisplayName("Продолжительность")]
        public TimeSpan? Duration { get; set; }

        /// <summary>
        ///     Gets or sets the genre.
        /// </summary>
        /*public Genre Genre { get; set; }*/

        /// <summary>
        ///     Gets or sets the genre id.
        /// </summary>
        public int GenreId { get; set; }

        /// <summary>
        ///     Gets or sets the genres.
        /// </summary>
        public ICollection<Genre> Genres { get; set; }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

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
        [DisplayName("Стоимость")]
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        public RatingViewModel Rating { get; set; }

        /// <summary>
        ///     Gets or sets the release date.
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy} г.", ApplyFormatInEditMode = true)]
        [DisplayName("Дата выхода")]
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        ///     Gets or sets the track file.
        /// </summary>
        public byte[] TrackFile { get; set; }
    }
}