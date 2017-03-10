using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Contracts.Providers;
using Warden.Business.Entities;

namespace Warden.DataProvider.DataProviders
{
    public class BaseDataProvider : IDataProvider<Entity>
    {
        public virtual List<Entity> All()
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual void Save(Entity entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(Entity entity)
        {
            throw new NotImplementedException();
        }
    }
}
