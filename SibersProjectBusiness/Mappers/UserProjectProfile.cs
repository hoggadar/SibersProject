using AutoMapper;
using SibersProjectBusiness.DTOs.Project;
using SibersProjectDataAccess.Entities;

namespace SibersProjectBusiness.Mappers
{
    public class UserProjectProfile : Profile
    {
        public UserProjectProfile()
        {
            CreateMap<UserProjectDto, UserProjectEntity>();
            CreateMap<UserProjectEntity, UserProjectDto>();
        }
    }
}
