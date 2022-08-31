using AutoMapper;
using Harbinton.API.Dto;
using Harbinton.API.Model;

namespace Harbinton.API.MapProfile
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
