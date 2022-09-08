
using Harbinton.API.Application.Contracts.Persitence;
using Harbinton.API.Application.Dto;
using Harbinton.API.Application.Dto.Transaction;
using Harbinton.API.Application.Extensions;
using Harbinton.API.Application.Helper.Interface;
using Harbinton.API.Application.ResponseData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace Harbinton.API.Controllers
{
    [Route("api/transaction")]
    [ApiController]
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository _repo;
        private readonly IMemoryCache _cache;
        private readonly IExtensionCache _extensioncache;

        public TransactionController(ITransactionRepository repo, IMemoryCache cache, IExtensionCache extensionCache)
        {
            _repo = repo;
            _cache = cache;
            _extensioncache = extensionCache;
        }
        [HttpPost("Transfer/{AccountNumber}")]
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.NotFound)]

        public async Task<ActionResult<ResponseModel>> Transfer(string AccountNumber, [FromBody] BaseDto baseDto)
        {

            string recordKey = "Transfer" + AccountNumber;
            if (_cache.TryGetValue(recordKey, out string cacheValue) != false)
            {
                ResponseModel responseModel = await _extensioncache.GetRecordAsync<ResponseModel>(_cache, recordKey);
                if (responseModel.Status == "Failed")
                {
                    return NotFound(responseModel);
                }
                return Ok(responseModel);
            }
            ResponseModel model = await _repo.Transfer(AccountNumber, baseDto.AccountNumberToTransfer, baseDto.Amount);
            await _extensioncache.SetRecordAsync(_cache, model, recordKey);
            if (model.Status == "Failed")
            {
                return NotFound(model);
            }
            return Ok(model);
        }


        [HttpPost("BillsPayment/{AccountNumber}")]
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ResponseModel>> BillsPayment(string AccountNumber, [FromBody] BillDto bill)
        {
            string recordKey = "BillPayment" + AccountNumber;
            if(_cache.TryGetValue(recordKey, out string cacheValue) != false)
            {
                ResponseModel responseModel = await _extensioncache.GetRecordAsync<ResponseModel>(_cache, recordKey);
                if (responseModel.Status == "Failed")
                {
                    return NotFound(responseModel);
                }

                return Ok(responseModel);
            }

            ResponseModel model = await _repo.BillPayment(AccountNumber, bill);
            await _extensioncache.SetRecordAsync(_cache, model, recordKey);
            if (model.Status == "Failed")
            {
                return NotFound(model);
            }

            return Ok(model);
        }

        [HttpGet("{TransactionRef}")]
        [ProducesResponseType(typeof(TransactionDto), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<TransactionDto>> GetTransactionStatus(string TransactionRef)
        {
            string recordKey = TransactionRef;
            if (_cache.TryGetValue(recordKey, out string cacheValue) != false)
            {
                return Ok(await _extensioncache.GetRecordAsync<TransactionDto>(_cache, recordKey));
            }
            TransactionDto transact = await _repo.GetStatus(recordKey);
            await _extensioncache.SetRecordAsync(_cache, transact, recordKey);
            return Ok(transact);
        }

        

    }
}
