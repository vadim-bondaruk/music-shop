using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Ship.Infrastructure.Models;

namespace Ship.Infrastructure.Repositories
{
    public interface IRepository<TEntity> where TEntity: BaseEntity, new()
    {
        TEntity GetById(int id);

        ICollection<TEntity> GetAll();

        ICollection<TEntity> GetAll(Expression<Func<TEntity, bool>> filter);

        void AddOrUpdate(TEntity model);

        void Delete(int id);

        void Delete(TEntity model);
    }
}