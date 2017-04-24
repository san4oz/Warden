using Warden.Business.Entities;

namespace Warden.Business.Providers
{
    public interface IImportSettingsProvider : IProvider<TransactionImportSettings>
    {
        TransactionImportSettings GetByPayerId(string payerId);
    }
}
