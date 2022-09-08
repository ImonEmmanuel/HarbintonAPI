using Harbinton.API.Application.Dto.User;
using Harbinton.API.Application.Extensions;

namespace Harbinton.API.Application.Helper.Interface
{
    public interface IAccountCacheHelper
    {
        Task<IEnumerable<UserDto>> GetAllAccount();
        Task<DisplayDetailsDto> GetAccountDetails(string? accountId, string? accountNumber);
    }
}