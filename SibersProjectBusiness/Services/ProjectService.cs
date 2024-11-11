using AutoMapper;
using Microsoft.Extensions.Logging;
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
        private readonly IMapper _mapper;
        private readonly ILogger<ProjectService> _logger;

        public ProjectService(IProjectRepository projectRepo, IUserProjectRepository userProjectRepo, IMapper mapper, ILogger<ProjectService> logger)
        {
            _projectRepo = projectRepo;
            _userProjectRepo = userProjectRepo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ProjectDto>> GetAll()
        {
            var projets = await _projectRepo.GetAll();
            return _mapper.Map<IEnumerable<ProjectDto>>(projets);
        }

        public async Task<IEnumerable<ProjectDto>> GetAllByDirector(long directorId)
        {
            var projects = await _projectRepo.GetAllByDirectorId(directorId);
            return _mapper.Map<IEnumerable<ProjectDto>>(projects);
        }

        public async Task<ProjectDto> Create(CreateProjectDto dto)
        {
            var newProject = _mapper.Map<ProjectEntity>(dto);
            newProject.StartDate = DateTime.UtcNow;
            var createdProject = await _projectRepo.Create(newProject);
            return _mapper.Map<ProjectDto>(createdProject);
        }

        public async Task<ProjectDto?> Update(long id, UpdateProjectDto dto)
        {
            var existingProject = await _projectRepo.GetById(id);
            if (existingProject == null) return null;
            var updatedProject = _mapper.Map(dto, existingProject);
            var result = await _projectRepo.Update(updatedProject);
            return _mapper.Map<ProjectDto?>(result);
        }

        public async Task<ProjectDto?> Delete(long id)
        {
            var project = await _projectRepo.GetById(id);
            if (project == null) return null;
            var deletedProject = await _projectRepo.Delete(project);
            return _mapper.Map<ProjectDto>(deletedProject);
        }

        public async Task<UserProjectDto> AddEmployeeToProject(UserProjectDto dto)
        {
            var newUserProject = _mapper.Map<UserProjectEntity>(dto);
            var createdUserProject = await _userProjectRepo.Create(newUserProject);
            return _mapper.Map<UserProjectDto>(createdUserProject);
        }

        public async Task<UserProjectDto?> RemoveEmployeeFromProject(UserProjectDto dto)
        {
            var userProject = await _userProjectRepo.GetByUserAndProject(dto.EmployeeId, dto.ProjectId);
            if (userProject == null) return null;
            var deletedUserProject = await _userProjectRepo.Delete(userProject);
            return _mapper.Map<UserProjectDto>(deletedUserProject);
        }
    }
}
