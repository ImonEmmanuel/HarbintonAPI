using Harbinton.API.Application.Contracts.Persitence;
using Harbinton.API.Application.Dto.Transaction;
using Harbinton.API.Application.Helper.Interface;
using Harbinton.API.Application.ResponseData;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harbinton.API.Application.Helper
{
    public class TransactionCacheHelper : ITransactionCacheHelper
    {
        private readonly IMemoryCache _cache;
        private readonly ITransactionRepository _transRepo;
        public TransactionCacheHelper(IMemoryCache cache, ITransactionRepository transRepo)
        {
            _cache = cache;
            _transRepo = transRepo;

        }

        public async Task<TransactionDto> GetStatus(string TransactionRef)
        {
            TransactionDto transaction;
            bool exist = _cache.TryGetValue("Ref", out transaction);
            if (!exist)
            {
                transaction = await _transRepo.GetStatus(TransactionRef);
                var refUser = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(20));
                _cache.Set("Ref", transaction, refUser);
            }
            if (transaction.TransactionRef.ToString() != TransactionRef)
            {
                transaction = await _transRepo.GetStatus(TransactionRef);
                var refUser = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(20));
                _cache.Set("Ref", transaction, refUser);
            }
            return transaction;
        }
    }
}
