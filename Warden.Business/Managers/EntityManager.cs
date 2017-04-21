using System;
using System.Collections.Generic;
using Warden.Business.Entities;
using Warden.Business.Providers;

namespace Warden.Business.Managers
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
