namespace Shop.DAL.Repositories
{
    using System.Data.Entity;
    using Common.Models;
    using Infrastruture;

    /// <summary>
    /// The artist repsitory.
    /// </summary>
    public class ArtistRepository : Repository<Artist>, IArtistRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistRepository"/> class.
        /// </summary>
        /// <param name="dbContext">
        /// The db context.
        /// </param>
        public ArtistRepository(DbContext dbContext) : base(dbContext)
        {
        }

        #endregion //Constructors
    }
}