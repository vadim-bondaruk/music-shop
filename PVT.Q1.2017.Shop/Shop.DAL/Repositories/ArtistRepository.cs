namespace Shop.DAL.Repositories
{
    using System.Data.Entity;
    using Common.Models;
    using Infrastruture;

    /// <summary>
    /// The artist repsitory.
    /// </summary>
    public class ArtistRepository : BaseRepository<Artist>, IArtistRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistRepository"/> class.
        /// </summary>
        /// <param name="dbContext">
        /// The db context.
        /// </param>
        public ArtistRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}