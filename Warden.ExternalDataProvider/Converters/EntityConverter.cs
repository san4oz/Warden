using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Entities;
using Warden.ExternalDataProvider.Entities;

namespace Warden.ExternalDataProvider
{
    public static class EntityConverter
    {
        public static Transaction ToWardenTransaction(this TransactionData source)
        {
            if (source == null)
                return null;

            return new Transaction()
            {
                PayerId = source.PayerId,
                ReceiverId = source.ReceiverId,
                Price = source.Price,
                Date = source.Date,
                Keywords = source.Keywords,
                ExternalId = source.ExternalId
            };
        }
    }
}
