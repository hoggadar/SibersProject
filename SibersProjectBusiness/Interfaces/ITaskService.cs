using SibersProjectBusiness.DTOs.Task;
using SibersProjectDataAccess.Entities;

namespace SibersProjectBusiness.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetAll();
        Task<IEnumerable<TaskDto>> GetAllByEmployeeId(long id);
        Task<TaskDto?> GetById(long id);
        Task<TaskDto> Create(CreateTaskDto dto);
        Task<TaskDto?> Update(long id, UpdateTaskDto dto);
        Task<TaskDto?> Delete(long id);
    }
}
