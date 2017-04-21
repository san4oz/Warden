using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using Warden.Business.Api;
using Warden.Business.Import;
using Warden.Business.Managers;
using Warden.Business.Import.Pipeline;
using Warden.Business.Providers;
using Warden.DataProvider.DataProviders;
using Warden.ExternalDataProvider;
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
            
            RegisterDataProviders(builder);
            builder.RegisterType<ExternalApi>().As<IExternalApi>();
            RegisterManagers(builder);

            builder.RegisterType<TransactionImportTask>().As<TransactionImportTask>().SingleInstance();
            RegisterPipeline(builder);
        }

        private static void RegisterManagers(ContainerBuilder builder)
        {
            builder.RegisterType<CategoryManager>().As<CategoryManager>();
            builder.RegisterType<PayerManager>().As<PayerManager>();
            builder.RegisterType<TransactionManager>().As<TransactionManager>();
            builder.RegisterType<SearchManager>().As<ISearchManager>();
            builder.RegisterType<AnalysisManager>().As<AnalysisManager>();
        }

        private static void RegisterDataProviders(ContainerBuilder builder)
        {
            builder.RegisterType<TransactionDataProvider>().As<ITransactionDataProvider>();
            builder.RegisterType<PayerDataProvider>().As<IPayerDataProvider>();
            builder.RegisterType<TransactionTaskConfigurationDataProvider>().As<ITransactionImportConfigurationDataProvider>();
            builder.RegisterType<CategoryDataProvider>().As<ICategoryDataProvider>();
            builder.RegisterType<KeywordDataProvider>().As<IKeywordDataProvider>();
        }

        private static void RegisterPipeline(ContainerBuilder builder)
        {
            builder.RegisterType<TransactionImportPipeline>().As<TransactionImportPipeline>();
        }
    }
}
