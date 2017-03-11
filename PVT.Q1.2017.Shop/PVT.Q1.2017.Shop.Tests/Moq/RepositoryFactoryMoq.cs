namespace PVT.Q1._2017.Shop.Tests.Moq
{
    using global::Shop.DAL.Infrastruture;

    public class RepositoryFactoryMoq : IRepositoryFactory
    {
        public IAlbumRepository GetAlbumRepository()
        {
            return new AlbumRepositoryMoq().Repository;
        }

        public IArtistRepository GetArtistRepository()
        {
            return new ArtistRepositoryMoq().Repository;
        }

        public ITrackRepository GetTrackRepository()
        {
            return new TrackRepositoryMoq().Repository;
        }

        public ICurrencyRepository GetCurrencyRepository()
        {
            return new CurrencyRepositoryMoq().Repository;
        }

        public IAlbumPriceRepository GetAlbumPriceRepository()
        {
            return new AlbumPriceRepositoryMoq().Repository;
        }

        public ICurrencyRateRepository GetCurrencyRateRepository()
        {
            return new CurrencyRateRepositorMoq().Repository;
        }

        public IFeedbackRepository GetFeedbackRepository()
        {
            return new FeedbackRepositoryMoq().Repository;
        }

        public IGenreRepository GetGenreRepository()
        {
            return new GenreRepositoryMoq().Repository;
        }

        public IPriceLevelRepository GetPriceLevelRepository()
        {
            return new PriceLevelRepositoryMoq().Repository;
        }

        public ITrackPriceRepository GetTrackPriceRepository()
        {
            return new TrackPriceRepositoryMoq().Repository;
        }

        public IUserRepository GetUserRepository()
        {
            return new UserRepositoryMoq().Repository;
        }

        public IVoteRepository GetVoteRepository()
        {
            return new VoteRepositoryMoq().Repository;
        }

        public IAlbumTrackRelationRepository GetAlbumTrackRelationRepository()
        {
            return new AlbumTrackRelationRepositoryMoq().Repository;
        }
    }
}