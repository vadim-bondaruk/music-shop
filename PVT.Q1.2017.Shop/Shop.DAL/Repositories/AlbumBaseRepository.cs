namespace Shop.DAL.Repositories
{
    using System.Data.Entity;
    using Common.Models;
    using Infrastruture;

    /// <summary>
    /// The album repository
    /// </summary>
    public class AlbumBaseRepository : BaseRepository<Album>, IAlbumRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumBaseRepository"/> class.
        /// </summary>
        /// <param name="dbContext">
        /// The db context.
        /// </param>
        public AlbumBaseRepository(DbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Adds the specified <paramref name="album"/> into Db.
        /// </summary>
        /// <param name="album">
        /// The album to add.
        /// </param>
        protected override void Add(Album album)
        {
            EntityState artistEntryState;

            if (album.ArtistId == null && album.Artist != null)
            {
                album.ArtistId = album.Artist.Id;
            }

            // Detaching the navigation properties in case if they are attached to prevent unexpected behaviour of the DbContext.
            // The AlbumBaseRepository should be SOLID, should only add information about album! Not about artist!
            this.DetachNavigationProperty(album.Artist, out artistEntryState);

            album.Artist = null;

            // adding the album into Db.
            base.Add(album);
        }
    }
}