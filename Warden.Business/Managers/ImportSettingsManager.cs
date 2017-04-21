using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Entities;
using Warden.Business.Import;
using Warden.Business.Providers;

namespace Warden.Business.Managers
{
    public class ImportSettingsManager : EntityManager<TransactionImportSettings, IImportSettingsProvider>
    {
        public ImportSettingsManager(IImportSettingsProvider provider) : base(provider)
        {
        }

        public TransactionImportSettings GetByPayerId(string payerId) => Provider.GetByPayerId(payerId);

        public void UpdateTaskStatus(TransactionImportSettings settings, ImportTaskStatus newStatus)
        {
            settings.Status = newStatus;
            Provider.Save(settings);
        }
    }
}
