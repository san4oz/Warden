using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Contracts.Providers;
using Warden.Business.Entities;

namespace Warden.Business.Contracts.Managers
{
    public abstract class EntityManager<T, TProvider>
        where T : Entity, new()
        where TProvider : IDataProvider<T>        
    {
        protected TProvider Provider;

        public EntityManager(TProvider provider) => Provider = provider;

        public virtual void Save(T entity) => Provider.Save(entity);

        public virtual void Update(T entity) => Provider.Update(entity);
        
        public virtual List<T> All() => Provider.All();

        public virtual void Delete(Guid id) => Provider.Delete(id);

        public virtual T Get(Guid id) => Provider.Get(id);
    }
}
