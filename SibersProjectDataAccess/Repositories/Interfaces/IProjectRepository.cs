using SibersProjectDataAccess.Entities;

namespace SibersProjectDataAccess.Repositories.Interfaces
{
    public interface IProjectRepository : IRepository<ProjectEntity>
    {
        Task<IEnumerable<ProjectEntity>> GetAllByDirectorId(long directorId);
        Task<ProjectEntity?> GetByTitle(string title);
        Task<IEnumerable<ProjectDocumentEntity>> AddDocuments(IEnumerable<ProjectDocumentEntity> documents);
    }
}
