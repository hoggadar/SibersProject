using AutoMapper;
using Microsoft.Extensions.Logging;
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
        private readonly IMapper _mapper;
        private readonly ILogger<TaskService> _logger;

        public TaskService(ITaskRepository taskRepo, IMapper mapper, ILogger<TaskService> logger)
        {
            _taskRepo = taskRepo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<TaskDto>> GetAll()
        {
            var tasks = await _taskRepo.GetAll();
            return _mapper.Map<IEnumerable<TaskDto>>(tasks);
        }

        public async Task<IEnumerable<TaskDto>> GetAllByEmployeeId(long id)
        {
            var tasks = await _taskRepo.GetAllByEmployeeId(id);
            return _mapper.Map<IEnumerable<TaskDto>>(tasks);
        }

        public async Task<IEnumerable<TaskDto>> GetAllByAuthorId(long id)
        {
            var tasks = await _taskRepo.GetAllByAuthorId(id);
            return _mapper.Map<IEnumerable<TaskDto>>(tasks);
        }

        public async Task<TaskDto?> GetById(long id)
        {
            var task = await _taskRepo.GetById(id);
            return _mapper.Map<TaskDto>(task);
        }

        public async Task<TaskDto> Create(CreateTaskDto dto)
        {
            var newTask = _mapper.Map<TaskEntity>(dto);
            var createdTask = await _taskRepo.Create(newTask);
            return _mapper.Map<TaskDto>(createdTask);
        }

        public async Task<TaskDto?> Update(long id, UpdateTaskDto dto)
        {
            var existingTask = await _taskRepo.GetById(id);
            if (existingTask == null) return null;
            if (!Enum.TryParse<StatusEnum>(dto.Status, true, out var _))
            {
                throw new ArgumentException($"Invalid role: {dto.Status}");
            }
            var updatedTask = _mapper.Map(dto, existingTask);
            var result = await _taskRepo.Update(updatedTask);
            return _mapper.Map<TaskDto?>(result);
        }

        public async Task<TaskDto?> Delete(long id)
        {
            var task = await _taskRepo.GetById(id);
            if (task == null) return null;
            var deletedTask = await _taskRepo.Delete(task);
            return _mapper.Map<TaskDto?>(deletedTask);
        }
    }
}
