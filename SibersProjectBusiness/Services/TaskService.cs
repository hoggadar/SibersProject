using SibersProjectBusiness.DTOs.Task;
using SibersProjectBusiness.Interfaces;
using SibersProjectDataAccess.Entities;
using SibersProjectDataAccess.Enums;
using SibersProjectDataAccess.Repositories.Interfaces;

namespace SibersProjectBusiness.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepo;

        public TaskService(ITaskRepository taskRepo)
        {
            _taskRepo = taskRepo;
        }

        public async Task<IEnumerable<TaskEntity>> GetAll()
        {
            return await _taskRepo.GetAll();
        }

        public async Task<TaskEntity?> GetById(long id)
        {
            return await _taskRepo.GetById(id);
        }

        public async Task<TaskEntity> Create(TaskDto dto)
        {
            var task = new TaskEntity
            {
                Title = dto.Title,
                AuthorId = dto.AuthorId,
                PerformerId = dto.PerformerId,
                Status = StatusEnum.ToDo,
                ProjectId = dto.ProjectId,
                Priority = dto.Priority,
            };
            return await _taskRepo.Create(task);
        }

        public async Task<TaskEntity?> Delete(long id)
        {
            var task = await _taskRepo.GetById(id);
            if (task == null) return null;
            return await _taskRepo.Delete(task);
        }
    }
}
