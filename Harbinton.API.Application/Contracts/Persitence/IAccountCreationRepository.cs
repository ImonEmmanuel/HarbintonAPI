using Harbinton.API.Application.Dto.User;
using Harbinton.API.Application.ResponseData;
using Harbinton.API.Domain.Model;

namespace Harbinton.API.Application.Contracts.Persitence
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
        Task<ResponseModel> DeleteCustomerDetails(string accountNumber);
    }
}
