using Warden.Business.Entities;

namespace Warden.Business.Providers
{
    public interface IPayerProvider : IProvider<Payer>
    {
        Payer Get(string payerId);
    }
}
