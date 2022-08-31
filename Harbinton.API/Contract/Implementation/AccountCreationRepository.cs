using AutoMapper;
using Harbinton.API.Database;
using Harbinton.API.Dto;
using Harbinton.API.Model;
using Harbinton.API.ResponseData;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Harbinton.API.Contract.Implementation
{
    public class AccountCreationRepository : IAccountCreationRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public AccountCreationRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateAccount()
        {
            Account userAccount = new Account()
            {
                Amount = 0
            };
            await _context.Accounts.AddAsync(userAccount);
            await _context.SaveChangesAsync();
            return userAccount.Id;
        }

        public async Task<UserDto> CreateAccountAsync(CreateUserDto user)
        {
            var newUser = _mapper.Map<User>(user);
            newUser.AccountId = await CreateAccount();
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDto>(newUser);

        }

        public async Task<Account?> CreditAccount(string accountNumber, int Amount)
        {
            var currentUser = await _context.Accounts.FirstOrDefaultAsync(x => x.AccountNumber == accountNumber);

            if (currentUser == null)
            {
                return null; 
            }
            var bal = currentUser.Amount;
            currentUser.Amount = bal + Amount;
            await _context.SaveChangesAsync();
            return currentUser;
        }

        public async Task<DisplayDetailsDto> GetCustomerDetails(string? accountId, string? accountNumber)
        {
            if (accountNumber != null)
            {
                Account acc = await _context.Accounts.FirstOrDefaultAsync(x => x.AccountNumber == accountNumber);
                if (acc == null)
                {
                    return null;
                }
                User userAcc = await _context.Users.FirstOrDefaultAsync(x => x.AccountId == acc.Id);
                var mapUser = _mapper.Map<DisplayDetailsDto>(userAcc);
                var mapAcc = _mapper.Map<AccountDto>(acc);
                mapUser.Account = mapAcc;
                return mapUser;
            }

            if (accountId != null)
            {
                Account acc = await _context.Accounts.FirstOrDefaultAsync(x => x.Id.ToString() == accountId);
                if (acc == null)
                {
                    return null;
                }
                User userAcc = await _context.Users.FirstOrDefaultAsync(x => x.AccountId == acc.Id);
                var mapUser = _mapper.Map<DisplayDetailsDto>(userAcc);
                var mapAcc = _mapper.Map<AccountDto>(acc);
                mapUser.Account = mapAcc;
                return mapUser;
            }
            return null;
        }

        public Task<string> GetAccountBalanceAsync(string accountNumber)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserDto>> GetAllCustomer()
        {
            var alluser = await _context.Users.OrderBy(x => x.FirstName).ToListAsync();
            var mapUser = _mapper.Map<IEnumerable<UserDto>>(alluser);
            return mapUser;
        }

        public async Task UpdateAccountAsync(string accountNumber, CreateUserDto user)
        {
            var check = await _context.Accounts.FirstOrDefaultAsync(x => x.AccountNumber == accountNumber);
            if (check == null)
            {
                return;
            }
            var userUpdate = await _context.Users.FirstOrDefaultAsync(x => x.AccountId == check.Id);
            userUpdate.Address = user.Address;
            userUpdate.FirstName = user.FirstName;
            userUpdate.LastName = user.LastName;
            userUpdate.Phone = userUpdate.Phone;

            _context.Users.Update(userUpdate);
            await _context.SaveChangesAsync();
        }

        public async Task<ResponseModel> DeleteCustomerDetails(string accountNumber)
        {
            Account detail = await _context.Accounts.FirstOrDefaultAsync(x => x.AccountNumber == accountNumber);
            if (detail == null)
            {
                return new ResponseModel()
                {
                    Message = "Account Not Found",
                    ResponseCode = (int)HttpStatusCode.NotFound,
                    Status = "Failed",
                    TransactionRef = accountNumber
                };
            }
            User user = await _context.Users.Where(x => x.AccountId == detail.Id).FirstAsync();
            _context.Accounts.Remove(detail);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return null;

        }
    }
}
