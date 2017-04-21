using Warden.Business.Entities;

namespace Warden.Business.Providers
{
    public interface ITransactionImportConfigurationDataProvider : IDataProvider<TransactionImportTaskConfiguration>
    {
        TransactionImportTaskConfiguration GetForPayer(string payerId);

        TransactionImportTaskConfiguration GetDefault(string payerId);
    }
}
