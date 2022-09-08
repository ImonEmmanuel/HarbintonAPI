using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Net;
using Harbinton.API.Application.Contracts.Persitence;
using Harbinton.API.Application.Dto.User;
using Harbinton.API.Application.ResponseData;
using Microsoft.Extensions.Caching.Memory;
using Harbinton.API.Application.Helper.Interface;
using Microsoft.Extensions.Caching.Distributed;
using Harbinton.API.Application.Extensions;

namespace Harbinton.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountCreationController : Controller
    {
        //private IAccountCacheHelper _cache;
        private readonly IAccountCreationRepository _repo;
        private readonly IMemoryCache _cache;
        private readonly IExtensionCache _extensionCache;
        public AccountCreationController(IAccountCreationRepository repo, IMemoryCache cache, IExtensionCache extensionCache)
        {
            _repo = repo;
            _cache = cache;
            _extensionCache = extensionCache;
        }

        [HttpPost("CreateAccount")]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserDto>> CreateAccount([FromBody] CreateUserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var mapUser = await _repo.CreateAccountAsync(user);
            return Ok(mapUser);

        }

        [HttpGet("GetAllCustomer")]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllCustomer()
        {
            string recordId = "AllUser";
            if (_cache.TryGetValue(recordId, out string cacheValue) != false)
            {
                IEnumerable<UserDto> cacheData = await _extensionCache.GetRecordAsync<IEnumerable<UserDto>>(_cache,recordId);
                return Ok(cacheData);
            }
            
            var allData = await _repo.GetAllCustomer();
            await _extensionCache.SetRecordAsync<IEnumerable<UserDto>>(_cache, allData, recordId);
            return Ok(allData);
        }

        [HttpGet("{accountNumber}/CreditAccount", Name = "CreditAccount")]
        public async Task<ActionResult<string>> CreditAccount(string accountNumber, int Amount)
        {
            var details = await _repo.CreditAccount(accountNumber, Amount);
            if (details == null)
            {
                return BadRequest("Account Not Found");
            }
            string note = $"Your account {details.AccountNumber} has been credited with {Amount} Current Account Balance is {details.Amount}";
            return Ok(note);
        }

        [HttpGet("GetCustomerDetails")]
        [ProducesResponseType(typeof(DisplayDetailsDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<DisplayDetailsDto>> GetCustomerDetails(string? accountId, string? accountNumber)
        {
            if (accountId == null & accountNumber == null)
            {
                return BadRequest("Supply either an AccountID or AccountNumber");
            }
            string recordKey = "Customer";

            if (!string.IsNullOrEmpty(accountNumber))
                recordKey = recordKey + accountNumber;

            else
                recordKey = recordKey + accountId;
                

            if (_cache.TryGetValue(recordKey, out string cacheValue) != false)
            {
                DisplayDetailsDto details = await _extensionCache.GetRecordAsync<DisplayDetailsDto>(_cache, recordKey);
                return Ok(details);
            }
            DisplayDetailsDto data = await _repo.GetCustomerDetails(accountId, accountNumber);
            if (data == null)
            {
                return BadRequest("Account Not Found Supply a Valid Details");
            }
            await _extensionCache.SetRecordAsync<DisplayDetailsDto>(_cache, data, recordKey);
            return Ok(data);
        }

        [HttpPut("{accountNumber}/UpdateAccountDetails", Name = "UpdateAccountDetails")]
        public async Task<ActionResult> UpdateAccountDetails(string accountNumber,[FromBody] CreateUserDto user)
        {
            await _repo.UpdateAccountAsync(accountNumber, user);
            return NoContent();
        }

        [HttpDelete("{accountNumber}")]
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ResponseModel>> DeleteAccount(string accountNumber)
        {
            ResponseModel model = await _repo.DeleteCustomerDetails(accountNumber);
            if(model != null)
            {
                return NotFound(model);
            }
            return NoContent();
        }
    }
}
