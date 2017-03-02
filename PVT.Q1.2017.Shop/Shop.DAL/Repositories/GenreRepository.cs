namespace Shop.DAL.Repositories
{
    using System.Data.Entity;
    using Common.Models;
    using Infrastruture;

    /// <summary>
    /// The genre repository
    /// </summary>
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GenreRepository"/> class.
        /// </summary>
        /// <param name="dbContext">
        /// The db context.
        /// </param>
        public GenreRepository(DbContext dbContext) : base(dbContext)
        {
        }

        #endregion //Constructors
    }
}