﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.Business.Entities;
using Warden.Business.Entities.ExternalProvider;

namespace Warden.Business.Contracts.Providers
{
    public interface IExternalDataProvider
    {
        List<Transaction> GetTransactions(TransactionRequest request);
    }
}
