using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Autofac;
using Autofac.Builder;
using Autofac.Integration.Mvc;
using Warden.Business.Contracts.Providers;
using Warden.DataProvider.DataProviders;
using Warden.Search;

namespace Warden.Mvc.App_Start
{
    public static class AutofacConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(Startup).Assembly);
            RegisterTypes(builder);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
        }

        private static void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<ExternalDataProvider.ExternalApi>().As<IExternalApi>();
            builder.RegisterType<TransactionDataProvider>().As<ITransactionDataProvider>();
            builder.RegisterType<SearchManager>().As<ISearchManager>();
        }
    }
}
