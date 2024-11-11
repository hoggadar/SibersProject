using AutoMapper;
using SibersProjectBusiness.DTOs.User;
using SibersProjectDataAccess.Entities;

namespace SibersProjectBusiness.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserEntity, UserDto>();
            CreateMap<CreateUserDto, UserEntity>();
            CreateMap<UpdateUserDto, UserEntity>();
        }
    }
}
