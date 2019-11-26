using Nhibernate_Fluent.Base.BaseRepository;
using Nhibernate_Fluent.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nhibernate_Fluent.Repository
{
    public interface IProductRepository : IBaseRepository<Product> 
    {
    }
}
