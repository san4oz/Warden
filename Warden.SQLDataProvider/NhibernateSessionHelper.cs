using NHibernate;
using NHibernate.Cfg;

namespace Warden.SQLDataProvider
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
