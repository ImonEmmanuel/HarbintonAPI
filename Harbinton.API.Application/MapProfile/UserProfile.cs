using AutoMapper;
using Harbinton.API.Application.Dto.Transaction;
using Harbinton.API.Application.Dto.User;
using Harbinton.API.Domain.Model;

namespace Harbinton.API.Application.MapProfile
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<UserDto, CreateUserDto>().ReverseMap();
            CreateMap<User, AccountDto>().ReverseMap();
            CreateMap<Account, AccountDto>().ReverseMap();
            CreateMap<DisplayDetailsDto, User>().ReverseMap();
            CreateMap<Transaction, TransactionDto>().ReverseMap();
        }
    }
}
