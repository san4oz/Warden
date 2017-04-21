using Warden.Business.Entities;

namespace Warden.Business.Providers
{
    public interface ICategoryDataProvider : IDataProvider<Category>
    {
        Category GetByTitle(string title);
    }
}
