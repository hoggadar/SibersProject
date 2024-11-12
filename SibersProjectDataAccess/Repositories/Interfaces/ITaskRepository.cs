using SibersProjectDataAccess.Entities;

namespace SibersProjectDataAccess.Repositories.Interfaces
{
    public interface ITaskRepository : IRepository<TaskEntity>
    {
        Task<IEnumerable<TaskEntity>> GetAllByEmployeeId(long employeeId);
        Task<IEnumerable<TaskEntity>> GetAllByAuthorId(long authorId);
        Task<TaskEntity?> GetByTitle(string title);
    }
}
