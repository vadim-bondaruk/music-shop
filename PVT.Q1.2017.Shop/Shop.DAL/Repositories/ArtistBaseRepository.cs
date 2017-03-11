namespace Shop.DAL.Repositories
{
    using System.Data.Entity;
    using Common.Models;
    using Infrastruture;

    /// <summary>
    /// The artist repsitory.
    /// </summary>
    public class ArtistBaseRepository : BaseRepository<Artist>, IArtistRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistBaseRepository"/> class.
        /// </summary>
        /// <param name="dbContext">
        /// The db context.
        /// </param>
        public ArtistBaseRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}