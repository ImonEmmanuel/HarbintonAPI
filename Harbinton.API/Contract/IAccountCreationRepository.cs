using Harbinton.API.Dto;
using Harbinton.API.Model;

namespace Harbinton.API.Contract
{
    public interface IAccountCreationRepository
    {
        Task<UserDto> CreateAccountAsync(CreateUserDto user);
        Task UpdateAccountAsync(string accountNumber, CreateUserDto user);
        Task<string> GetAccountBalanceAsync(string accountNumber);
        Task<Account?> CreditAccount(string accountNumber, int Amount);
        Task<IEnumerable<UserDto>> GetAllCustomer();
        Task<int> CreateAccount();
        Task<DisplayDetailsDto> GetCustomerDetails(string? accountId, string? accountNumber);
    }
}
