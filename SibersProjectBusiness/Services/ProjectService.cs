using SibersProjectBusiness.DTOs.Project;
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

        public async Task<IEnumerable<ProjectEntity>> GetAll()
        {
            return await _projectRepo.GetAll();
        }

        //public async Task<IEnumerable<ProjectEntity>> GetAllFull()
        //{
        //}

        public async Task<IEnumerable<ProjectEntity>> GetAllByDirector(long directorId)
        {
            return await _projectRepo.GetAllByDirectorId(directorId);
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

        public async Task<ProjectEntity?> Update(long id, CreateProjectDto dto)
        {
            var project = await _projectRepo.GetById(id);
            if (project == null) return null;
            project.Title = dto.Title;
            project.CustomerCompany = dto.CustomerCompany;
            project.PerformerCompany = dto.PerformerCompany;
            project.DirectorId = dto.DirectorId;
            project.Priority = dto.Priority;
            return await _projectRepo.Update(project);
        }

        public async Task<ProjectEntity?> Delete(long id)
        {
            var project = await _projectRepo.GetById(id);
            if (project == null) return null;
            return await _projectRepo.Delete(project);
        }

        public async Task<bool> AddEmployeeToProject(UserProjectDto dto)
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
