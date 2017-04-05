using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Entities;

namespace Warden.Business.Contracts.Providers
{
    public interface ICategoryDataProvider : IDataProvider<Category>
    {
        Category GetByTitle(string title);
    }
}
