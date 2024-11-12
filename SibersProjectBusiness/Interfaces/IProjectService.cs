using SibersProjectBusiness.DTOs.Document;
using SibersProjectBusiness.DTOs.Project;
using SibersProjectDataAccess.Entities;

namespace SibersProjectBusiness.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAll();
        Task<IEnumerable<ProjectDto>> GetAllByDirector(long directorId);
        Task<ProjectDto> Create(CreateProjectDto dto);
        Task<ProjectDto?> Update(long id, UpdateProjectDto dto);
        Task<ProjectDto?> Delete(long id);
        Task<IEnumerable<DocumentDto>> AddDocuments(IEnumerable<ProjectDocumentEntity> documents);
        Task<UserProjectDto> AddEmployeeToProject(UserProjectDto dto);
        Task<UserProjectDto?> RemoveEmployeeFromProject(UserProjectDto dto);
    }
}
