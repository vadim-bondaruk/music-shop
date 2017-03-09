using Shop.DAL.Infrastruture;

namespace Shop.DAL.Repositories
{
    using System.Data.Entity;
    using Common.Models;

    /// <summary>
    /// The genre repository
    /// </summary>
    public class GenreBaseRepository : BaseRepository<Genre>, IGenreRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GenreBaseRepository"/> class.
        /// </summary>
        /// <param name="dbContext">
        /// The db context.
        /// </param>
        public GenreBaseRepository(DbContext dbContext) : base(dbContext)
        {
        }

        #endregion //Constructors
    }
}