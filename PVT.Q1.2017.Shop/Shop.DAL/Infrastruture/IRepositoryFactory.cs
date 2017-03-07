namespace Shop.DAL.Infrastruture
{
    public interface IRepositoryFactory
    {
        IAlbumPriceRepository GetAlbumPriceRepository();

        //// read!!! https://github.com/ninject/Ninject.Extensions.Factory/wiki/Factory-interface
    }
}