using SibersProjectBusiness.DTOs.Task;
using SibersProjectDataAccess.Entities;

namespace SibersProjectBusiness.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskEntity>> GetAll();
        Task<TaskEntity?> GetById(long id);
        Task<TaskEntity> Create(TaskDto dto);
        //Task<TaskEntity> Update(TaskEntity entity);
        Task<TaskEntity?> Delete(long id);
    }
}
