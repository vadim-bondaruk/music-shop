namespace Shop.BLL.Services.Infrastructure
{
    using Common.Models;
    using DAL.Repositories.Infrastruture;
    using Shop.Infrastructure;
    using Shop.Infrastructure.Validators;

    /// <summary>
    /// The genre service.
    /// </summary>
    public class GenreService : Service<IGenreRepository, Genre>, IGenreService
    {
        public GenreService(IFactory factory, IValidator<Genre> validator) : base(factory, validator)
        {
        }

        public void AddGenre(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}