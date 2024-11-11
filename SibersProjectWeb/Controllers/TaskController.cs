using Microsoft.AspNetCore.Mvc;
using SibersProjectBusiness.DTOs.Task;
using SibersProjectBusiness.Interfaces;

namespace SibersProjectWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("get-all-tasks")]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskService.GetAll();
            return Ok(tasks);
        }

        [HttpGet("get-tasks-by-employee/{id}")]
        public async Task<IActionResult> GetAllByEmployee(long id)
        {
            var tasks = await _taskService.GetAllByEmployeeId(id);
            return Ok(tasks);
        }

        [HttpPost("create-task")]
        public async Task<IActionResult> Create([FromBody] CreateTaskDto dto)
        {
            var createdTask = await _taskService.Create(dto);
            return Ok(createdTask);
        }

        [HttpPut("update-task/{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateTaskDto dto, long id)
        {
            var updatedTask = await _taskService.Update(id, dto);
            if (updatedTask == null) return BadRequest("Task not found");
            return Ok(updatedTask);
        }

        [HttpDelete("delete-task/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var deletedUser = await _taskService.Delete(id);
            if (deletedUser == null) return BadRequest("Task not found");
            return Ok(deletedUser);
        }
    }
}
