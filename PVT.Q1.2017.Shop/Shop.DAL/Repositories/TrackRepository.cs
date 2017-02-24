﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TrackRepository.cs" company="PVT.Q1.2017">
//   PVT.Q1.2017
// </copyright>
// <summary>
//   Defines the TrackRepository type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Shop.DAL.Repositories
{
    #region

    using System;

    using Shop.Infrastructure.Models;

    #endregion

    /// <summary>
    /// </summary>
    public class TrackRepository
    {
        /// <summary>
        /// </summary>
        /// <exception cref="System.NotImplementedException" />
        /// <returns>
        /// </returns>
        public object GetAlbumList()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <exception cref="System.NotImplementedException" />
        /// <returns>
        /// </returns>
        public object GetAlbumList(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <exception cref="System.NotImplementedException" />
        /// <returns>
        /// </returns>
        public object GetArtistList()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// </returns>
        public ITrack GetTrack(int id)
        {
            return null;
        }

        /// <summary>
        /// </summary>
        /// <exception cref="System.NotImplementedException" />
        /// <returns>
        /// </returns>
        public object GetTrackList()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <param name="artistId">The artist id.</param>
        /// <exception cref="System.NotImplementedException" />
        /// <returns>
        /// </returns>
        public object GetTrackList(int? albumId, int? artistId)
        {
            throw new NotImplementedException();
        }
    }
}