using SibersProjectDataAccess.Entities;

namespace SibersProjectDataAccess.Repositories.Interfaces
{
    public interface IProjectRepository : IRepository<ProjectEntity>
    {
        Task<ProjectEntity?> GetByTitle(string title);
    }
}
