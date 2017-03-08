namespace Shop.DAL.Infrastruture
{
    using Shop.Common.Models;
    using Shop.Infrastructure.Repositories;

    /// <summary>
    ///     The album repository
    /// </summary>
    public interface IAlbumRepository : IRepository<Album>
    {
    }
}