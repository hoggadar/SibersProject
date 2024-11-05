using SibersProjectBusiness.DTOs;
using SibersProjectBusiness.DTOs.Employee;
using SibersProjectBusiness.Interfaces;
using SibersProjectDataAccess.Entities;
using SibersProjectDataAccess.Repositories.Interfaces;

namespace SibersProjectBusiness.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepo;
        private readonly IUserProjectRepository _userProjectRepo;

        public ProjectService(IProjectRepository projectRepo, IUserProjectRepository userProjectRepo)
        {
            _projectRepo = projectRepo;
            _userProjectRepo = userProjectRepo;
        }

        public async Task<ProjectEntity> Create(CreateProjectDto dto)
        {
            var newProject = new ProjectEntity
            {
                Title = dto.Title,
                CustomerCompany = dto.CustomerCompany,
                PerformerCompany = dto.PerformerCompany,
                DirectorId = dto.DirectorId,
                StartDate = DateTime.UtcNow,
                EndDate = null,
                Priority = dto.Priority,
            };
            return await _projectRepo.Create(newProject);
        }

        public async Task<bool> AddEmployeeToProject(EmployeeProjectDto dto)
        {
            var newUserProject = new UserProjectEntity
            {
                EmployeeId = dto.EmployeeId,
                ProjectId = dto.ProjectId,
            };
            await _userProjectRepo.Create(newUserProject);
            return true;
        }
    }
}
