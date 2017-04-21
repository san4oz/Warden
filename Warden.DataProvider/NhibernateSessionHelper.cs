using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;

namespace Warden.DataProvider
{
    public class NhibernateSessionHelper
    {
        private static readonly ISessionFactory sessionFactory;

        static NhibernateSessionHelper()
        {
            var configuration = new Configuration().Configure();
            sessionFactory = configuration.BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            var session = sessionFactory.OpenSession();
            return session;
        }
    }
}
