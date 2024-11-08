using Microsoft.AspNetCore.Mvc;
using SibersProjectBusiness.DTOs.Project;
using SibersProjectBusiness.Interfaces;

namespace SibersProjectWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("get-all-projects")]
        public async Task<IActionResult> GetAll()
        {
            var projects = await _projectService.GetAll();
            return Ok(projects);
        }

        [HttpGet("get-all-projects/{directorId}")]
        public async Task<IActionResult> GetAllByDirector(long directorId)
        {
            var projects = await _projectService.GetAllByDirector(directorId);
            return Ok(projects);
        }

        [HttpPost("create-project")]
        public async Task<IActionResult> Create([FromBody] CreateProjectDto dto)
        {
            var createdUser = await _projectService.Create(dto);
            return Ok(createdUser);
        }

        [HttpPost("add-employee-to-project")]
        public async Task<IActionResult> AddEmployee([FromBody] UserProjectDto dto)
        {
            var result = await _projectService.AddEmployeeToProject(dto);
            return Ok(result);
        }

        [HttpPost("remove-employee-from-project")]
        public async Task<IActionResult> RemoveEmployee([FromBody] UserProjectDto dto)
        {
            var result = await _projectService.RemoveEmployeeFromProject(dto);
            return Ok(result);
        }

        [HttpPut("update-project/{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateProjectDto dto, long id)
        {
            var updatedUser = await _projectService.Update(id, dto);
            if (updatedUser == null) return BadRequest("Project not found");
            return Ok(updatedUser);
        }

        [HttpDelete("delete-project/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var deletedUser = await _projectService.Delete(id);
            if (deletedUser == null) return BadRequest("Project not found");
            return Ok(deletedUser);
        }
    }
}
