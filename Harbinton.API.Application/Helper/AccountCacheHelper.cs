using Harbinton.API.Application.Contracts.Persitence;
using Harbinton.API.Application.Dto.User;
using Harbinton.API.Application.Helper.Interface;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harbinton.API.Application.Extensions;
using Microsoft.Extensions.Caching.Distributed;

namespace Harbinton.API.Application.Helper
{
    public class AccountCacheHelper : IAccountCacheHelper
    {
        private readonly IMemoryCache _cache;
        private readonly IAccountCreationRepository _accRepo;

        public AccountCacheHelper(IMemoryCache cache, IAccountCreationRepository accRepo)
        {
            _cache = cache;
            _accRepo = accRepo;
        }
        public async Task<DisplayDetailsDto> GetAccountDetails(string? accountId, string? accountNumber)
        {
            
            DisplayDetailsDto userDetails;
            bool exist = _cache.TryGetValue("UserDetails", out userDetails);
            if (!exist)
            {
                userDetails = await _accRepo.GetCustomerDetails(accountId, accountNumber);
                var cachedUser = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(20));
                _cache.Set<DisplayDetailsDto>("CachedTime", userDetails, cachedUser);
            }

            //To check if the account Number been passed is what is in the cached DB
            if(userDetails.Account.AccountNumber != accountNumber)
            {
                userDetails = await _accRepo.GetCustomerDetails(accountId, accountNumber);
                var cachedUser = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(20));
                _cache.Set("CachedTime", userDetails, cachedUser);
            }
            return userDetails;
        }

        public async Task<IEnumerable<UserDto>> GetAllAccount()
        {
            //string recordKey = "AllUser";
            //IEnumerable<UserDto> allData = await _distCache.GetRecordAsync<IEnumerable<UserDto>>(recordKey);

            //if(allData is null)
            //{
            //    allData = await _accRepo.GetAllCustomer();
            //    await _distCache.SetRecordAsync<IEnumerable<UserDto>>(recordKey, allData);
            //}

            //return allData;

            IEnumerable<UserDto> userdto;
            bool exist = _cache.TryGetValue("AllUser", out userdto);
            if (!exist)
            {
                userdto = await _accRepo.GetAllCustomer();
                var cachedEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(20));
                _cache.Set("AllUser", userdto, cachedEntryOptions);
            }
            return userdto;
        }
    }
}
