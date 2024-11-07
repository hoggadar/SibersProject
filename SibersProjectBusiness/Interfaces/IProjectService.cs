using SibersProjectBusiness.DTOs.Project;
using SibersProjectDataAccess.Entities;

namespace SibersProjectBusiness.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectEntity>> GetAll();
        //Task<IEnumerable<ProjectEntity>> GetAllFull();
        Task<IEnumerable<ProjectEntity>> GetAllByDirector(long directorId);
        Task<ProjectEntity> Create(CreateProjectDto dto);
        Task<ProjectEntity?> Update(long id, UpdateProjectDto dto);
        Task<ProjectEntity?> Delete(long id);
        Task<bool> AddEmployeeToProject(UserProjectDto dto);
        Task<bool> DeleteEmployeeFromProject(UserProjectDto dto);
    }
}
