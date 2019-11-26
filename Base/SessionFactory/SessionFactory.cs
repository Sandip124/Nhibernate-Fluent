using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.AspNetCore.Http;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Nhibernate_Fluent.Base.SessionFactory
{
    public class SessionFactory
    {
        private static readonly ISessionFactory sessionFactory;
        private static object lockObject = new Object();
        private const string CurrentSessionKey = "nhibernate.current_session";
        public static IHttpContextAccessor httpContextAccessor { get; set; }

        static SessionFactory()
        {
            lock (lockObject)
            {
                if (sessionFactory == null)
                {
                    Configuration configuration = new Configuration().Configure("hibernate.cfg.xml");
                    sessionFactory = Fluently.Configure(configuration)
                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Program>())
                        .BuildSessionFactory();
                }
            }
        }

        public static NHibernate.ISession getCurrentSession()
        {
            var context = httpContextAccessor.HttpContext;
            var currentSession = context.Items[CurrentSessionKey] as NHibernate.ISession;

            if (currentSession == null)
            {
                currentSession = sessionFactory.OpenSession();
                context.Items[CurrentSessionKey] = currentSession;
            }

            return currentSession;
        }

        public static void CloseSession()
        {
            var context = httpContextAccessor.HttpContext;
            var currentSession = context.Items[CurrentSessionKey] as NHibernate.ISession;

            if (currentSession == null)
            {
                // No current session
                return;
            }

            currentSession.Close();
            context.Items.Remove(CurrentSessionKey);
        }

        public static void flushAndDisposeSession()
        {
            NHibernate.ISession currentSession = getCurrentSession();
            currentSession.Flush();

            //currentSession.Close();
            currentSession.Dispose();
            var context = httpContextAccessor.HttpContext;
            context.Items.Remove(CurrentSessionKey);

        }

        public static void disposeSession()
        {
            NHibernate.ISession currentSession = getCurrentSession();
            currentSession.Dispose();
        }

        public static void closeSessionFactory()
        {
            if (sessionFactory != null) { sessionFactory.Close(); }
        }
    }
}
