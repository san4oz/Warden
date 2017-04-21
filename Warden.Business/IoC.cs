using System.Web.Mvc;

namespace Warden.Business
{
    public static class IoC
    {
        private static IDependencyResolver resolver;

        public static void Init(IDependencyResolver resolver)
        {
            IoC.resolver = resolver;
        }

        public static T Resolve<T>() => 
            (T)resolver.GetService(typeof(T));
    }
}
