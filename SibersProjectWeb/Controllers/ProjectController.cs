using Microsoft.AspNetCore.Authorization;
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
        private readonly IFileService _fileService;

        public ProjectController(IProjectService projectService, IFileService fileService)
        {
            _projectService = projectService;
            _fileService = fileService;
        }

        [Authorize]
        [HttpGet("get-all-projects")]
        public async Task<IActionResult> GetAll()
        {
            var projects = await _projectService.GetAll();
            return Ok(projects);
        }

        [Authorize(Roles = "Director,ProjectManager")]
        [HttpGet("get-all-projects/{directorId}")]
        public async Task<IActionResult> GetAllByDirector(long directorId)
        {
            var projects = await _projectService.GetAllByDirector(directorId);
            return Ok(projects);
        }

        [Authorize(Roles = "Director")]
        [HttpPost("create-project")]
        public async Task<IActionResult> Create([FromBody] CreateProjectDto dto)
        {
            var createdUser = await _projectService.Create(dto);
            return Ok(createdUser);
        }

        [Authorize(Roles = "Director")]
        [HttpPut("update-project/{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateProjectDto dto, long id)
        {
            var updatedUser = await _projectService.Update(id, dto);
            if (updatedUser == null) return BadRequest("Project not found");
            return Ok(updatedUser);
        }

        [Authorize(Roles = "Director")]
        [HttpDelete("delete-project/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var deletedUser = await _projectService.Delete(id);
            if (deletedUser == null) return BadRequest("Project not found");
            return Ok(deletedUser);
        }

        [Authorize(Roles = "Director")]
        [HttpPost("upload-documents/{id}")]
        public async Task<IActionResult> UploadDocument(long id, IFormFileCollection files)
        {
            if (files == null || files.Count == 0) return BadRequest("No files uploaded");
            try
            {
                var uploadedDocuments = await _fileService.UploadFilesAsync(id, files);
                var createdDocuments = await _projectService.AddDocuments(uploadedDocuments);
                return Ok(createdDocuments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Director,ProjectManager")]
        [HttpPost("add-employee-to-project")]
        public async Task<IActionResult> AddEmployee([FromBody] UserProjectDto dto)
        {
            var result = await _projectService.AddEmployeeToProject(dto);
            return Ok(result);
        }

        [Authorize(Roles = "Director,ProjectManager")]
        [HttpPost("remove-employee-from-project")]
        public async Task<IActionResult> RemoveEmployee([FromBody] UserProjectDto dto)
        {
            var result = await _projectService.RemoveEmployeeFromProject(dto);
            return Ok(result);
        }
    }
}
