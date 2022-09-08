using Harbinton.API.Application.Dto.Transaction;
using Harbinton.API.Application.ResponseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harbinton.API.Application.Helper.Interface
{
    public interface ITransactionCacheHelper
    {
        Task<TransactionDto> GetStatus(string TransactionRef);
    }
}
