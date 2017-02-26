﻿using System.Linq;
using Ninject.Modules;
using Shop.Common.Models;
using Shop.DAL.Repositories;
using Shop.Infrastructure.Repositories;
using Shop.DAL.Context;

namespace Shop.DAL
{
    public class DalNinjectModule: NinjectModule
    {
        public override void Load()
        {
            Bind<IRepository<Cart>>().To<CartRepository>().WithConstructorArgument("dbContext", new ShopContext());
        }
    }
}