namespace Shop.BLL.Helpers
{
    using System.Collections.Generic;
    using Common.Models;
    using Infrastructure.Repositories;

    /// <summary>
    /// The track helper.
    /// </summary>
    public static class TrackHelper
    {
        /// <summary>
        /// Fills information about artists for the specified <paramref name="tracks"/> if possible.
        /// </summary>
        /// <param name="tracks">
        /// The tracks.
        /// </param>
        /// <param name="repositoryFactory">
        /// The repository factory.
        /// </param>
        public static void FillArtistInfo(IRepositoryFactory repositoryFactory, ICollection<Track> tracks)
        {
            using (var repository = repositoryFactory.CreateRepository<Artist>())
            {
                foreach (var track in tracks)
                {
                    FillArtistInfo(track, repository);
                }
            }
        }

        /// <summary>
        /// Fills information about artist for the specified <paramref name="track"/> if possible.
        /// </summary>
        /// <param name="track">
        /// The track.
        /// </param>
        /// <param name="repositoryFactory">
        /// The repository factory.
        /// </param>
        public static void FillArtistInfo(IRepositoryFactory repositoryFactory, Track track)
        {
            using (var repository = repositoryFactory.CreateRepository<Artist>())
            {
                FillArtistInfo(track, repository);
            }
        }

        /// <summary>
        /// Fills information about artist for the specified <paramref name="track"/> if possible.
        /// </summary>
        /// <param name="track">
        /// The track.
        /// </param>
        /// <param name="repository">
        /// The artists repository.
        /// </param>
        public static void FillArtistInfo(Track track, IDisposableRepository<Artist> repository)
        {
            // if information about artist is not filled but track has ArtistId
            if (track.ArtistId != null && track.Artist == null)
            {
                track.Artist = repository.GetById(track.ArtistId.Value);
            }
        }
    }
}