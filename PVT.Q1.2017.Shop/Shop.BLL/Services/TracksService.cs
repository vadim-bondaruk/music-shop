using Shop.Common.Models;
using Shop.Infrastructure.Repositories;
using Shop.Infrastructure.Services;

namespace Shop.BLL.Services
{
    public class TracksService : IService<Track>
    {
        public TracksService(IRepository<Track> tracksRepository, IRepository<Artist> artistsRepository, IRepository<Album> albumsRepository,
                             IRepository<TrackPrice> trackRepository, IRepository<Feedback> feedbacksRepository, IRepository<Vote> votesRepository)
        {
        }
    }
}