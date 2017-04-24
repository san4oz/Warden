using Warden.Business.Entities;

namespace Warden.Business.Providers
{
    public interface IPayerDataProvider : IDataProvider<Payer>
    {
        Payer Get(string payerId);
    }
}
