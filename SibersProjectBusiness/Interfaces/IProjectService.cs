using SibersProjectBusiness.DTOs;
using SibersProjectBusiness.DTOs.Employee;
using SibersProjectDataAccess.Entities;

namespace SibersProjectBusiness.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectEntity> Create(CreateProjectDto dto);
        Task<bool> AddEmployeeToProject(EmployeeProjectDto dto);
    }
}
