using Harbinton.API.Contract;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Harbinton.API.Dto;
using System.Net;
using Harbinton.API.Model;

namespace Harbinton.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountCreationController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAccountCreationRepository _repo;
        public AccountCreationController(IAccountCreationRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
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
            var allCustomer = await _repo.GetAllCustomer();
            return Ok(allCustomer);
        }

        [HttpGet("{accountNumber}/CreditAccount", Name = "CreditAccount")]
        public async Task<ActionResult<string>> CreditAccount(string accountNumber, int Amount)
        {
            var details = await _repo.CreditAccount(accountNumber, Amount);
            if (details == null)
            {
                return BadRequest();
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
                return BadRequest();
            }
            return Ok(await _repo.GetCustomerDetails(accountId, accountNumber));
        }

        [HttpPut("{accountNumber}/UpdateAccountDetails", Name = "UpdateAccountDetails")]
        public async Task<ActionResult> UpdateAccountDetails(string accountNumber,[FromBody] CreateUserDto user)
        {
            await _repo.UpdateAccountAsync(accountNumber, user);
            return NoContent();
        }
    }
}
