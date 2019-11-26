using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nhibernate_Fluent.Base.BaseRepository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {

        public void delete(T entities)
        {
            ISession session = SessionFactory.SessionFactory.getCurrentSession();
            session.Delete(entities);


        }
        public void insert(T entities)
        {

            ISession session = SessionFactory.SessionFactory.getCurrentSession();

            session.Save(entities);

        }
        public void update(T entities)
        {

            ISession session = SessionFactory.SessionFactory.getCurrentSession();

            session.Update(entities);

        }
        public IList<T> getAll()
        {
            IList<T> lists;
            ISession session = SessionFactory.SessionFactory.getCurrentSession();
            lists = session.QueryOver<T>().List<T>();
            return lists;
        }
        public IQueryOver<T, T> getQueryable()
        {
            ISession session = SessionFactory.SessionFactory.getCurrentSession();
            return session.QueryOver<T>();
        }

        public T getById(long id)
        {
            ISession session = SessionFactory.SessionFactory.getCurrentSession();
            return session.Get<T>(id);
        }
    }
}
