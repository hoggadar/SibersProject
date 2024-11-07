using SibersProjectDataAccess.Entities;

namespace SibersProjectDataAccess.Repositories.Interfaces
{
    public interface IProjectRepository : IRepository<ProjectEntity>
    {
        //Task<IEnumerable<ProjectEntity>> GetAllFull();
        Task<IEnumerable<ProjectEntity>> GetAllByDirectorId(long directorId);
        //Task<ProjectEntity?> GetByIdFull(long id);
        Task<ProjectEntity?> GetByTitle(string title);
    }
}
