using Shop.Common.Models;
using Shop.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DAL.Infrastruture
{
    public interface ICountryRepository : IRepository<Country>
    {
    }
}
