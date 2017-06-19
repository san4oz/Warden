using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using Warden.Business.Api.Payer;
using Warden.Business.Import;
using Warden.Business.Import.Processor;
using Warden.Business.Import.Processor.Steps;
using Warden.Business.Managers;
using Warden.Business.Providers;
using Warden.Search;
using Warden.SQLDataProvider.DataProviders;
using Warden.TransactionSource;

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
            builder.RegisterType<APITransactionProvider>().As<IAPITransactionProvider>();
            RegisterManagers(builder);
            RegisterApis(builder);

            RegisterTransactionImportTask(builder);
            RegisterImportProcessor(builder);
        }

        private static void RegisterApis(ContainerBuilder builder)
        {
            builder.RegisterType<PayerApi>().As<PayerApi>();
        }

        private static void RegisterManagers(ContainerBuilder builder)
        {
            builder.RegisterType<CategoryManager>().As<CategoryManager>();
            builder.RegisterType<PayerManager>().As<PayerManager>();
            builder.RegisterType<TransactionManager>().As<TransactionManager>();
            builder.RegisterType<SearchManager>().As<ISearchManager>();
            builder.RegisterType<AnalysisManager>().As<AnalysisManager>();
            builder.RegisterType<ImportSettingsManager>().As<ImportSettingsManager>();
            builder.RegisterType<PostManager>().As<PostManager>();
        }

        private static void RegisterDataProviders(ContainerBuilder builder)
        {
            builder.RegisterType<TransactionDataProvider>().As<ITransactionProvider>();
            builder.RegisterType<PayerDataProvider>().As<IPayerProvider>();
            builder.RegisterType<ImportSettingsProvider>().As<IImportSettingsProvider>();
            builder.RegisterType<CategoryDataProvider>().As<ICategoryProvider>();
            builder.RegisterType<KeywordDataProvider>().As<IKeywordProvider>();
            builder.RegisterType<PostProvider>().As<IPostProvider>();
        }

        private static void RegisterImportProcessor(ContainerBuilder builder)
        {
            builder.RegisterType<TransactionImportProcessor>().As<TransactionImportProcessor>();

            builder.RegisterType<TransactionRetreivingStep>().As<ITransactionImportStep>();
            builder.RegisterType<TransactionFilteringStep>().As<ITransactionImportStep>();
            builder.RegisterType<TransactionProcessingStep>().As<ITransactionImportStep>();
            builder.RegisterType<TransactionCreatingStep>().As<ITransactionImportStep>();
            builder.RegisterType<TransactionIndexingStep>().As<ITransactionImportStep>();
        }

        private static void RegisterTransactionImportTask(ContainerBuilder builder)
        {
            builder.RegisterType<TransactionImportTask>()
                  .Named<ITransactionImportTask>("transactionImportTask");

            builder.RegisterDecorator<ITransactionImportTask>(
                (c, inner) => new TransactionImportTaskLogger(inner), fromKey: "transactionImportTask");
        }
    }
}
