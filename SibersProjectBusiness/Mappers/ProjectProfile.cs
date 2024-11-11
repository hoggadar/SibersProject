using AutoMapper;
using SibersProjectBusiness.DTOs.Project;
using SibersProjectDataAccess.Entities;

namespace SibersProjectBusiness.Mappers
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectEntity, ProjectDto>();
            CreateMap<ProjectDto, ProjectEntity>();
            CreateMap<CreateProjectDto, ProjectEntity>();
            CreateMap<UpdateProjectDto, ProjectEntity>();
        }
    }
}
