﻿namespace Ship.Infrastructure.Services
{
    using Ship.Infrastructure.Models;

    /// <summary>
    /// Base service contract (dummy)
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IService<TEntity> where TEntity : BaseEntity, new()
    {
    }
}