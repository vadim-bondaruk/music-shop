namespace Shop.DAL.Context
{
    using System.Data.Entity;

    using Common.Models;
    using Migrations;
    using Migrations.Configurations;

    /// <summary>
    /// Music shop Db
    /// </summary>
    public class ShopContext : DbContext
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ShopContext"/> class.
        /// </summary>
        public ShopContext() : this("ShopConnection")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShopContext"/> class.
        /// </summary>
        /// <param name="connectionStringOrName">
        /// The connection string or Db name.
        /// </param>
        public ShopContext(string connectionStringOrName) : base(connectionStringOrName)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ShopContext, Configuration>());
        }

        #endregion //Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the tracks.
        /// </summary>
        public DbSet<Track> Tracks { get; set; }

        /// <summary>
        /// Gets or sets the artists.
        /// </summary>
        public DbSet<Artist> Artists { get; set; }

        /// <summary>
        /// Gets or sets the albums.
        /// </summary>
        public DbSet<Album> Albums { get; set; }

        /// <summary>
        /// Gets or sets the track prices.
        /// </summary>
        public DbSet<TrackPrice> TrackPrices { get; set; }

        /// <summary>
        /// Gets or sets the album prices.
        /// </summary>
        public DbSet<AlbumPrice> AlbumPrices { get; set; }

        /// <summary>
        /// Gets or sets the currency rates.
        /// </summary>
        public DbSet<CurrencyRate> CurrencyRates { get; set; }

        /// <summary>
        /// Gets or sets the feedbacks.
        /// </summary>
        public DbSet<Feedback> Feedbacks { get; set; }

        /// <summary>
        /// Gets or sets the votes.
        /// </summary>
        public DbSet<Vote> Votes { get; set; }

        /// <summary>
        /// Gets or sets the genres.
        /// </summary>
        public DbSet<Genre> Genres { get; set; }

        #endregion //Properties

        #region Protected Methods

        /// <summary>
        /// The Db configuration.
        /// </summary>
        /// <param name="modelBuilder">
        /// The model builder.
        /// </param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TrackConfiguration())
                        .Add(new ArtistConfiguration())
                        .Add(new AlbumConfiguration())
                        .Add(new FeedbackConfiguration())
                        .Add(new VoteConfiguration())
                        .Add(new GenreConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        #endregion //Protected Methods
    }
}