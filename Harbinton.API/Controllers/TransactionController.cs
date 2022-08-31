using Harbinton.API.Contract;
using Harbinton.API.Dto;
using Harbinton.API.ResponseData;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Harbinton.API.Controllers
{
    [Route("api/transaction")]
    [ApiController]
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository _repo;

        public TransactionController(ITransactionRepository repo)
        {
            _repo = repo;
        }
        [HttpPost("Transfer/{AccountNumber}")]
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.NotFound)]

        public async Task<ActionResult<ResponseModel>> Transfer(string AccountNumber, [FromBody] BaseDto baseDto)
        {
            ResponseModel model = await _repo.Transfer(AccountNumber, baseDto.AccountNumberToTransfer, baseDto.Amount);
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
            ResponseModel model = await _repo.BillPayment(AccountNumber, bill);
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
            TransactionDto transact = await _repo.GetStatus(TransactionRef);
            return Ok(transact);
        }

        

    }
}
