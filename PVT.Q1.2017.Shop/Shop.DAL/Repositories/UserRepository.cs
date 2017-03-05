namespace Shop.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using Common.Models;
    using Shop.DAL.Context;
    using Infrastructure.Repositories;

    /// <summary>
    /// 
    /// </summary>
    public class UserRepository : Repository<User>
    {
        public UserRepository(UserContext userContext) : base(userContext)
        {
        }
    }
}
