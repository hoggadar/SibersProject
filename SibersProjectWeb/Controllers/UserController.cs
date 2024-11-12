using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SibersProjectBusiness.DTOs.User;
using SibersProjectBusiness.Interfaces;

namespace SibersProjectWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet("get-all-users")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        [Authorize(Roles = "Director,ProjectManager")]
        [HttpGet("get-users-by-project/{projectId}")]
        public async Task<IActionResult> GetAllByProject(long projectId)
        {
            var users = await _userService.GetAllByProjectId(projectId);
            return Ok(users);
        }

        [Authorize(Roles = "Director")]
        [HttpPost("create-user")]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            var createdUser = await _userService.Create(dto);
            return Ok(createdUser);
        }

        [Authorize(Roles = "Director")]
        [HttpPut("update-user/{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateUserDto dto, long id)
        {
            var updatedUser = await _userService.Update(id, dto);
            if (updatedUser == null) return BadRequest("User not found");
            return Ok(updatedUser);
        }

        [Authorize(Roles = "Director")]
        [HttpDelete("delete-user/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var deletedUser = await _userService.Delete(id);
            if (deletedUser == null) return BadRequest("User not found");
            return Ok(deletedUser);
        }
    }
}
