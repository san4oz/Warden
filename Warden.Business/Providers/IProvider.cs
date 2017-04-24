using System;
using System.Collections.Generic;
using Warden.Business.Entities;

namespace Warden.Business.Providers
{
    public interface IProvider<T>
        where T : Entity, new()
    {
        void Save(T entity);

        void Update(T entity);

        List<T> All();

        void Delete(Guid id);

        T Get(Guid id);
    }
}
