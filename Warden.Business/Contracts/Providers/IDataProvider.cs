using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Entities;

namespace Warden.Business.Contracts.Providers
{
    public interface IDataProvider<T>
        where T : Entity, new()
    {
        void Save(T entity);

        void Update(T entity);

        List<T> All();

        void Delete(Guid id);
    }
}
