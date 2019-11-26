using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nhibernate_Fluent.Base.BaseRepository
{
    public interface IBaseRepository<T> where T : class
    {
        void delete(T entities);
        void insert(T entities);
        void update(T entities);
        IList<T> getAll();
        IQueryOver<T, T> getQueryable();
        T getById(long id);
    }
}
