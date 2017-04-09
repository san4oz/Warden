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
using Warden.Business.Entities;
using Warden.Business.Contracts.Scheduler;
using Warden.ExternalDataProvider;
using Warden.Business.Pipeline;
using Warden.Business.Contracts.Pipeline;
using Warden.Business.Import;

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
            
            RegisterDataProviders(builder);

            builder.RegisterType<ExternalApi>().As<IExternalApi>();
            builder.RegisterType<SearchManager>().As<ISearchManager>();
            builder.RegisterType<TransactionImportTask>().As<ITransactionImportTask>();
            RegisterPipeline(builder);
        }

        private static void RegisterDataProviders(ContainerBuilder builder)
        {
            builder.RegisterType<TransactionDataProvider>().As<ITransactionDataProvider>();
            builder.RegisterType<PayerDataProvider>().As<IPayerDataProvider>();
            builder.RegisterType<TransactionTaskConfigurationDataProvider>().As<ITransactionImportConfigurationDataProvider>();
            builder.RegisterType<CategoryDataProvider>().As<ICategoryDataProvider>();
        }

        private static void RegisterPipeline(ContainerBuilder builder)
        {
            builder.RegisterType<TransactionImportPipeline>().As<ITransactionImportPipeline>().SingleInstance();
        }
    }
}
