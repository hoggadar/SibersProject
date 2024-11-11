using AutoMapper;
using SibersProjectBusiness.DTOs.Task;
using SibersProjectDataAccess.Entities;

namespace SibersProjectBusiness.Mappers
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskEntity, TaskDto>();
            CreateMap<CreateTaskDto, TaskEntity>();
            CreateMap<UpdateTaskDto, TaskEntity>();
        }
    }
}
