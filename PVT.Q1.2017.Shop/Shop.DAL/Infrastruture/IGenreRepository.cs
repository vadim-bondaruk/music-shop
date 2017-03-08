//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Track.cs" company="PVT.Q1.2017">
//    PVT.Q1.2017
//  </copyright>
//  <summary>
//    The track.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------

namespace Shop.DAL.Infrastruture
{
    using Shop.Common.Models;
    using Shop.Infrastructure.Repositories;

    /// <summary>
    ///     The genre repository.
    /// </summary>
    public interface IGenreRepository : IRepository<Genre>
    {
    }
}